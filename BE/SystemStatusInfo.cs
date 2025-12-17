using System;
using System.Collections.Generic;

namespace BE
{
    public class SystemStatusInfo
    {
        public bool BaseDatosConectada { get; set; }
        public DateTime HoraLocal { get; set; }
        public DateTime? HoraServidorUtc { get; set; }
        public string IdiomaActual { get; set; }
        public string VersionAplicacion { get; set; }
        public List<PublicActivityItem> ActividadPublica { get; set; } = new List<PublicActivityItem>();
    }

    public class PublicActivityItem
    {
        public DateTime FechaLocal { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
    }
}
