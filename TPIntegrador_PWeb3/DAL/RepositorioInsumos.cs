using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPIntegrador_PWeb3.DAL
{
    public class RepositorioInsumos : RepositorioBase<NecesidadesDonacionesInsumos>
    {
        public NecesidadesDonacionesInsumos obtenerPorNecesidad(int idNecesidad)
        {
            List<NecesidadesDonacionesInsumos> todas = this.ObtenerTodos();
            NecesidadesDonacionesInsumos insumo = new NecesidadesDonacionesInsumos();
            insumo = null;
            foreach(NecesidadesDonacionesInsumos ndi in todas)
            {
                if(ndi.IdNecesidad == idNecesidad)
                {
                    insumo=ndi;
                }
            } 
            
            return insumo;
            
        }
    }
}