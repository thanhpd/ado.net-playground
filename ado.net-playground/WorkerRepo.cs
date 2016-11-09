using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ado.net_playground
{
    class WorkerRepo
    {
        public static void EstablishConnection()
        {
            string connectionString = ConfigurationManager.AppSettings["NorthwindConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
            }
        }
    }
}
