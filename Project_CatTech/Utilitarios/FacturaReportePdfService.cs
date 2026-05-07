using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_CatTech.Layer.DTO;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Project_CatTech.Utilitarios
{
    public  class FacturaReportePdfService
    {
        public void GenerarPdf(List<FacturaReporteDTO> lista, decimal total, DateTime fechaInicial, DateTime fechaFinal)
        {
            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "ReporteFacturas.pdf"
            );

            var pdfBytes = Document.Create(documento =>
            {
                documento.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(28);
                    page.PageColor("#FFFDFE");
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor("#3F3A3D"));

                    page.Header().Column(col =>
                    {
                        col.Item().Background("#FCE4EC").Padding(18).Column(c =>
                        {
                            c.Item().Text("CatTech")
                                .FontSize(24)
                                .Bold()
                                .FontColor("#D63384");

                            c.Item().Text("Reporte de Facturas")
                                .FontSize(15)
                                .SemiBold()
                                .FontColor("#7A4E64");

                            c.Item().PaddingTop(8).Text("Fecha inicial: " + fechaInicial.ToString("dd/MM/yyyy"));
                            c.Item().Text("Fecha final: " + fechaFinal.ToString("dd/MM/yyyy"));
                            c.Item().Text("Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        });
                    });

                    page.Content().PaddingVertical(16).Column(col =>
                    {
                        col.Spacing(14);

                        col.Item().Text("Facturas encontradas")
                            .Bold()
                            .FontSize(13)
                            .FontColor("#C2185B");

                        col.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2); // factura
                                columns.RelativeColumn(2); // fecha
                                columns.RelativeColumn(3); // cliente
                                columns.RelativeColumn(2); // total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).Text("Factura").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).Text("Fecha").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).Text("Cliente").Bold().FontColor("#5A2A42");
                                header.Cell().Background("#F8BBD0").BorderBottom(1).BorderColor("#E91E63").Padding(7).AlignRight().Text("Total").Bold().FontColor("#5A2A42");
                            });

                            foreach (var item in lista)
                            {
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).Text(item.NumeroFactura);
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).Text(item.Fecha.ToString("dd/MM/yyyy"));
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).Text(item.Cliente);
                                table.Cell().BorderBottom(1).BorderColor("#F3D6E1").Padding(7).AlignRight().Text(item.TotalColones.ToString("N2", CultureInfo.InvariantCulture));
                            }
                        });

                        col.Item().AlignRight().Width(250).Background("#FFF1F5").Border(1).BorderColor("#F8BBD0").Padding(10).Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Padding(4).Text("Cantidad de facturas:").Bold().FontColor("#C2185B");
                            table.Cell().Padding(4).AlignRight().Text(lista.Count.ToString()).Bold().FontColor("#C2185B");

                            table.Cell().Padding(4).Text("Total facturado:").Bold().FontColor("#C2185B");
                            table.Cell().Padding(4).AlignRight().Text(total.ToString("N2", CultureInfo.InvariantCulture)).Bold().FontColor("#C2185B");
                        });
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
    }
}

