using AyudandoEnLaPandemia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.Servicios;


namespace AyudandoEnLaPandemia.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult HomeNecesidad()
        {
            ServicioNecesidad n = new ServicioNecesidad();
            List<Necesidad> list = n.listar();
            List<Necesidad> necesidades = n.ranking(list);


            return View("Index", necesidades);
        }

    }
}