using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class DAL
    {

        protected readonly string mConn;

        public DAL()
        {
            mConn = "Data Source=localhost\\MSSQLSERVER01;Initial Catalog=BDSistemaDeTickets;Integrated Security=True;TrustServerCertificate=True";

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(mConn);

        }

        protected SqlConnection GetMasterConnection()
        {
            var builder = new SqlConnectionStringBuilder(mConn)
            {
                InitialCatalog = "master"
            };

            return new SqlConnection(builder.ConnectionString);
        }

    }
}
