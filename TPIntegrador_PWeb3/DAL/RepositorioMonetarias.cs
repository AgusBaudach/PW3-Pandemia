using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TPIntegrador_PWeb3.DAL
{
    public class RepositorioMonetarias : RepositorioBase<NecesidadesDonacionesMonetarias>
    {

        public NecesidadesDonacionesMonetarias obtenerPorNecesidad(int idNecesidad)
        {
            List<NecesidadesDonacionesMonetarias> todas = this.ObtenerTodos();
            NecesidadesDonacionesMonetarias monetaria = new NecesidadesDonacionesMonetarias();
            monetaria = null;
            foreach (NecesidadesDonacionesMonetarias ndi in todas)
            {
                if (ndi.IdNecesidad == idNecesidad)
                {
                    monetaria=ndi;
                }
            }
            return monetaria;
        }
    }
}