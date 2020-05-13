using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AyudandoEnLaPandemia.Entidades
{
    public class Persona
    {

        public int id
        {
            get; set;
        }
        
        public string nombreUsuario{get;set;}
        public string nombre
        {
            get; set;
        }

        public string apellido
        {
            get; set;
        }

        public int dni
        {
            get; set;
        }

        public string password
        {
            get; set;
        }
    }
}