using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Project_CatTech.Layer.Entities;

namespace Project_CatTech.Layer.Utilitarios
{
    public static class FacturaPdfService
    {
        public static string GenerarPdfFactura(
              Factura factura,
              List<FacturaDetalle> detalles,
              string nombreCliente,
              string cedulaCliente,
              string nombreUsuario,
              string tipoPago,
              byte[] firmaBytes,
              System.Drawing.Image qrImage)
        {
            if (factura == null)
                throw new Exception("La factura no puede ser nula.");

            if (detalles == null || detalles.Count == 0)
                throw new Exception("La factura no tiene detalle.");

            string carpeta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FacturasPDF");

            if (!Directory.Exists(carpeta))
                Directory.CreateDirectory(carpeta);

            string rutaPdf = Path.Combine(carpeta, factura.NumeroFactura + ".pdf");

            byte[] qrBytes = ConvertirImagenAPng(qrImage);

            Document.Create(documento =>
            {
                documento.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(28);
                    page.PageColor("#FFFDFE");
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor("#3F3A3D"));

                    page.Header().Column(col =>
                    {
                        col.Item().Background("#FCE4EC").Padding(18).Row(row =>
                        {
                            row.RelativeItem().Column(c =>
                            {
                                c.Item().Text("CatTech")
                                    .FontSize(26)
                                    .Bold()
                                    .FontColor("#D63384");

                                c.Item().Text("Factura electrónica")
                                    .FontSize(14)
                                    .SemiBold()
                                    .FontColor("#7A4E64");

                                c.Item().PaddingTop(8).Text("Número de factura: " + factura.NumeroFactura)
                                    .FontSize(11);

                                c.Item().Text("Fecha: " + factura.Fecha.ToString("dd/MM/yyyy HH:mm"))
                                    .FontSize(11);

                                c.Item().Text("Usuario: " + nombreUsuario)
                                    .FontSize(11);

                                c.Item().Text("Estado: " + (factura.Estado ? "Activa" : "Pendiente"))
                                    .FontSize(11);
                            });

                            if (qrBytes != null)
                            {
                                row.ConstantItem(110)
                                   .AlignMiddle()
                                   .AlignRight()
                                   .Background(Colors.White)
                                   .Border(1)
                                   .BorderColor("#F8BBD0")
                                   .Padding(8)
                                   .Image(qrBytes);
                            }
                        });
                    });

                    page.Content().PaddingVertical(16).Column(col =>
                    {
                        col.Spacing(14);

                        col.Item().Background("#FFF1F5").Border(1).BorderColor("#F8BBD0").Padding(12).Column(c =>
                        {
                            c.Item().Text("Datos del cliente")
                                .Bold()
                                .FontSize(13)
                                .FontColor("#C2185B");

                            c.Item().PaddingTop(4).Text("Cliente: " + nombreCliente).FontSize(11);
                            c.Item().Text("Cédula: " + cedulaCliente).FontSize(11);
                            c.Item().Text("Tipo de pago: " + tipoPago).FontSize(11);
                        });

                        col.Item().Text("Detalle de factura")
                            .Bold()
                            .FontSize(13)
                            .FontColor("#C2185B");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(1);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).Text("Producto").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).AlignCenter().Text("Cantidad").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).AlignRight().Text("Precio").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).AlignRight().Text("Subtotal").Bold().FontColor("#5A2A42");
                            });

                            foreach (var item in detalles)
                            {
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).Text(item.IdProducto); ;
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).AlignCenter().Text(item.Cantidad.ToString());
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).AlignRight().Text(item.Precio.ToString("N2", CultureInfo.InvariantCulture));
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).AlignRight().Text(item.Subtotal.ToString("N2", CultureInfo.InvariantCulture));
                            }
                        });

                        col.Item().AlignRight().Width(250).Background("#FFF1F5").Border(1).BorderColor("#F8BBD0").Padding(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            AgregarFilaTotal(table, "SubTotal:", factura.SubTotal.ToString("N2", CultureInfo.InvariantCulture), false);
                            AgregarFilaTotal(table, "Impuesto:", factura.Impuesto.ToString("N2", CultureInfo.InvariantCulture), false);
                            AgregarFilaTotal(table, "Total Colones:", factura.TotalColones.ToString("N2", CultureInfo.InvariantCulture), true);
                            AgregarFilaTotal(table, "Total Dólares:", factura.TotalDolares.ToString("N2", CultureInfo.InvariantCulture), true);
                        });

                        col.Item().PaddingTop(8).Background("#FFF1F5").Border(1).BorderColor("#F8BBD0").Padding(12).Column(c =>
                        {
                            c.Item().Text("Firma del cliente")
                                .Bold()
                                .FontSize(13)
                                .FontColor("#C2185B");

                            c.Item().PaddingTop(6);

                            if (firmaBytes != null && firmaBytes.Length > 0)
                            {
                                c.Item().Height(85).Image(firmaBytes);
                            }
                            else
                            {
                                c.Item().Height(50).AlignMiddle().AlignCenter().Text("Sin firma registrada")
                                    .Italic()
                                    .FontColor("#8A6C79");
                            }
                        });
                    });

                    page.Footer().PaddingTop(8).Column(col =>
                    {
                        col.Item().LineHorizontal(1).LineColor("#F8BBD0");
                        col.Item().PaddingTop(5).AlignCenter().Text("Gracias por su compra en CatTech")
                            .FontSize(9)
                            .FontColor("#A64D79");
                    });
                });
            }).GeneratePdf(rutaPdf);

            return rutaPdf;
        }

        private static void AgregarFilaTotal(TableDescriptor table, string titulo, string valor, bool negrita)
        {
            if (negrita)
            {
                table.Cell().Padding(4).Text(titulo).Bold().FontColor("#C2185B");
                table.Cell().Padding(4).AlignRight().Text(valor).Bold().FontColor("#C2185B");
            }
            else
            {
                table.Cell().Padding(4).Text(titulo);
                table.Cell().Padding(4).AlignRight().Text(valor);
            }
        }

        private static byte[] ConvertirImagenAPng(System.Drawing.Image imagen)
        {
            if (imagen == null)
                return null;

            using (var ms = new MemoryStream())
            {
                imagen.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}