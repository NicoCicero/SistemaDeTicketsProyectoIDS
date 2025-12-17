using System;
using System.Data.SqlClient;

namespace DAO
{
    public class SeguridadBootstrapRepository : DAL
    {
        public int? ObtenerPermisoId(string nombre)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT Permiso_Id FROM Permiso WHERE Permiso_Nombre = @nombre;", cn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cn.Open();
                var res = cmd.ExecuteScalar();
                return res == null ? (int?)null : Convert.ToInt32(res);
            }
        }

        public int? ObtenerRolId(string nombre)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT Rol_Id FROM Rol WHERE Rol_Nombre = @nombre;", cn))
            {
                cmd.Parameters.AddWithValue("@nombre", nombre);
                cn.Open();
                var res = cmd.ExecuteScalar();
                return res == null ? (int?)null : Convert.ToInt32(res);
            }
        }

        public int CrearPermisoSimple(string nombre)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "INSERT INTO Permiso (Permiso_Nombre, EsCompuesto) VALUES (@nom, 0); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                cn))
            {
                cmd.Parameters.AddWithValue("@nom", nombre);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public int CrearRolSinPermisos(string nombre)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(
                "INSERT INTO Rol (Rol_Nombre) VALUES (@nom); SELECT CAST(SCOPE_IDENTITY() AS INT);",
                cn))
            {
                cmd.Parameters.AddWithValue("@nom", nombre);
                cn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }

        public void AsignarPermisoSiNoExiste(int rolId, int permisoId)
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand(@"IF NOT EXISTS (SELECT 1 FROM RolPermiso WHERE Rol_Id = @rol AND Permiso_Id = @permiso)
    INSERT INTO RolPermiso (Rol_Id, Permiso_Id) VALUES (@rol, @permiso);", cn))
            {
                cmd.Parameters.AddWithValue("@rol", rolId);
                cmd.Parameters.AddWithValue("@permiso", permisoId);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
