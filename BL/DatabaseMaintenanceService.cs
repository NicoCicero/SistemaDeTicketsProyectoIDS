using DAO;
using System;
using System.IO;

namespace BL
{
    public class DatabaseMaintenanceService
    {
        private static readonly Lazy<DatabaseMaintenanceService> _inst =
            new Lazy<DatabaseMaintenanceService>(() => new DatabaseMaintenanceService());

        public static DatabaseMaintenanceService Instancia => _inst.Value;

        private readonly DatabaseMaintenanceRepository _repo = new DatabaseMaintenanceRepository();

        private const string DatabaseName = "BDSistemaDeTickets";
        private const string CarpetaBackup = @"C:\\Program Files\\Microsoft SQL Server\\MSSQL15.MSSQLSERVER\\MSSQL\\Backup";

        private DatabaseMaintenanceService() { }

        public string RutaBackupPorDefecto => Path.Combine(CarpetaBackup, $"{DatabaseName}.bak");

        public string CrearBackup()
        {
            var rutaCompleta = RutaBackupPorDefecto;

            _repo.CrearBackup(DatabaseName, rutaCompleta);

            return rutaCompleta;
        }

        public void RestaurarDesdeBackup(string backupPath = null)
        {
            var ruta = backupPath ?? RutaBackupPorDefecto;
            _repo.RestaurarDesdeBackup(DatabaseName, ruta);
        }
    }
}
