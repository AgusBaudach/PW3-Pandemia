using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL;

namespace TPIntegrador_PWeb3.Servicios
{
    public class ServicioValoraciones : ValoracionesRepositorio
    {
        public List<NecesidadesValoraciones> obtenerPorIdNecesidad(int id)
        {
            List<NecesidadesValoraciones> todas = this.ObtenerTodos();
            List<NecesidadesValoraciones> porId=todas.FindAll(v => v.IdNecesidad == id);

            return porId;
        
        }
    }
}