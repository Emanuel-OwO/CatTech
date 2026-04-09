using Project_CatTech.Layer.Entities;
using Project_CatTech.Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project_CatTech.Layer.BLL
{
    public class BLLPadron : IBLLPadron
    {
        public PadronElectoral GetById(string pId)
        {
            string json = "";

            // Leer del App.Config el URL con el Key URLPadron
            string URLPadron = ConfigurationManager.AppSettings["URLPadron"];

            // Concatenar el Id al URL
            string url = URLPadron + pId;


            // Creates a GET request to fetch  
            WebRequest request = WebRequest.Create(url);
            // Verb GET
            request.Method = "GET";


            // GetResponse returns a web response containing the response to the request
            using (WebResponse webResponse = request.GetResponse())
            {
                // Reading data
                StreamReader reader = new StreamReader(webResponse.GetResponseStream());
                json = reader.ReadToEnd();
            }

            // Deserializa la respuesta JSON en el DTO esperado
            PadronElectoral oPadronDTO = JsonSerializer.Deserialize<PadronElectoral>(json);

            return oPadronDTO;
        }
    }
}
