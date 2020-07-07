using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL;

namespace TPIntegrador_PWeb3.Servicios
{
    public class ServicioDonacionesMonetarias: RepositorioDonacionesMonetarias
    {
        public List<DonacionesMonetarias> verMonetarias(int idUsuario)
        {

            ServicioNecesidad sn = new ServicioNecesidad();
            List<Necesidades> necesidades = sn.listarPorUsuario(idUsuario);
            ServicioNecesidadesMonetarias si = new ServicioNecesidadesMonetarias();
            List<DonacionesMonetarias> delusuario = new List<DonacionesMonetarias>();
            List<NecesidadesDonacionesMonetarias> lista = si.ObtenerTodos();
            List<NecesidadesDonacionesMonetarias> listaUsuario = new List<NecesidadesDonacionesMonetarias>();
            foreach (Necesidades n in necesidades)
            {
                foreach (NecesidadesDonacionesMonetarias ni in lista)
                {
                    if (n.IdNecesidad == ni.IdNecesidad)
                    {
                        listaUsuario.Add(ni);
                    }

                }

                List<DonacionesMonetarias> monet = this.ObtenerTodos();

                foreach (NecesidadesDonacionesMonetarias ndi in listaUsuario)
                {
                    foreach (DonacionesMonetarias i in monet)
                    {
                        if (i.IdNecesidadDonacionMonetaria == ndi.IdNecesidadDonacionMonetaria)
                        {
                            delusuario.Add(i);
                        }
                    }

                }
            }
            return delusuario;
        }
    }
}