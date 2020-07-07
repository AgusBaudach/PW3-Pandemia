using Evaluacion2.DAL;
using Evaluacion2.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluacion2.Controllers
{
    public class CompetidoresController : Controller
    {
        // GET: Competidores
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaCompetidores()
        {
            ServicioCompetidores servicio = new ServicioCompetidores();
            List<Competidor> competidores = servicio.ObtenerTodos();

            return View(competidores);
        }

        public ActionResult AltaCompetidor()
        {
            Competidor competidor = new Competidor();
            return View(competidor);
        }

        [HttpPost]
        public ActionResult GuardarCompetidor(Competidor competidor)
        {
            ServicioCompetidores sc = new ServicioCompetidores();

            if (ModelState.IsValid)
            {
                sc.Crear(competidor);
                return RedirectToAction("ListaCompetidores");
            }
            else
            {
                return View("AltaCompetidor");
            }
          
        }
    }
}