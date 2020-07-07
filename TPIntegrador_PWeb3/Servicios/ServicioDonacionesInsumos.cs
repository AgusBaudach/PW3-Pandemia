using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL;

namespace TPIntegrador_PWeb3.Servicios
{
    public class ServicioDonacionesInsumos : RepositorioDonacionesInsumos
    {
        public List<DonacionesInsumos> verInsumos(int idUsuario)
        {

            ServicioNecesidad sn = new ServicioNecesidad();
            List<Necesidades> necesidades = sn.listarPorUsuario(idUsuario);
            ServicioNecesidadesInsumos si = new ServicioNecesidadesInsumos();
            List<DonacionesInsumos> delusuario = new List<DonacionesInsumos>();
            List<NecesidadesDonacionesInsumos> lista = si.ObtenerTodos();
            List<NecesidadesDonacionesInsumos> listaUsuario = new List<NecesidadesDonacionesInsumos>();
            foreach (Necesidades n in necesidades)
            {
                foreach (NecesidadesDonacionesInsumos ni in lista)
                {
                    if (n.IdNecesidad == ni.IdNecesidad)
                    {
                        listaUsuario.Add(ni);
                    }

                }

                List<DonacionesInsumos> insumos = this.ObtenerTodos();
                
                foreach (NecesidadesDonacionesInsumos ndi in listaUsuario)
                {
                    foreach (DonacionesInsumos i in insumos)
                    {
                        if (i.IdNecesidadDonacionInsumo == ndi.IdNecesidadDonacionInsumo)
                        {
                            delusuario.Add(i);
                        }
                    }

                }
            }return delusuario;
        }
    }
}