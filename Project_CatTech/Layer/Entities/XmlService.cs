using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Project_CatTech.Layer.Entities
{
    public class XmlService
    {
        public static XElement GenerarXML(Factura factura, List<FacturaDetalle> detalle)
        {
            XElement xml = new XElement("Factura",
                new XElement("Encabezado",
                    new XElement("NumeroFactura", factura.NumeroFactura),
                    new XElement("Fecha", factura.Fecha.ToString("yyyy-MM-dd HH:mm:ss")),
                    new XElement("IdCliente", factura.IdCliente),
                    new XElement("IdUsuario", factura.IdUsuario),
                    new XElement("SubTotal", factura.SubTotal),
                    new XElement("Impuesto", factura.Impuesto),
                    new XElement("TotalColones", factura.TotalColones),
                    new XElement("TotalDolares", factura.TotalDolares)
                ),
                new XElement("Detalle",
                    detalle.Select(d =>
                        new XElement("Linea",
                            new XElement("IdProducto", d.IdProducto),
                            new XElement("Descripcion", d.IdDetalle),
                            new XElement("Cantidad", d.Cantidad),
                            new XElement("PrecioUnitario", d.Precio),
                            new XElement("Subtotal", d.Subtotal)
                        )
                    )
                )
            );
            return xml;
        }
    }
}
