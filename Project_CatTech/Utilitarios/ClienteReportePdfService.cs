using Project_CatTech.Layer.DTO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System;
using System.Globalization;
using QuestPDF.Helpers;
using Project_CatTech.Layer;
using Project_CatTech.Layer.Entities;

public class ClienteReportePdfService
{
    public void GenerarPdf(List<Cliente> lista)
    {
        try
        {
            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "ReporteClientes_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".pdf"
            );

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            var pdfBytes = Document.Create(documento =>
            {
                documento.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);
                    page.PageColor("#FFFDFE");
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor("#3F3A3D"));

                    page.Header().Column(col =>
                    {
                        col.Item().Background("#FCE4EC").Padding(16).Column(c =>
                        {
                            c.Item().Text("SweetTech")
                                .FontSize(24)
                                .Bold()
                                .FontColor("#D63384");

                            c.Item().Text("Reporte de Clientes")
                                .FontSize(15)
                                .SemiBold()
                                .FontColor("#7A4E64");

                            c.Item().PaddingTop(6).Text("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        });
                    });

                    page.Content().PaddingVertical(15).Column(col =>
                    {
                        col.Spacing(12);

                        foreach (var item in lista)
                        {
                            col.Item()
                                .Background("#FFF1F5")
                                .Border(1)
                                .BorderColor("#F8BBD0")
                                .Padding(12)
                                .Column(c =>
                                {
                                    c.Spacing(4);

                                    c.Item().Text(
                                        (item.Nombre + " " + item.PrimerApellido + " " + item.SegundoApellido).Trim())
                                        .Bold()
                                        .FontSize(13)
                                        .FontColor("#C2185B");

                                    c.Item().Text("Identificación: " + item.Identificacion);
                                    c.Item().Text("Teléfono: " + item.Telefono);
                                    c.Item().Text("Correo: " + item.Correo);
                                    c.Item().Text("Provincia: " + item.Provincia);
                                    c.Item().Text("Dirección: " + item.Direccion);

                                    //c.Item().PaddingTop(6).Text("Fotografía")
                                    //    .SemiBold()
                                    //    .FontColor("#A64D79") ;

                                    //c.Item().Height(90).Border(1).BorderColor("#F8BBD0").Background(Colors.White).AlignMiddle().AlignCenter().Element(cont =>
                                    //{
                                    //    if (item.Fotografia != null && item.Fotografia.Length > 0)
                                    //        cont.Image(item.Fotografia);
                                    //    else
                                    //        cont.Text("Sin foto").Italic().FontColor("#8A6C79");
                                    //});
                                    c.Item().Row(row =>
                                    {
                                        // 🔹 Texto a la izquierda
                                        row.RelativeItem().AlignMiddle().Text("Fotografía")
                                            .SemiBold()
                                            .FontColor("#A64D79");

                                        // 🔹 Imagen a la derecha
                                        row.ConstantItem(100).Height(90)
                                            .Border(1)
                                            .BorderColor("#F8BBD0")
                                            .Background(Colors.White)
                                            .AlignMiddle()
                                            .AlignCenter()
                                            .Element(cont =>
                                            {
                                                if (item.Fotografia != null && item.Fotografia.Length > 0)
                                                {
                                                    cont.Image(item.Fotografia, ImageScaling.FitArea);
                                                }
                                                else
                                                {
                                                    cont.Text("Sin foto")
                                                        .Italic()
                                                        .FontColor("#8A6C79");
                                                }
                                            });
                                    });
                                });
                        }
                    });

                    page.Footer().PaddingTop(8).Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor("#F8BBD0");
                        col.Item().PaddingTop(5).AlignCenter().Text("Reporte generado por SweetTech")
                            .FontSize(9)
                            .FontColor("#A64D79");
                    });
                });
            }).GeneratePdf();

            File.WriteAllBytes(ruta, pdfBytes);
            Process.Start(new ProcessStartInfo(ruta) { UseShellExecute = true });

        }
        catch (Exception ex)
        {

            MessageBox.Show("Error al generar reporte de clientes: " + ex.Message);
        }
    }
}


