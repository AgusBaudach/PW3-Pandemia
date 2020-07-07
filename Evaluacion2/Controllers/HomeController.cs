using Evaluacion2.DAL;
using Evaluacion2.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Evaluacion2.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View("Bienvenido");
        }

       public String TotalPremios()
        {
        
            var url = $"https://localhost:44341/api/CantidadTotalPremiosApi/2000";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";



            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {

                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();

                        return responseBody;
                    }
                }
            }
        }
    }
}








