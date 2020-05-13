using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AyudandoEnLaPandemia.Models;
using AyudandoEnLaPandemia.Servicios;

namespace AyudandoEnLaPandemia.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
       

        public ActionResult Listar()
        {
            ServicioUsuarios su = new ServicioUsuarios();
            List<Usuario> lista = su.listar();

            return View(lista);
        }
        [HttpPost]
        public ActionResult Ingresar(FormCollection form)
        {
            string nombreUsuario = form["nombreUsuario"];
            string password = form["password"];

            ServicioUsuarios su = new ServicioUsuarios();
            Usuario usuario = su.buscarUsuario(password, nombreUsuario);

            if (usuario == null)
            {
                string msg = "Usted no es un usuario registrado";
                ViewBag.msg = msg;
                return View("Index");
            }
            else
            {

                return View("Home");
            }
        }

        [HttpGet]
        public ActionResult Registrarse()
        {
            Usuario usuario = new Usuario();
            return View("RegistrarseGet", usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegistrarseGet([Bind(Include = "NombreUsuario,Password,Nombre,Apellido,Dni,Edad")] Usuario usuario)
        {

            if (ModelState.IsValid)
            {



                ServicioUsuarios su = new ServicioUsuarios();
                su.ingresarUsuario(usuario);

                return RedirectToAction("Listar");
            }
            else return View(usuario);
        }
    }


}
