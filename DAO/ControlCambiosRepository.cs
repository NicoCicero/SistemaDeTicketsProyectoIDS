using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAO
{
    public class ControlCambiosRepository : DAL
    {
        public void RegistrarCambio(
            string entidad,
            int entidadId,
            string accion,
            string campo,
            string valorAnterior,
            string valorNuevo,
            int? usuarioId)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO ControlCambios
                    (FechaUtc, UsuarioId, Entidad, EntidadId, Accion, Campo, ValorAnterior, ValorNuevo)
                VALUES
                    (SYSUTCDATETIME(), @uid, @ent, @entId, @acc, @campo, @valAnt, @valNue);", cn))
            {
                cmd.Parameters.AddWithValue("@uid", (object)usuarioId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ent", entidad);
                cmd.Parameters.AddWithValue("@entId", entidadId);
                cmd.Parameters.AddWithValue("@acc", accion);
                cmd.Parameters.AddWithValue("@campo", (object)campo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valAnt", (object)valorAnterior ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@valNue", (object)valorNuevo ?? DBNull.Value);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<ControlCambioEntry> FiltrarCambios(
           int? id = null,
           int? usuarioId = null,
           string entidad = null,
           int? entidadId = null,
           string campo = null,
           DateTime? desdeUtc = null,
           DateTime? hastaUtcExcl = null,
           int top = 500)
        {
            var list = new List<ControlCambioEntry>();

            using (var cn = GetConnection())
            {
                var sb = new System.Text.StringBuilder(@"
                    SELECT {TOP} Id, FechaUtc, UsuarioId, Entidad, EntidadId, Accion, Campo, ValorAnterior, ValorNuevo
                    FROM ControlCambios
                    WHERE 1=1");

                if (id.HasValue) sb.Append(" AND Id = @id");
                if (usuarioId.HasValue) sb.Append(" AND UsuarioId = @uid");
                if (!string.IsNullOrWhiteSpace(entidad)) sb.Append(" AND Entidad = @ent");
                if (entidadId.HasValue) sb.Append(" AND EntidadId = @entid");
                if (!string.IsNullOrWhiteSpace(campo)) sb.Append(" AND Campo = @campo");
                if (desdeUtc.HasValue) sb.Append(" AND FechaUtc >= @desde");
                if (hastaUtcExcl.HasValue) sb.Append(" AND FechaUtc < @hasta");

                sb.Append(" ORDER BY FechaUtc DESC;");

                string sql = sb.ToString().Replace("{TOP}",
                    (id.HasValue || usuarioId.HasValue || !string.IsNullOrWhiteSpace(entidad)
                     || entidadId.HasValue || !string.IsNullOrWhiteSpace(campo)
                     || desdeUtc.HasValue || hastaUtcExcl.HasValue)
                     ? ""
                     : $"TOP ({top})");

                using (var cmd = new SqlCommand(sql, cn))
                {
                    if (id.HasValue) cmd.Parameters.AddWithValue("@id", id.Value);
                    if (usuarioId.HasValue) cmd.Parameters.AddWithValue("@uid", usuarioId.Value);
                    if (!string.IsNullOrWhiteSpace(entidad)) cmd.Parameters.AddWithValue("@ent", entidad);
                    if (entidadId.HasValue) cmd.Parameters.AddWithValue("@entid", entidadId.Value);
                    if (!string.IsNullOrWhiteSpace(campo)) cmd.Parameters.AddWithValue("@campo", campo);
                    if (desdeUtc.HasValue) cmd.Parameters.AddWithValue("@desde", desdeUtc.Value);
                    if (hastaUtcExcl.HasValue) cmd.Parameters.AddWithValue("@hasta", hastaUtcExcl.Value);

                    cn.Open();
                    using (var rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            list.Add(new ControlCambioEntry
                            {
                                Id = rd.GetInt32(0),
                                FechaUtc = rd.GetDateTime(1),
                                UsuarioId = rd.IsDBNull(2) ? (int?)null : rd.GetInt32(2),
                                Entidad = rd.GetString(3),
                                EntidadId = rd.GetInt32(4),
                                Accion = rd.GetString(5),
                                Campo = rd.IsDBNull(6) ? null : rd.GetString(6),
                                ValorAnterior = rd.IsDBNull(7) ? null : rd.GetString(7),
                                ValorNuevo = rd.IsDBNull(8) ? null : rd.GetString(8)
                            });
                        }
                    }
                }
            }

            return list;
        }  
    
    }
}
