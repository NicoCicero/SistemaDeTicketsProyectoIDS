using System;
using System.Data.SqlClient;

namespace DAO
{
    public class DatabaseMaintenanceRepository : DAL
    {
        public void CrearBackup(string databaseName, string backupPath)
        {
            using (var cn = GetMasterConnection())
            using (var cmd = new SqlCommand("BACKUP DATABASE " + databaseName + " TO DISK = @ruta WITH FORMAT, INIT;", cn))
            {
                cmd.Parameters.AddWithValue("@ruta", backupPath);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RestaurarDesdeBackup(string databaseName, string backupPath)
        {
            var sql = $@"ALTER DATABASE {databaseName} SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
RESTORE DATABASE {databaseName} FROM DISK = @ruta WITH REPLACE;
ALTER DATABASE {databaseName} SET MULTI_USER;";

            using (var cn = GetMasterConnection())
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@ruta", backupPath);
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
