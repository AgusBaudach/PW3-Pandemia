using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AyudandoEnLaPandemia.Models;

namespace AyudandoEnLaPandemia.Servicios
{
    public class ServicioNecesidad
    {
        private static List<Necesidad> lista = new List<Necesidad>();

        public List<Necesidad> listar()
        {
            return lista;
        }

        public List<Necesidad> ranking(List<Necesidad> necesidades)
        {
            
            
          necesidades.OrderBy(n=>n.ValoracionActual);

            return necesidades;
        }


        public void crearNecesidad(Necesidad necesidad)
        {
            int idMax = 0;

            if(lista.Count > 0)
            {
                idMax = lista.Max(n => n.id);
            }

            necesidad.id=idMax + 1;

            lista.Add(necesidad);


        }

       
    }
}