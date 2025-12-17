using System;
using System.Collections.Generic;
using BE;
using DAO;

namespace BL
{
    public class DashboardService
    {
        private readonly HealthRepository _healthRepo = new HealthRepository();
        private readonly AuditoriaService _auditoriaService = AuditoriaService.Instancia;

        public SystemStatusInfo ConstruirEstadoPublico(string idiomaActual, string versionAplicacion, string categoriaFiltro)
        {
            var estado = new SystemStatusInfo
            {
                IdiomaActual = string.IsNullOrWhiteSpace(idiomaActual) ? "" : idiomaActual,
                VersionAplicacion = versionAplicacion,
                HoraLocal = DateTime.Now
            };

            try
            {
                estado.BaseDatosConectada = _healthRepo.ProbarConexion();
                if (estado.BaseDatosConectada)
                {
                    var fechaUtc = _healthRepo.ObtenerFechaUtcServidor();
                    estado.HoraServidorUtc = fechaUtc == DateTime.MinValue ? (DateTime?)null : fechaUtc;
                }
            }
            catch
            {
                estado.BaseDatosConectada = false;
                estado.HoraServidorUtc = null;
            }

            estado.ActividadPublica = ObtenerActividadPublica(categoriaFiltro, estado.BaseDatosConectada);
            return estado;
        }

        public List<PublicActivityItem> ObtenerActividadPublica(string categoriaFiltro, bool tieneConexion)
        {
            var lista = new List<PublicActivityItem>();
            if (!tieneConexion)
                return lista;

            try
            {
                var registros = _auditoriaService.FiltrarAuditoria(top: 50) ?? new List<BE.AuditoriaEntry>();
                foreach (var registro in registros)
                {
                    var item = MapearActividad(registro);
                    if (item == null)
                        continue;

                    if (string.Equals(categoriaFiltro, "Todos", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(item.Categoria, categoriaFiltro, StringComparison.OrdinalIgnoreCase))
                    {
                        lista.Add(item);
                    }
                }
            }
            catch
            {
                return new List<PublicActivityItem>();
            }

            return lista;
        }

        private PublicActivityItem MapearActividad(AuditoriaEntry registro)
        {
            string categoria = "Sistema";
            string descripcion;

            switch (registro.Evento)
            {
                case AuditoriaEvento.AppStart:
                    descripcion = "Sistema iniciado";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.AppExit:
                    descripcion = "Aplicación cerrada";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.LoginOk:
                    descripcion = "Ingreso registrado";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.LoginFail:
                    descripcion = "Intento de acceso";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.LoginBloqueado:
                    descripcion = "Bloqueo temporal";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.Logout:
                    descripcion = "Cierre de sesión";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.PermisoDenegado:
                    descripcion = "Operación restringida";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.CambioPassword:
                    descripcion = "Contraseña actualizada";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.AltaUsuario:
                    descripcion = "Usuario creado";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.BajaUsuario:
                    descripcion = "Usuario deshabilitado";
                    categoria = "Sistema";
                    break;
                case AuditoriaEvento.ModificacionUsuario:
                    descripcion = "Perfil actualizado";
                    categoria = "Sistema";
                    break;
                default:
                    descripcion = "Actividad registrada";
                    break;
            }

            var detalle = registro.Detalle ?? string.Empty;
            if (detalle.IndexOf("idioma", StringComparison.OrdinalIgnoreCase) >= 0)
                categoria = "Idioma";
            else if (detalle.IndexOf("ticket", StringComparison.OrdinalIgnoreCase) >= 0)
                categoria = "Tickets";
            else if (detalle.IndexOf("backup", StringComparison.OrdinalIgnoreCase) >= 0)
                categoria = "Sistema";

            return new PublicActivityItem
            {
                Categoria = categoria,
                Descripcion = descripcion,
                FechaLocal = registro.FechaUtc.ToLocalTime()
            };
        }
    }
}
