using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TPIntegrador_PWeb3.DAL.Partials;

namespace TPIntegrador_PWeb3.DAL
{
    [MetadataType(typeof(UsuariosMetadata))]
    public partial class Usuarios
    {

        public string PasswordVerificacion
        {
            get; set;
        }
    }
}