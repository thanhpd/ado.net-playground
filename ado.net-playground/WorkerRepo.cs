﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
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

        public static void DataSetFromMultipleDataAdapter(SqlConnection connection)
        {
            using (connection)
            {
                SqlDataAdapter territoryAdapter = new SqlDataAdapter("SELECT * FROM Territories", connection);
                SqlDataAdapter employeeAdapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
                SqlDataAdapter employeeTerritoriesAdapter = new SqlDataAdapter("SELECT * FROM EmployeeTerritories", connection);

                DataSet employeeTerritories = new DataSet();

                territoryAdapter.Fill(employeeTerritories, "Territories");
                employeeAdapter.Fill(employeeTerritories, "Employees");
                employeeTerritoriesAdapter.Fill(employeeTerritories, "EmployeeTerritories");

                DataRelation relation1 = employeeTerritories.Relations.Add("EmployeeID",
                    employeeTerritories.Tables["Employees"].Columns["EmployeeID"],
                    employeeTerritories.Tables["EmployeeTerritories"].Columns["EmployeeID"]);
                DataRelation relation2 = employeeTerritories.Relations.Add("TerritoryID",
                    employeeTerritories.Tables["Territories"].Columns["TerritoryID"],
                    employeeTerritories.Tables["EmployeeTerritories"].Columns["TerritoryID"]);

                foreach (DataRow p1Row in employeeTerritories.Tables["Territories"].Rows)
                {
                    Console.WriteLine($"Territory ID = {p1Row["TerritoryID"]}");
                    foreach (object item in p1Row.ItemArray)
                    {
                        Console.Write($"{item} ");
                    }
                    Console.WriteLine();
                    foreach (DataRow p2Row in p1Row.GetChildRows(relation2))
                    {
                        Console.WriteLine($"EmployeeID = {p2Row["EmployeeID"]}");
                        foreach (DataRow p3Row in p2Row.GetParentRows(relation1))
                        {
                            foreach (object item in p3Row.ItemArray)
                            {
                                Console.Write($"{item} ");
                            }
                        }
                        Console.WriteLine();
                    }
                }

            }
        }

        private static SqlDataAdapter CreateSqlDataAdapter(SqlConnection connection)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            // Create the command
            adapter.SelectCommand = new SqlCommand("SELECT CustomerID, CompanyName FROM Customers", connection);
            adapter.InsertCommand = new SqlCommand("INSERT INTO Customers (CustomerID, CompanyName) VALUES (@CustomerID, @CompanyName)", connection);
            adapter.UpdateCommand = new SqlCommand("UPDATE Customers SET CustomerID = @CustomerID, CompanyName = @CompanyName WHERE CustomerID = @oldCustomerID", connection);
            adapter.DeleteCommand = new SqlCommand("DELETE FROM Customers WHERE CustomerID = @CustomerID", connection);

            // Create the parameters
            adapter.InsertCommand.Parameters.Add("@CustomerID", SqlDbType.Char, 5, "CustomerID");
            adapter.InsertCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 40, "CompanyName");

            adapter.UpdateCommand.Parameters.Add("@CustomerID", SqlDbType.Char, 5, "CustomerID");
            adapter.UpdateCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 40, "CompanyName");
            adapter.UpdateCommand.Parameters.Add("@oldCustomerID", SqlDbType.Char, 5, "CustomerID").SourceVersion = DataRowVersion.Original;

            adapter.DeleteCommand.Parameters.Add("@CustomerID", SqlDbType.Char, 5, "CustomerID").SourceVersion = DataRowVersion.Original;
            return adapter;
        }

        public static void TestCustomSqlDataAdapter(SqlConnection connection)
        {
            SqlDataAdapter customerAdapter = CreateSqlDataAdapter(connection);
            DataSet customerDataSet = new DataSet();
            customerAdapter.Fill(customerDataSet, "Customers");

            foreach (DataTable dataTable in customerDataSet.Tables)
            {
                foreach (DataRow dataTableRow in dataTable.Rows)
                {
                    foreach (var o in dataTableRow.ItemArray)
                    {
                        Console.Write($"{o} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void FillDataSetWithConstraintFromDataAdapter(SqlConnection connection)
        {
            SqlDataAdapter customerAdapter = CreateSqlDataAdapter(connection);
            customerAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            DataSet customerDataSet = new DataSet();
            customerAdapter.FillSchema(customerDataSet, SchemaType.Source, "Customers");
            customerAdapter.Fill(customerDataSet, "Customers");            

            foreach (DataTable dataTable in customerDataSet.Tables)
            {
                foreach (Constraint dataTableConstraint in dataTable.Constraints)
                {
                    Console.WriteLine($"{dataTableConstraint.ConstraintName} {dataTableConstraint.ExtendedProperties}");
                }                
            }
        }

        public static void TestingMappingDataAdapter(SqlConnection connection)
        {
            using (connection)
            {
                SqlDataAdapter employeeAdapter = new SqlDataAdapter("SELECT * FROM Employees", connection);
                DataTableMapping mapping = employeeAdapter.TableMappings.Add("EmployeesTable", "Employees");
                mapping.ColumnMappings.Add("GivenName", "FirstName");
                mapping.ColumnMappings.Add("FamilyName", "LastName");
                DataSet employeeDataSet = new DataSet();
                employeeAdapter.Fill(employeeDataSet, "EmployeesTable");

                if (!employeeDataSet.HasErrors)
                {
                    foreach (DataTable table in employeeDataSet.Tables)
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

        // Not really recommend since server still return all result instead of a small chunk; use query instead
        public static void PagingQueryResultDataAdapter(SqlConnection connection)
        {
            using (connection)
            {
                int currentIndex = 0;
                int pageSize = 5;

                SqlDataAdapter customerAdapter = new SqlDataAdapter("SELECT * FROM Customers ORDER BY CustomerID", connection);
                DataSet customersDataSet = new DataSet();
                customerAdapter.Fill(customersDataSet, currentIndex, pageSize, "Customers");

                if (!customersDataSet.HasErrors)
                {
                    foreach (DataTable table in customersDataSet.Tables)
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
