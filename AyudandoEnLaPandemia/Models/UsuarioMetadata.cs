using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AyudandoEnLaPandemia.Models
{
    public class UsuarioMetadata
    {

        public int id
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese su nombre de usuario!")]
        [StringLength(20, ErrorMessage = "No debe superar los 20 caracteres!!")]
        [ExisteUsuario(ErrorMessage = "Ya existe ese usuario!!")]
        public string NombreUsuario { get; set; }

        [Required(ErrorMessage = "¡Ingrese su nombre!")]
        [StringLength(20, ErrorMessage = "No debe superar los 20 caracteres!!")]
        public string Nombre
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese su apellido!")]
        [StringLength(20, ErrorMessage = "No debe superar los 20 caracteres!!")]
        public string Apellido
        {
            get; set;
        }

       /// [Range(1, 100, ErrorMessage = "La edad debe estar en el rango 1 a 100")]
        public int Edad
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese su dni!")]
       
        public int Dni
        {
            get; set;
        }

        [Required(ErrorMessage = "¡Ingrese una contraseña!")]
        [RegularExpression("[A-Z]{4}", ErrorMessage = "Debe contener 4 caracteres, solo letras")]
        public string Password
        {
            get; set;
        }

        
     

    }

    public class ExisteUsuario : ValidationAttribute
    {
        List<string> MisUsuarios = new List<string>() { "Agus84", "Mica22", "Sofi" };
        bool resultado = false;
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                resultado = MisUsuarios.Any(c => c.Contains(value.ToString()));
            }
            

            return !resultado;
        }

    }
}