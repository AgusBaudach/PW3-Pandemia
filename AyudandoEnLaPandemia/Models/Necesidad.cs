using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AyudandoEnLaPandemia.Models
{

    [MetadataType(typeof(NecesidadMetadata))]
    public class Necesidad
    {

       
        public int id { get; set; }
        public int UsuarioCreadorId { get; set; }
        public int ValoracionActual { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Detalle { get; set; }
        public string FotoPrincipal { get; set; }

       // public string NombreSignificativoImagen { get; set; }
      /* {
          //  get
            {
              //  return string.Format("{0}{1}", this.Apellido ?? "Apellido", this.Nombre ?? "Nombre");
            }
       // }*/

    }
}