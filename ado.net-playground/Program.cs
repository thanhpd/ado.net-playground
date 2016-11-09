using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ado.net_playground
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            WorkerRepo.FillDataSetWithConstraintFromDataAdapter(connection);
            Console.ReadLine();
        }
    }
}
