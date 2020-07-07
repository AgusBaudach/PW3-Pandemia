
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using TPIntegrador_PWeb3.DAL;
using TPIntegrador_PWeb3.Servicios;

namespace TPIntegrador_PWeb3.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ServicioNecesidad servicio = new ServicioNecesidad();
            IEnumerable<Necesidades> necesidades = servicio.ranking(servicio.ObtenerTodos()) ;
            
            
          

            return View(necesidades);
        }

      /*  private List<NecesidadesDTO> GetHistorial()
        {
            var url = $"https://localhost:44330/api/HistorialApi";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return new List<NecesidadesDTO>();
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var result = JsonConvert.DeserializeObject<List<NecesidadesDTO>>(responseBody);
                        return result;
                    }
                }
            }
        }*/

        public ActionResult Menu()
        {
            

                return View();
            
        }
    }
}