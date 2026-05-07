using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using   Project_CatTech.Layer.Entities;
using System.Xml;

namespace Project_CatTech.Utilitarios.Util
{
    public  class FacturaXmlHelper
    {
        public static string GenerarXml(Factura factura, List<FacturaDetalle> detalles, string nombreCliente, string tipoPago)
        {
            XmlDocument xmlDoc = new XmlDocument();


            XmlElement root = xmlDoc.CreateElement("Factura");
            xmlDoc.AppendChild(root);

            XmlElement encabezado = xmlDoc.CreateElement("Encabezado");
            root.AppendChild(encabezado);

            AgregarNodo(xmlDoc, encabezado, "IdFactura", factura.IdFactura.ToString());
            AgregarNodo(xmlDoc, encabezado, "NumeroFactura", factura.NumeroFactura);
            AgregarNodo(xmlDoc, encabezado, "Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm:ss"));
            AgregarNodo(xmlDoc, encabezado, "IdCliente", factura.IdCliente.ToString());
            AgregarNodo(xmlDoc, encabezado, "Cliente", nombreCliente);
            AgregarNodo(xmlDoc, encabezado, "IdUsuario", factura.IdUsuario.ToString());
            AgregarNodo(xmlDoc, encabezado, "SubTotal", factura.SubTotal.ToString("F2"));
            AgregarNodo(xmlDoc, encabezado, "Impuesto", factura.Impuesto.ToString("F2"));
            AgregarNodo(xmlDoc, encabezado, "TotalColones", factura.TotalColones.ToString("F2"));
            AgregarNodo(xmlDoc, encabezado, "TotalDolares", factura.TotalDolares.ToString("F2"));
            AgregarNodo(xmlDoc, encabezado, "TipoPago", tipoPago);
            //AgregarNodo(xmlDoc, encabezado, "Estado", factura.Estado);
            AgregarNodo(xmlDoc, encabezado, "Estado", factura.Estado.ToString());

            XmlElement detalleNode = xmlDoc.CreateElement("Detalle");
            root.AppendChild(detalleNode);

            foreach (FacturaDetalle item in detalles)
            {
                XmlElement linea = xmlDoc.CreateElement("Linea");
                detalleNode.AppendChild(linea);

                AgregarNodo(xmlDoc, linea, "IdProducto", item.IdProducto.ToString());
                AgregarNodo(xmlDoc, linea, "Cantidad", item.Cantidad.ToString());
                AgregarNodo(xmlDoc, linea, "Precio", item.Precio.ToString("F2"));
                AgregarNodo(xmlDoc, linea, "SubTotal", item.Subtotal.ToString("F2"));
            }

            return xmlDoc.OuterXml;
        }

        private static void AgregarNodo(XmlDocument doc, XmlNode padre, string nombre, string valor)
        {
            XmlElement nodo = doc.CreateElement(nombre);
            nodo.InnerText = valor ?? string.Empty;
            padre.AppendChild(nodo);
        }
    }
}

