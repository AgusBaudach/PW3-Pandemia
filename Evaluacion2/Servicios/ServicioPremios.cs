using Evaluacion2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Evaluacion2.Servicios
{
    public class ServicioPremios : RepositorioBase<CantidadPremiosPorCompetidor>
    {
        public void guardarPremio(CantidadPremiosPorCompetidor premio)
        {
            List<CantidadPremiosPorCompetidor> todos = this.ObtenerTodos();
            CantidadPremiosPorCompetidor CompetidorAño = new CantidadPremiosPorCompetidor();
            foreach (CantidadPremiosPorCompetidor c in todos) { 
            if(c.Año == premio.Año && c.idCompetidor == premio.idCompetidor)
                {
                    CompetidorAño = c;
                }
            }

            if (CompetidorAño.idCompetidor== premio.idCompetidor)
            {
                int? cantidadVieja = CompetidorAño.CantidadPremios;
                int? cantidadNueva = premio.CantidadPremios;
                int? actualizada = cantidadNueva + cantidadVieja;

                CompetidorAño.CantidadPremios = actualizada;
                this.Modificar(CompetidorAño);
            }
            else
            {
                this.Crear(premio);
                
            }
        }
    }
}