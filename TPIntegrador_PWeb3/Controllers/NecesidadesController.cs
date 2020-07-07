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
            int Id= (int)id;
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad=sn.ObtenerPorId(Id);
            return View(necesidad);
        }

        [HttpPost]
        public ActionResult modificarNecesidadPost([Bind(Include = "IdNecesidad,Descripcion,Nombre,TelefonoContacto")]Necesidades necesidad)
        {
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades vieja=sn.ObtenerPorId(necesidad.IdNecesidad);
            if(necesidad.Descripcion != null)
            {
                vieja.Descripcion = necesidad.Descripcion;
            }
            if (necesidad.Nombre != null)
            {
                vieja.Nombre = necesidad.Nombre;
            }

            if (necesidad.TelefonoContacto != null)
            {
                vieja.TelefonoContacto = necesidad.TelefonoContacto;
            }
            sn.Modificar(vieja);

            if(vieja.NecesidadesReferencias.Count>0) { 
            NecesidadesReferencias referencia=vieja.NecesidadesReferencias.ElementAt(0);
            
            if (referencia != null) {
            referencia.IdNecesidad = necesidad.IdNecesidad;
           
                String nombreRef = Request["nombreRef"];
            if(nombreRef != null) { 
            referencia.Nombre = nombreRef;
            
           String telefonoRef = Request["telefonoRef"];
            if (telefonoRef != null)
            {
                
                referencia.Telefono = telefonoRef;
             }


            ServicioNecesidadesReferencia snr = new ServicioNecesidadesReferencia();

            snr.Modificar(referencia);
                }
            }
            }
            else
            {
                NecesidadesReferencias referencia2 = new NecesidadesReferencias();
                
                    referencia2.IdNecesidad = necesidad.IdNecesidad;

                    String nombreRef = Request["nombreRef"];
                if (nombreRef != null)
                {
                    referencia2.Nombre = nombreRef;
                }

                        String telefonoRef = Request["telefonoRef"];
                        if (telefonoRef != null)
                        {

                            referencia2.Telefono = telefonoRef;
                        }


                        ServicioNecesidadesReferencia snr = new ServicioNecesidadesReferencia();

                        snr.Crear(referencia2);
                    }
                

           if(vieja.NecesidadesDonacionesInsumos.Count >0) { 
            
                        NecesidadesDonacionesInsumos insumos = vieja.NecesidadesDonacionesInsumos.ElementAt(0);
            
            if (insumos != null) { 
                        insumos.IdNecesidad = necesidad.IdNecesidad;
                        string CantidadInsumos = Request["cantidadInsumos"];
                        if (CantidadInsumos != "")
                        {
                            insumos.Cantidad = int.Parse(CantidadInsumos);}

                        string nombreInsumos = Request["nombreInsumos"];
                        if (nombreInsumos != "")
                            {
                                insumos.Nombre = nombreInsumos;


                        }

                        ServicioNecesidadesInsumos sni = new ServicioNecesidadesInsumos();

                        sni.Modificar(insumos);
            }
            }
            else
            {
                NecesidadesDonacionesInsumos insumos2 = new NecesidadesDonacionesInsumos();
                insumos2.IdNecesidad = necesidad.IdNecesidad;
                string CantidadInsumos = Request["cantidadInsumos"];
                if (CantidadInsumos != "")
                {
                    insumos2.Cantidad = int.Parse(CantidadInsumos);
                }

                string nombreInsumos = Request["nombreInsumos"];
                if (nombreInsumos != "")
                {
                    insumos2.Nombre = nombreInsumos;


                }

                ServicioNecesidadesInsumos sni = new ServicioNecesidadesInsumos();

                sni.Crear(insumos2);
            }

            if (vieja.NecesidadesDonacionesMonetarias.Count > 0)
            {

                NecesidadesDonacionesMonetarias monetarias = vieja.NecesidadesDonacionesMonetarias.ElementAt(0);

                if (monetarias != null)
                {
                    monetarias.IdNecesidad = necesidad.IdNecesidad;
                    string Cantidaddin = Request["dinero"];
                    if (Cantidaddin != "")
                    {
                        monetarias.Dinero = Decimal.Parse(Cantidaddin);

                    }

                    string cbu = Request["cbu"];
                    if (cbu != "")
                    {
                        monetarias.CBU = cbu;


                    }

                    ServicioNecesidadesMonetarias snm = new ServicioNecesidadesMonetarias();

                    snm.Modificar(monetarias);
                }
            }
            else
            {
                NecesidadesDonacionesMonetarias mon2 = new NecesidadesDonacionesMonetarias();
                mon2.IdNecesidad = necesidad.IdNecesidad;
                string dinero = Request["dinero"];
                if (dinero != "")
                {
                    mon2.Dinero = Decimal.Parse(dinero);
                }

                string cbu = Request["cbu"];
                if (cbu != "")
                {
                    mon2.CBU = cbu;


                }

                ServicioNecesidadesMonetarias snm = new ServicioNecesidadesMonetarias();

                snm.Crear(mon2);
            }

            return RedirectToAction("../Usuario/HomeUsuario");
        }



        public ActionResult detallesNecesidad(int? id)
        {
          if(id==null) {
                 id = Helper.SessionHelper.IdNecesidad;
                
            }
            
            
            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad = sn.ObtenerPorId(id);
            if (TPIntegrador_PWeb3.Helper.SessionHelper.Nombre != null)
            {
                Helper.SessionHelper.contador = null;
                Helper.SessionHelper.IdNecesidad = null;
                return View(necesidad);
            }
            else
            {
                Helper.SessionHelper.contador = 1;
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
            string pass = Helper.SessionHelper.Nombre;
            ServicioUsuarios su = new ServicioUsuarios();
            Usuarios usuario = su.traerUsuario(pass);

            ServicioNecesidad sn = new ServicioNecesidad();
            Necesidades necesidad = sn.ObtenerPorId(id);
            ServicioValoraciones sv = new ServicioValoraciones();
            List<NecesidadesValoraciones> porNecesidad = sv.obtenerPorIdNecesidad(id);
            NecesidadesValoraciones valoracion = new NecesidadesValoraciones();
            if (porNecesidad.Count == 0)
            {
                
                if (megusta != "")
                {

                    valoracion.MeGusta = true;
                    valoracion.Valoracion = 10;
                    valoracion.IdNecesidad = id;
                    valoracion.IdUsuario = usuario.IdUsuario;
                    sv.Crear(valoracion);
                }
                else if (nomegusta != "")
                {
                    valoracion.MeGusta = false;
                    valoracion.Valoracion = 0;
                    valoracion.IdNecesidad = id;
                    valoracion.IdUsuario = usuario.IdUsuario;
                    sv.Crear(valoracion);
                }
            }
            else if(porNecesidad.Count != 0)
            {
                if (megusta != "")
                {
                    NecesidadesValoraciones nv = porNecesidad.First();
                    int promedio = (int)(nv.Valoracion + 10) / (porNecesidad.Count()+1);
                    valoracion.MeGusta = true;
                    valoracion.Valoracion = promedio;
                    valoracion.IdNecesidad = id;
                    valoracion.IdUsuario = usuario.IdUsuario;
                    sv.Crear(valoracion);
                }
                else if (nomegusta != "")
                {
                    NecesidadesValoraciones nv = porNecesidad.First();
                    int promedio = (int)(nv.Valoracion - 10) / (porNecesidad.Count() + 1);
                    valoracion.MeGusta = false;
                    valoracion.Valoracion = promedio;
                    valoracion.IdNecesidad = id;
                    valoracion.IdUsuario = usuario.IdUsuario;
                    sv.Crear(valoracion);
                }
           
           
            
           
            }
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

        public ActionResult listar()
        {
            ServicioNecesidad sn = new ServicioNecesidad();
            List<Necesidades> necesidades1 = sn.ObtenerTodos();
            List<Necesidades> necesidades = necesidades1.FindAll(a => a.Estado != 0);
            return View( necesidades);
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
