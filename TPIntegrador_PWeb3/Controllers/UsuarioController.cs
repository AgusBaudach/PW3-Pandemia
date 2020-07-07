using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPIntegrador_PWeb3.DAL;
using TPIntegrador_PWeb3.Servicios;

namespace TPIntegrador_PWeb3.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult historial(int? id)
        {
            int idUsuario = (int)id;
            ServicioDonacionesInsumos sdi = new ServicioDonacionesInsumos();
            List<DonacionesInsumos> donaciones=sdi.verInsumos(idUsuario);

            ServicioDonacionesMonetarias sdm = new ServicioDonacionesMonetarias();
            List<DonacionesMonetarias> monet = sdm.verMonetarias(idUsuario);
            ViewBag.monetarias = monet;

            return View(donaciones);
        }

        public ActionResult gestionarDenuncias()
        {
            ServicioUsuarios su = new ServicioUsuarios();
           Usuarios u= su.traerUsuario(Helper.SessionHelper.Nombre);
            if (u.TipoUsuario == 1) {
                ServicioDenuncia sd = new ServicioDenuncia();
                List<Denuncias> denuncias = sd.ObtenerTodos();
                List<Denuncias> sinRevisar = denuncias.FindAll(d => d.Estado == 1);
                if(sinRevisar != null) { 
            return View(sinRevisar);
            }
                else { return RedirectToAction("HomeUsuario"); }
            }
            else { return RedirectToAction("HomeUsuario"); }
        }

        [HttpPost]
        public ActionResult gestionar()
        {
            string idDenuncia = Request["idDenuncia"];
            int id = int.Parse(idDenuncia);
            string estado = Request["Estado"];
            string estadoNecesidad = Request["EstadoNecesidad"];
            if (estado != "") { 
            int estadoInt = int.Parse(estado);

            ServicioDenuncia sd = new ServicioDenuncia();
            Denuncias vieja=sd.ObtenerPorId(id);
           
            vieja.Estado = estadoInt;
            sd.Modificar(vieja);
            }
            if (estadoNecesidad != "")
            {
                ServicioDenuncia sd = new ServicioDenuncia();
                Denuncias vieja = sd.ObtenerPorId(id);
                ServicioNecesidad sn = new ServicioNecesidad();
                Necesidades n=sn.ObtenerPorId(vieja.IdNecesidad);
                n.Estado = 0;
                sn.Modificar(n);
            }

            return RedirectToAction("gestionarDenuncias");

        }
        

            public ActionResult registrarse()
        {
            Usuarios usuario = new Usuarios();
            return View(usuario);
        }

        [HttpPost]
        public ActionResult registrarUsuario([Bind(Include = "Password,Email,FechaNacimiento,TipoUsuario")]Usuarios usuario)
        {


            if (ModelState.IsValid)
            {
                ServicioUsuarios su = new ServicioUsuarios();
                usuario.Activo = true;
                su.Crear(usuario);
                su.EnviarMail(usuario);

                return View("ingresar");
            }
            else
            {
                return View("registrarse");
            }
        }

        public ActionResult activar(int Token)
        {
            ViewBag.token = Token;

            return View();
        }



        public ActionResult ingresar()
        {
           
            return View();
        }

      


        [HttpPost]
        public ActionResult login(FormCollection form)
        {
            string Email = form["Email"];
            string password = form["Password"];
           

            ServicioUsuarios su = new ServicioUsuarios();

            Usuarios usuario = su.buscarUsuario(password, Email);

             List<Usuarios> usuarios = su.ObtenerTodos();
             usuarios.Remove(usuario);

            if (usuario.Email != Email )
            {
                string msg = "Email/contraseña invalidos";
                ViewBag.msg = msg;
                return RedirectToAction("../Home/Index");
            }
            else if (usuario.Email == Email &&  usuario.Activo == true && Helper.SessionHelper.contador != 1)
            {

                TPIntegrador_PWeb3.Helper.SessionHelper.Email = Email;
                TPIntegrador_PWeb3.Helper.SessionHelper.Nombre = password;
                
               

                ViewBag.usuarios = usuarios;
                return View("HomeUsuario", usuario);
            }
            else if (usuario.Email == Email  && usuario.Activo == false)
            {
                ViewBag.activar = "Usuario Inactivo.Debe activarlo desde el mail recibido";
                return View("ingresar");
            }
            else if (usuario.Email == Email && Helper.SessionHelper.contador == 1 && usuario.Activo == true)
            {
                
                TPIntegrador_PWeb3.Helper.SessionHelper.Email = Email;
                TPIntegrador_PWeb3.Helper.SessionHelper.Nombre = password;
                
                return RedirectToAction("../Necesidades/detallesNecesidad");
            }
            else { return View("ingresar"); }

        }



        public ActionResult HomeUsuario()
        {

            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios usuario = su.traerUsuario(Helper.SessionHelper.Nombre);
            List<Usuarios> usuarios = su.ObtenerTodos();
            usuarios.Remove(usuario);
            ViewBag.usuarios = usuarios;

            return View("HomeUsuario", usuario);
        }

        
        public ActionResult necesidadesActivas()
        { 
            ServicioNecesidad sn = new ServicioNecesidad();
            List<Necesidades> necesidades = sn.ObtenerTodos();
            String pass = Helper.SessionHelper.Nombre;
            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios us = su.traerUsuario(pass);
            
            List<Necesidades> misnecesidades = sn.listarPorUsuario(us.IdUsuario);
            List<Necesidades> activas = misnecesidades.FindAll(n => n.Estado == 1);
            ViewBag.necesidadesActivas = activas;
           
            return View("HomeUsuario", us);
        }

        public ActionResult modificarUsuario()
        {
           

            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios usuario=su.traerUsuario(Helper.SessionHelper.Nombre);

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Modificar([Bind(Include = "IdUsuario,Foto,Nombre,Apellido")] Usuarios usuario)
        {
           
          
           
            string foto = usuario.Foto;
              String[] parts = foto.Split('\\');
              int lng = parts.Length;
              int l = (lng - 1);
              string pathRelativo = parts[l];


      
          
                ServicioUsuarios u = new ServicioUsuarios();
            Usuarios viejo=u.ObtenerPorId(usuario.IdUsuario);

            viejo.Foto = pathRelativo;
            viejo.Nombre = usuario.Nombre;
            viejo.Apellido = usuario.Apellido;

            u.Modificar(viejo);



                return RedirectToAction("HomeUsuario");
           



        }

        public ActionResult motivos()
        {

            MotivoDenuncia politica = new MotivoDenuncia();
            politica.Descripcion="Propaganda politica";

            MotivoDenuncia indebida = new MotivoDenuncia();
            indebida.Descripcion = "Indebida";

            MotivoDenuncia fraude = new MotivoDenuncia();
            fraude.Descripcion = "Fraude";

            MotivoDenuncia violacionDerechos = new MotivoDenuncia();
            violacionDerechos.Descripcion = "Violacion de derechos";

            ServicioMotivo sm = new ServicioMotivo();
            sm.Crear(politica);
            sm.Crear(indebida);
            sm.Crear(fraude);
            sm.Crear(violacionDerechos);

            return RedirectToAction("HomeUsuario");
        }


       

        public ActionResult denunciar()
        {
            string id = Request["idNecesidad"];
            int idNecesidad = int.Parse(id);
           

           
            ServicioMotivo sm = new ServicioMotivo();
            List<MotivoDenuncia> motivos = sm.ObtenerTodos();
            
            ViewBag.motivos = motivos;

            Denuncias denuncia = new Denuncias();
           
            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios usuario = su.traerUsuario(Helper.SessionHelper.Nombre);
            ViewBag.idUsuario = usuario.IdUsuario;
            ViewBag.idNecesidad = idNecesidad;
            return View(denuncia);
        }


        [HttpPost]
        public ActionResult denunciarNecesidad(Denuncias denuncia)
        {
            DateTime fecha = DateTime.Now;
            denuncia.FechaCreacion = fecha;
            
            ServicioDenuncia sd = new ServicioDenuncia();
            sd.Crear(denuncia);

            
         
            

            return RedirectToAction("HomeUsuario");
        }


        [HttpGet]
        public ActionResult LogOut()
        {
            Helper.SessionHelper.contador = null;
            Helper.SessionHelper.Nombre = null;
            Helper.SessionHelper.IdNecesidad = null;
            return RedirectToAction("../Home/Index");
        }
    }
}