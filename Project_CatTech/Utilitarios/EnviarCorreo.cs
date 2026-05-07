using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_CatTech.Utilitarios
{
    public class EnviarCorreo
    {
        public string CuentaCorreoElectronico = "utnpruebaisw711@gmail.com";
        public string ContrasenaGeneradaGmail = "fhplwjoogyjbpftd";

        public void enviarCorreoGmail(string body, string receptor, string asunto, List<string> adjuntos)
        {
            MailMessage mensaje = new MailMessage();
            mensaje.IsBodyHtml = true;
            mensaje.Subject = asunto;
            mensaje.Body = body;
            mensaje.From = new MailAddress(CuentaCorreoElectronico);
            mensaje.To.Add(receptor);

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential(CuentaCorreoElectronico, ContrasenaGeneradaGmail);
            smtp.EnableSsl = true;

            if (adjuntos != null)
            {
                foreach (string archivo in adjuntos)
                {
                    if (!string.IsNullOrWhiteSpace(archivo) && System.IO.File.Exists(archivo))
                    {
                        Attachment attachment = new Attachment(archivo);
                        mensaje.Attachments.Add(attachment);
                    }
                }
            }

            smtp.Send(mensaje);
            MessageBox.Show("Correo enviado correctamente", "Enviar Correo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}

