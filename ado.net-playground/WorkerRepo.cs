using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ado.net_playground
{
    class WorkerRepo
    {
        public static void BasicCommand()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT TOP 1000 CompanyName FROM Customers";

                // Open the connection and execute reader
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}", reader[0]);
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }

                reader.Close();
            }
        }

        public static void MultipleResults()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["NorthwindConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT TOP 10 CompanyName, ContactName FROM Customers; SELECT TOP 10 EmployeeID, LastName FROM Employees";

                // Open the connection and execute reader
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader[0], reader[1]);
                    }
                    reader.NextResult();
                }                

                reader.Close();
            }
        }
    }
}
