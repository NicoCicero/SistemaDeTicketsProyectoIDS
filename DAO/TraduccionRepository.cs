using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class TraduccionRepository : DAL
    {
        /// <summary>
        /// Devuelve diccionario {clave -> texto} para un idioma.
        /// Si alguna traducción está NULL en la tabla, NO la agrega.
        /// El fallback se hace en la capa BL.
        /// </summary>
        public Dictionary<string, string> ObtenerTraduccionesPorCodigo(string codigoIdioma)
        {
            var dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                SELECT e.Clave, t.Texto
                FROM Idioma i
                JOIN Traduccion t ON t.IdIdioma = i.IdIdioma
                JOIN Etiqueta e ON e.IdEtiqueta = t.IdEtiqueta
                WHERE i.Codigo = @cod
                  AND t.Texto IS NOT NULL;", cn))
            {
                cmd.Parameters.AddWithValue("@cod", codigoIdioma);
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        string clave = rd.GetString(0);
                        string texto = rd.GetString(1);
                        dic[clave] = texto;
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// Devuelve la lista de TODAS las claves conocidas (Etiqueta).
        /// La usamos para hacer fallback.
        /// </summary>
        public List<string> ListarClaves()
        {
            var list = new List<string>();
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT Clave FROM Etiqueta;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                        list.Add(rd.GetString(0));
                }
            }
            return list;
        }
    }
}
