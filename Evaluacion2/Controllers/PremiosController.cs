using Evaluacion2.DAL;
using Evaluacion2.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Evaluacion2.Controllers
{
    public class PremiosController : Controller
    {
        // GET: Premios
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListaPremios()
        {
            ServicioPremios servicio = new ServicioPremios();
            List<CantidadPremiosPorCompetidor> premios = servicio.ObtenerTodos();

            return View(premios);
        }

        public ActionResult AltaPremios()
        {
            ServicioCompetidores servicio = new ServicioCompetidores();
            List<Competidor> lista = servicio.ObtenerTodos();
            ViewBag.competidores = lista;
            CantidadPremiosPorCompetidor premios = new CantidadPremiosPorCompetidor();

            return View(premios);
        }

        [HttpPost]
        public ActionResult GuardarPremio(CantidadPremiosPorCompetidor premio)
        {
            ServicioPremios servicio = new ServicioPremios();
            if (ModelState.IsValid)
            {
                servicio.guardarPremio(premio);
                return RedirectToAction("ListaPremios");
            }

            else
            {
                ServicioCompetidores servicioC = new ServicioCompetidores();
                List<Competidor> lista = servicioC.ObtenerTodos();
                ViewBag.competidores = lista;
                return View("AltaPremios");


            }
        }
        
    }
}