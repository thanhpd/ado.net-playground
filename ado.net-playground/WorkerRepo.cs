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
        public static void BasicCommandDataReader(SqlConnection connection)
        {            
            using (connection)
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

        public static void MultipleResultsDataReader(SqlConnection connection)
        {            
            using (connection)
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

        public static void GetSchemaInfoDataReader(SqlConnection connection)
        {
            using (connection)
            {
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "SELECT CategoryID, CategoryName FROM Categories";
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                DataTable schemaTable = reader.GetSchemaTable();

                foreach (DataRow row in schemaTable.Rows)
                {
                    foreach (DataColumn column in schemaTable.Columns)
                    {
                        Console.WriteLine($"{column.ColumnName} = {row[column]}");
                    }                    
                }                
            }
        }

        public static void BasicCommandDataAdapter(SqlConnection connection)
        {
            using (connection)
            {
                string queryString = "SELECT TOP 100 * FROM Customers";
                SqlDataAdapter adapter = new SqlDataAdapter(queryString, connection);

                DataSet customers = new DataSet();
                adapter.Fill(customers, "Customers");

                if (!customers.HasErrors)
                {
                    foreach (DataTable table in customers.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            foreach (object item in row.ItemArray)
                            {
                                Console.Write($"{item} ");
                            }
                            Console.WriteLine();
                        }                        
                    }                    
                }
            }
        }
    }
}
