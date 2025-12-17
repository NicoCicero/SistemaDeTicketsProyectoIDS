using System;
using DAO;

namespace BL
{
    public class SeguridadBootstrapService
    {
        private static readonly Lazy<SeguridadBootstrapService> _instancia =
            new Lazy<SeguridadBootstrapService>(() => new SeguridadBootstrapService());

        public static SeguridadBootstrapService Instancia => _instancia.Value;

        private readonly SeguridadBootstrapRepository _repo = new SeguridadBootstrapRepository();

        private SeguridadBootstrapService() { }

        public void AsegurarInvitadoYMultiidioma()
        {
            try
            {
                int? permisoId = _repo.ObtenerPermisoId("GestionarMultiidioma");
                if (!permisoId.HasValue)
                {
                    permisoId = _repo.CrearPermisoSimple("GestionarMultiidioma");
                }

                int? adminRolId = _repo.ObtenerRolId("Administrador");
                if (adminRolId.HasValue && permisoId.HasValue)
                {
                    _repo.AsignarPermisoSiNoExiste(adminRolId.Value, permisoId.Value);
                }

                int? invitadoRolId = _repo.ObtenerRolId("Invitado");
                if (!invitadoRolId.HasValue)
                {
                    _repo.CrearRolSinPermisos("Invitado");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Bootstrap de seguridad no pudo completarse: " + ex.Message);
            }
        }
    }
}
