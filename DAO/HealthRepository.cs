using System;
using System.Data.SqlClient;

namespace DAO
{
    public class HealthRepository : DAL
    {
        public bool ProbarConexion()
        {
            using (var cn = GetConnection())
            {
                cn.Open();
                return true;
            }
        }

        public DateTime ObtenerFechaUtcServidor()
        {
            using (var cn = GetConnection())
            using (var cmd = new SqlCommand("SELECT SYSUTCDATETIME();", cn))
            {
                cn.Open();
                var resultado = cmd.ExecuteScalar();
                return resultado == null ? DateTime.MinValue : (DateTime)resultado;
            }
        }
    }
}
