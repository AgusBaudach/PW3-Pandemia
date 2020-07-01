using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using Historial.DAL;

namespace Historial.DonacionesDTO
{
    [DataContract]
    public class NecesidadesDTO
    {
        [DataMember]
        public int IdNecesidad { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public System.DateTime FechaCreacion { get; set; }
        [DataMember]
        public System.DateTime FechaFin { get; set; }
        [DataMember]
        public string TelefonoContacto { get; set; }
        [DataMember]
        public int TipoDonacion { get; set; }
        [DataMember]
        public string Foto { get; set; }
        [DataMember]
        public int IdUsuarioCreador { get; set; }
        [DataMember]
        public int Estado { get; set; }
        [DataMember]
        public Nullable<decimal> Valoracion { get; set; }

        public NecesidadesDTO(Necesidades necesidad)
        {
            this.Descripcion = necesidad.Descripcion;
            this.TelefonoContacto = necesidad.TelefonoContacto;
        }

    }
}