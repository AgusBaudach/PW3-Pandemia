
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL;

namespace TPIntegrador_PWeb3.Servicios
{
        public class ServicioNecesidad : NecesidadRepositorio
        {
            private static List<Necesidades> lista = new List<Necesidades>();

           

            

        public List<Necesidades> listar()
        {
            List<Necesidades> lista = ObtenerTodos();
            return lista;
        }

        public List<Necesidades> listarPorUsuario(int idUsuario)
        {


            List<Necesidades> lista = ObtenerTodos();
            List<Necesidades> listaUsuario = lista.FindAll(n => n.IdUsuarioCreador.Equals(idUsuario));
           
            return listaUsuario;

        }



        public IEnumerable<Necesidades> ranking(List<Necesidades> necesidades)
            {
                if(necesidades.Count < 5) { 
            
                necesidades.OrderBy(n => n.Valoracion);
                
                return necesidades;
            }else { necesidades.OrderBy(n => n.Valoracion);
                
                necesidades.RemoveAt(4);
                return necesidades;
        }}
          

            public void crearNecesidad(Necesidades necesidad)
            {

                
                this.Crear(necesidad);


            }

          

            public void eliminarNecesidad(int id)
            {

               


            }




        }
    }


