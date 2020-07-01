using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TPIntegrador_PWeb3.DAL.Partials
{
    public class UsuariosMetadata
    {

        [Required(ErrorMessage = "¡Ingrese su email!")]
        [EmailAddress()]
        public string Email
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese su fecha de nacimiento!")]
        [CustomValidation(typeof(UsuariosMetadata), "ValidarFechas")]
        public DateTime FechaNacimiento
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese una contraseña!")]
        [RegularExpression("(([0-9A-Z]+){8,})", ErrorMessage = "Debe contener al menos una letra mayuscula y un numero")]
        public static string Password
        {
            get; set;
        }

        //[Compare("Password",ErrorMessage = "Las contraseñas deben coincidir")]
        public static string PasswordVerificacion
        {
            get; set;
        }

        public static ValidationResult ValidarFechas(object value, ValidationContext context)
        {
            var usuario = context.ObjectInstance as Usuarios;
            DateTime actual = DateTime.Now;
            
            if(usuario.FechaNacimiento != null){ 

            if ((actual.Year - usuario.FechaNacimiento.Value.Year ) < 18)
            {
                return new ValidationResult(string.Format("El usuario debe ser mayor a 18 años"));
                }
                else { return ValidationResult.Success; }
            }
            else { return ValidationResult.Success; }
        }
    }
}
