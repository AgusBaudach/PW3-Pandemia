using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AyudandoEnLaPandemia.Models
{
    public class NecesidadMetadata
    {
        public int id { get; set; }

        [Required(ErrorMessage = "¡Ingrese su necesidad!")]
        [StringLength(20, ErrorMessage = "No debe superar los 20 caracteres!!")]
        public string Nombre { get; set; }

        public int UsuarioCreadorId { get; set; }
        public int ValoracionActual { get; set; }
       
        public DateTime FechaCreacion { get; set; }
        public string Detalle { get; set; }
        public string FotoPrincipal { get; set; }

    }
}