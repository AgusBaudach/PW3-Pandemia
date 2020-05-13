using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.Models;
using AyudandoEnLaPandemia.Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class NecesidadController : Controller
    {
        // GET: Necesidad
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult listar()
        {

            ServicioNecesidad n = new ServicioNecesidad();
            List<Necesidad> lista = n.listar();
            return View(lista);
        }

        [HttpGet]
        public ActionResult MisNecesidades()
        {


            return RedirectToAction("listar");
        }

        [HttpGet]
        public ActionResult _NecesidadRanking()
        {
            ServicioNecesidad n = new ServicioNecesidad();
            List<Necesidad> lista = n.listar();
            List<Necesidad> necesidades = n.ranking(lista);


            return View(necesidades);
        }


        [HttpPost]
        public ActionResult RegistroNeed( Necesidad necesidad)
        {
           
               
               

           
            String[] parts = necesidad.FotoPrincipal.Split('\\');
            int lng = parts.Length;
            int l = (lng - 1);
            string pathRelativo = parts[l];


            necesidad.FotoPrincipal = pathRelativo;
            

            if (ModelState.IsValid)
            {


                ServicioNecesidad n = new ServicioNecesidad();
                n.crearNecesidad(necesidad);

                return RedirectToAction("listar");
            }
            else return View(necesidad);
        }
    







    public ActionResult Necesidades()
    {


        return View();
    }

    [HttpGet]
    public ActionResult CrearNecesidad()
    {
        Necesidad necesidad = new Necesidad();

        return View(necesidad);
    }

    }
}