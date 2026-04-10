using Project_CatTech.Layer.Entities.BCR;
using Project_CatTech.Layer.Interfaces.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLDolar : IBLLDolar
    {
        public double GetVentaDolar()
        {
			try
			{
                double tipoCambioVenta = 1;
                ServicesBCCR services = new ServicesBCCR();
                List<Dolar> cambioDolar = services.GetDolar(DateTime.Today, DateTime.Today, "v").ToList();
                if (cambioDolar != null)
                {
                    tipoCambioVenta = cambioDolar[0].Monto;
                }
                return tipoCambioVenta;

            }
            catch (Exception ex)
			{
                String erro = ex.Message;   
				return 0;
			}
        }
    }
}
