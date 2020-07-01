using TPIntegrador_PWeb3.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPIntegrador_PWeb3.DAL
{
    public class NecesidadRepositorio :RepositorioBase<Necesidades>
    {

        
        public Necesidades ObtenerPorId(int? id)
        {
            if (id != null)
            {
                int id1 = (int)id;
                return dbSet.Find(id1);
            }
            else return null;


        }
    }
}