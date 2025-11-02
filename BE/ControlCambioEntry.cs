using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class ControlCambioEntry
    {
        public int Id { get; set; }
        public DateTime FechaUtc { get; set; }
        public int? UsuarioId { get; set; }
        public string Entidad { get; set; }
        public int EntidadId { get; set; }
        public string Accion { get; set; }
        public string Campo { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }

    }
}
