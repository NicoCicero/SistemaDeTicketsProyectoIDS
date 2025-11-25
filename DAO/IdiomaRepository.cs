using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class IdiomaRepository : DAL
    {
        public List<(int Id, string Codigo, string Nombre, bool EsPorDefecto, bool Activo)> ListarIdiomasRaw()
        {
            var list = new List<(int, string, string, bool, bool)>();

            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT IdIdioma, Codigo, Nombre, EsPorDefecto, Activo FROM Idioma ORDER BY Nombre;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        list.Add((
                            rd.GetInt32(0),
                            rd.GetString(1),
                            rd.GetString(2),
                            rd.GetBoolean(3),
                            rd.GetBoolean(4)
                        ));
                    }
                }
            }
            return list;
        }

        public (int Id, string Codigo, string Nombre) ObtenerIdiomaPorDefecto()
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "SELECT TOP 1 IdIdioma, Codigo, Nombre FROM Idioma WHERE EsPorDefecto = 1;", cn))
            {
                cn.Open();
                using (var rd = cmd.ExecuteReader())
                {
                    if (rd.Read())
                        return (rd.GetInt32(0), rd.GetString(1), rd.GetString(2));
                }
            }
            return (0, null, null);
        }

        public bool ExisteCodigo(string codigo)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Idioma WHERE Codigo = @cod;", cn))
            {
                cmd.Parameters.AddWithValue("@cod", codigo);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public bool ExisteNombre(string nombre)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT COUNT(1) FROM Idioma WHERE Nombre = @nom;", cn))
            {
                cmd.Parameters.AddWithValue("@nom", nombre);
                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }

        public int CrearIdioma(string codigo, string nombre, bool activo)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"
                INSERT INTO Idioma (Codigo, Nombre, EsPorDefecto, Activo)
                OUTPUT INSERTED.IdIdioma
                VALUES (@cod, @nom, 0, @act);
            ", cn))
            {
                cmd.Parameters.AddWithValue("@cod", codigo);
                cmd.Parameters.AddWithValue("@nom", nombre);
                cmd.Parameters.AddWithValue("@act", activo);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void ActualizarEstado(int idIdioma, bool activo)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("UPDATE Idioma SET Activo = @act WHERE IdIdioma = @id;", cn))
            {
                cmd.Parameters.AddWithValue("@id", idIdioma);
                cmd.Parameters.AddWithValue("@act", activo);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarIdioma(int idIdioma)
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                using (var tx = cn.BeginTransaction())
                {
                    using (var cmdDelTrads = new SqlCommand("DELETE FROM Traduccion WHERE IdIdioma = @id;", cn, tx))
                    {
                        cmdDelTrads.Parameters.AddWithValue("@id", idIdioma);
                        cmdDelTrads.ExecuteNonQuery();
                    }

                    using (var cmdDelIdioma = new SqlCommand("DELETE FROM Idioma WHERE IdIdioma = @id;", cn, tx))
                    {
                        cmdDelIdioma.Parameters.AddWithValue("@id", idIdioma);
                        cmdDelIdioma.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
            }
        }
    }
}
