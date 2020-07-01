using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPIntegrador_PWeb3.DAL;
using TPIntegrador_PWeb3.Servicios;

namespace TPIntegrador_PWeb3.Controllers
{
    public class NecesidadesController : Controller
    {
        // GET: Necesidades
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult modificarNecesidad(int? id)
        {
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad=sn.ObtenerPorId(id);
            return View(necesidad);
        }

        [HttpPost]
        public ActionResult modificarNecesidadPost([Bind(Include = "IdNecesidad,Nombre,Descripcion,TelefonoContacto")]Necesidades necesidad)
        {
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades vieja=sn.ObtenerPorId(necesidad.IdNecesidad);
            if(necesidad.Descripcion != "")
            {
                vieja.Descripcion = necesidad.Descripcion;
            }
            if (necesidad.Nombre != "")
            {
                vieja.Nombre = necesidad.Nombre;
            }

            if (necesidad.TelefonoContacto != "")
            {
                vieja.TelefonoContacto = necesidad.TelefonoContacto;
            }
           
            sn.Modificar(vieja);
            return RedirectToAction("../Usuario/HomeUsuario");
        }



        public ActionResult detallesNecesidad(int? id)
        {
          if(Helper.SessionHelper.IdNecesidad != null && id== null ) {
                 id = Helper.SessionHelper.IdNecesidad;
                Helper.SessionHelper.IdNecesidad = null;
            }
            Helper.SessionHelper.IdNecesidad = null;
            
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad = sn.ObtenerPorId(id);
            if (TPIntegrador_PWeb3.Helper.SessionHelper.Nombre != null)
            {
                Helper.SessionHelper.IdNecesidad = id;
                return View(necesidad);
            }
            else
            {
                Helper.SessionHelper.IdNecesidad = id;
                return RedirectToAction("../Usuario/ingresar");
            }
            
           
        }

        [HttpGet]
        public ActionResult crear()
        {
            Necesidades necesidad = new Necesidades();

            ServicioUsuarios su = new ServicioUsuarios();
            String password = TPIntegrador_PWeb3.Helper.SessionHelper.Nombre;
            if(password!= null) { 
            Usuarios usuario = su.traerUsuario(password);
                if(usuario.Apellido != null) { 
            ViewBag.id = usuario.IdUsuario;


            return View(necesidad);
            }else
                {
                    ViewBag.perfil = "Debe completar los siguientes campos para continuar";

                    return RedirectToAction("../Usuario/modificarUsuario");

                }
            }
            else
            {
                return RedirectToAction("../Usuario/ingresar");
            }
        }




        [HttpPost]
        public ActionResult crearNecesidad(Necesidades necesidad)
        {

            String[] parts = necesidad.Foto.Split('\\');
            int lng = parts.Length;
            int l = (lng - 1);
            string pathRelativo = parts[l];


            necesidad.Foto = pathRelativo;
            DateTime fechaCreacion = DateTime.Now;
            necesidad.FechaCreacion = fechaCreacion;
            ServicioNecesidad sn = new ServicioNecesidad();
            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios creador = su.ObtenerPorId(necesidad.IdUsuarioCreador);

            List<Necesidades> necesidadesUsuario = sn.listarPorUsuario(necesidad.IdUsuarioCreador);
            List<Necesidades> activas = necesidadesUsuario.FindAll(n => n.Estado == 1);
            
           
            if (activas.Count <= 3)
            {

                sn.crearNecesidad(necesidad);

                List<Necesidades> lista = sn.ObtenerTodos();
                Necesidades nref = new Necesidades();

                foreach (Necesidades n in lista)
                {

                    if (n.Equals(necesidad))
                    {
                        nref = n;
                    }
                }

                NecesidadesReferencias referencia = new NecesidadesReferencias();

                referencia.IdNecesidad = nref.IdNecesidad;
                referencia.Nombre = Request["nombreRef"];
                referencia.Telefono = Request["telefonoRef"];
                

                ServicioNecesidadesReferencia snr = new ServicioNecesidadesReferencia();

                snr.Crear(referencia);

                NecesidadesReferencias referencia2 = new NecesidadesReferencias();

                referencia2.IdNecesidad = nref.IdNecesidad;
                referencia2.Nombre = Request["nombreRef2"];
                referencia2.Telefono = Request["telefonoRef2"];


                

                snr.Crear(referencia2);

                NecesidadesDonacionesInsumos insumos = new NecesidadesDonacionesInsumos();
                insumos.IdNecesidad = nref.IdNecesidad;
                string CantidadInsumos = Request["cantidadInsumos"];
                if(CantidadInsumos != "") { 
                insumos.Cantidad = int.Parse(CantidadInsumos);
                
                insumos.Nombre = Request["nombreInsumos"];

                ServicioNecesidadesInsumos sni = new ServicioNecesidadesInsumos();
                sni.Crear(insumos);
                }

                NecesidadesDonacionesMonetarias monetarias = new NecesidadesDonacionesMonetarias();
                monetarias.IdNecesidad = nref.IdNecesidad;
                string dinero = Request["dinero"];
                string cbu = Request["cbu"];
                if(dinero != "") { 
                Decimal dinerod = Decimal.Parse(dinero);
               


                monetarias.Dinero = dinerod;
                
                monetarias.CBU = cbu;

                ServicioNecesidadesMonetarias snm = new ServicioNecesidadesMonetarias();
                snm.Crear(monetarias);
                }

                ViewBag.id = necesidad.IdUsuarioCreador;
                ViewBag.creada = "La necesidad fue creada.¿Desea crear otra?";
                return View("crear", necesidad);
            }
            else if (activas.Count > 3 )
            {
                ViewBag.exceso = "Usted tiene mas de tres necesidades activas";
                return View("crear", necesidad);
            }

            ViewBag.error = "Error. Reintente";
            return View("crear", necesidad);


        }

        [HttpPost]
        public ActionResult valorar(FormCollection form)
        {

            string megusta = form["megusta"];
            string nomegusta = form["nomegusta"];
            string idNecesidad = form["idNecesidad"];
            int id = int.Parse(idNecesidad);
           

            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad = sn.ObtenerPorId(id);
            Decimal? valoracion = necesidad.Valoracion;
            Decimal? nuevaValoracion = (Decimal?)00.00;
            if (megusta != "")
            {
               
                Decimal? suma = (Decimal?)10.00;
                nuevaValoracion = valoracion + suma;
            }
            else if (nomegusta != "")
            {
                Decimal? resta = (Decimal?)10.0;
                nuevaValoracion = valoracion - resta;
            }

            necesidad.Valoracion = nuevaValoracion;
            sn.Modificar(necesidad);
            Helper.SessionHelper.IdNecesidad = id;
            return RedirectToAction("detallesNecesidad");
        }

        public ActionResult donar()
        {
            string id = Request["idNecesidad"];
            int idNecesidad = int.Parse(id);
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad = sn.ObtenerPorId(idNecesidad);

            ServicioNecesidadesInsumos sni = new ServicioNecesidadesInsumos();
            NecesidadesDonacionesInsumos insumo = sni.obtenerPorNecesidad(idNecesidad);

            ServicioNecesidadesMonetarias snm = new ServicioNecesidadesMonetarias();
            NecesidadesDonacionesMonetarias monetaria = snm.obtenerPorNecesidad(idNecesidad);

            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios usuario = su.traerUsuario(Helper.SessionHelper.Nombre);
            int idUsuario = usuario.IdUsuario;
            
        

            DonacionesInsumos donacionInsumos = new DonacionesInsumos();
            DonacionesMonetarias donacionMonetaria = new DonacionesMonetarias();

            if (insumo != null)
            {
               ViewBag.IdInsumo=insumo.IdNecesidadDonacionInsumo;
                
                ViewBag.IdUsuario = idUsuario;
                return View(donacionInsumos);
            }
            else if (monetaria != null)
            {
                ViewBag.IdMonetarias = monetaria.IdNecesidadDonacionMonetaria;
                ViewBag.IdUsuario = idUsuario;
                return View("donarMonetaria", donacionMonetaria);
            }

            return View();
        }

        [HttpPost]
        public ActionResult donarInsumos(DonacionesInsumos donacion)
        {
            ServicioDonacionesInsumos sdi = new ServicioDonacionesInsumos();
            sdi.Crear(donacion);

           
            return RedirectToAction("detallesNecesidad");
        }


        [HttpPost]
        public ActionResult donarMonetarias(DonacionesMonetarias donacion)
        {
            DateTime fecha = DateTime.Now;
            donacion.FechaCreacion = fecha;

            ServicioDonacionesMonetarias sdm = new ServicioDonacionesMonetarias();
            sdm.Crear(donacion);

            return RedirectToAction("detallesNecesidad");


        }

        [HttpPost]
        public ActionResult buscar(FormCollection form)
        {
            string nombre = form["necesidad"];
            string activas = form["activas"];
            ServicioNecesidad sn = new ServicioNecesidad();
            List<Necesidades> necesidades1 = sn.ObtenerTodos();
            List<Necesidades> necesidades = necesidades1.FindAll(a => a.Estado != 0);
            String pass = Helper.SessionHelper.Nombre;
            if(pass!= null) { 
            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios us = su.traerUsuario(pass);
            List<Necesidades> misNecesidades = necesidades.FindAll(n => n.IdUsuarioCreador != us.IdUsuario);

            List<Necesidades> necesidadesNombre = misNecesidades.FindAll(o => o.Nombre == nombre);
                
               necesidadesNombre.OrderByDescending(n => n.FechaFin);
            

            if (activas != null)
            {
                List<Necesidades> activasList = necesidadesNombre.FindAll(o => o.Estado == 1);
                return View("listar", activasList);
            }
            else
            {
                return View("listar",necesidadesNombre);
            }
            }
            else
            {
                return RedirectToAction("../Usuario/ingresar");
            }
        }
    }
}
