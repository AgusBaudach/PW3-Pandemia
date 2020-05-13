using AyudandoEnLaPandemia.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;


namespace AyudandoEnLaPandemia.Models
{

    [MetadataType(typeof(UsuarioMetadata))]
    public partial class Usuario { 
        public int id
    {
        get; set;
    }

    public string NombreUsuario { get; set; }
    public string Nombre
    {
        get; set;
    }

    public string Apellido
    {
        get; set;
    }

    public int Dni
    {
        get; set;
    }

        public int Edad
        {
            get; set;
        }

        public string Password
    {
        get; set;
    }
    

    }
}
