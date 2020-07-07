using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evaluacion2.DAL.Partials
{
    public class CantidadPremiosPorCompetidorMetadata
    {
        [Required(ErrorMessage = "¡Ingrese un valor!")]
        [Range(1, 10, ErrorMessage = "Debe ingresar entre uno y diez premios!!")]
        public Nullable<int> CantidadPremios { get; set; }

        [Required(ErrorMessage = "¡Ingrese un año!")]
        [CustomValidation(typeof(CantidadPremiosPorCompetidorMetadata), "ValidarAño")]
        public Nullable<int> Año { get; set; }

        [Required(ErrorMessage = "¡Ingrese un competidor")]
        public Nullable<int> idCompetidor { get; set; }


        public static ValidationResult ValidarAño(object value, ValidationContext context)
        {
            var premio = context.ObjectInstance as CantidadPremiosPorCompetidor;

            if (premio.Año <= 2010)
            {
                return new ValidationResult(string.Format("El año debe ser mayor a 2010"));
            }
            else { return ValidationResult.Success; }
        }
    }
}