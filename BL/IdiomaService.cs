using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    public class IdiomaService
    {
        private readonly IdiomaRepository _idiomaRepo = new IdiomaRepository();
        private readonly TraduccionRepository _tradRepo = new TraduccionRepository();

        public void SeleccionarIdioma(string codigo)
        {
            var traducciones = _tradRepo.ObtenerTraduccionesPorCodigo(codigo);
            var todasLasClaves = _tradRepo.ListarClaves();

            // fallback
            foreach (var clave in todasLasClaves)
                if (!traducciones.ContainsKey(clave))
                    traducciones[clave] = $"[{clave}]";

            // ✅ guardar el código actual antes o después de Notificar (da igual)
            IdiomaManager.Instancia.SetCodigoActual(codigo);

            // notificar a todos los forms suscriptos
            IdiomaManager.Instancia.Notificar(traducciones);
        }

        public void SeleccionarIdiomaPorDefecto()
        {
            var def = _idiomaRepo.ObtenerIdiomaPorDefecto();
            if (def.Codigo != null)
                SeleccionarIdioma(def.Codigo);
        }

        //  ESTA es la que usa el ComboBox
        public List<Idioma> ListarIdiomas()
        {
            var raws = _idiomaRepo.ListarIdiomasRaw();
            return raws.Select(r => new Idioma
            {
                Id = r.Id,
                Codigo = r.Codigo,
                Nombre = r.Nombre,
                EsPorDefecto = r.EsPorDefecto,
                Activo = r.Activo
            }).ToList();
        }

        public List<Idioma> ListarIdiomasActivos()
        {
            return ListarIdiomas().Where(i => i.Activo).ToList();
        }

        public Idioma CrearIdioma(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del idioma es obligatorio", nameof(nombre));

            nombre = nombre.Trim();
            if (_idiomaRepo.ExisteNombre(nombre))
                throw new InvalidOperationException("Ya existe un idioma con ese nombre.");

            string codigoBase = new string(nombre.ToUpperInvariant().Where(char.IsLetterOrDigit).ToArray());
            if (string.IsNullOrEmpty(codigoBase))
                codigoBase = "IDI";
            if (codigoBase.Length > 5)
                codigoBase = codigoBase.Substring(0, 5);

            string codigo = codigoBase;
            int idx = 1;
            while (_idiomaRepo.ExisteCodigo(codigo))
            {
                codigo = $"{codigoBase}{idx}";
                idx++;
            }

            var id = _idiomaRepo.CrearIdioma(codigo, nombre, false);
            return new Idioma { Id = id, Codigo = codigo, Nombre = nombre, EsPorDefecto = false, Activo = false };
        }

        public void ActualizarEstadoIdioma(int idIdioma, bool activo)
        {
            _idiomaRepo.ActualizarEstado(idIdioma, activo);
        }

        public void EliminarIdioma(int idIdioma)
        {
            _idiomaRepo.EliminarIdioma(idIdioma);
        }

        public List<EtiquetaTraduccion> ListarEtiquetasConTraduccion(int idIdioma)
        {
            var raws = _tradRepo.ListarEtiquetasConTraduccion(idIdioma);
            return raws.Select(r => new EtiquetaTraduccion
            {
                IdEtiqueta = r.IdEtiqueta,
                Clave = r.Clave,
                Descripcion = r.Descripcion,
                Texto = r.Texto
            }).ToList();
        }

        public void GuardarTraduccion(int idIdioma, int idEtiqueta, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("El texto de la traducción es obligatorio", nameof(texto));

            _tradRepo.GuardarTraduccion(idIdioma, idEtiqueta, texto.Trim());
        }

        public void EliminarTraduccion(int idIdioma, int idEtiqueta)
        {
            _tradRepo.EliminarTraduccion(idIdioma, idEtiqueta);
        }
    }
}
