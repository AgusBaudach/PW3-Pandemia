using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Evaluacion2.DAL.Partials
{

    public class CompetidorMetadata
    {
        [Required(ErrorMessage = "¡Ingrese su nombre!")]
        [StringLength(100, ErrorMessage = "Cien caracteres como maximo!!")]
        public string nombre { get; set; }


    }
}