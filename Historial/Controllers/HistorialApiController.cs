using Historial.DAL;
using Historial.DonacionesDTO;
using Historial.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Historial.Controllers
{
    public class HistorialApiController : ApiController
    {
        public List<NecesidadesDTO> Get()
        {
            ServicioNecesidades sn = new ServicioNecesidades();
            List<Necesidades> necesidadesEF = sn.ObtenerTodos();
            List<NecesidadesDTO> necesidadesDTO = new List<NecesidadesDTO>();
            foreach (var necesidadEF in necesidadesEF)
            {
                NecesidadesDTO dto = new NecesidadesDTO(necesidadEF);
                necesidadesDTO.Add(dto);
                necesidadesDTO.OrderByDescending(o => o.FechaCreacion);
            }
            return necesidadesDTO;
        }
    }
}
