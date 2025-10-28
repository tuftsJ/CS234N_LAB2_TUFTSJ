using MySql.Data.MySqlClient;
using MMABooksBusinessClasses;
using System;
using System.Collections.Generic;

namespace MMABooksDBClasses
{
    public static class CustomerDb
    {
        // Add a new customer to the database
        public static bool AddCustomer(Customer customer)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement = "INSERT INTO Customers (Name, Address, City, State, ZipCode) "
                                     + "VALUES (@Name, @Address, @City, @State, @ZipCode)";

            MySqlCommand insertCommand = new MySqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@Name", customer.Name);
            insertCommand.Parameters.AddWithValue("@Address", customer.Address);
            insertCommand.Parameters.AddWithValue("@City", customer.City);
            insertCommand.Parameters.AddWithValue("@State", customer.State);
            insertCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode);

            try
            {
                connection.Open();
                insertCommand.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // Get a customer by their ID
        public static Customer GetCustomer(int customerID)
        {
            Customer customer = null;
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, City, State, ZipCode "
                                    + "FROM Customers "
                                    + "WHERE CustomerID = @CustomerID";

            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@CustomerID", customerID);

            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer(
                        reader.GetInt32("CustomerID"),
                        reader.GetString("Name"),
                        reader.GetString("Address"),
                        reader.GetString("City"),
                        reader.GetString("State"),
                        reader.GetString("ZipCode")
                    );
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return customer;
        }

        // Get a list of all customers
        public static List<Customer> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT CustomerID, Name, Address, City, State, ZipCode "
                                    + "FROM Customers "
                                    + "ORDER BY Name";

            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);

            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer(
                        reader.GetInt32("CustomerID"),
                        reader.GetString("Name"),
                        reader.GetString("Address"),
                        reader.GetString("City"),
                        reader.GetString("State"),
                        reader.GetString("ZipCode")
                    );
                    customers.Add(customer);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return customers;
        }

        // Update a customer's details
        public static bool UpdateCustomer(Customer customer)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement = "UPDATE Customers "
                                    + "SET Name = @Name, Address = @Address, City = @City, State = @State, ZipCode = @ZipCode "
                                    + "WHERE CustomerID = @CustomerID";

            MySqlCommand updateCommand = new MySqlCommand(updateStatement, connection);
            updateCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
            updateCommand.Parameters.AddWithValue("@Name", customer.Name);
            updateCommand.Parameters.AddWithValue("@Address", customer.Address);
            updateCommand.Parameters.AddWithValue("@City", customer.City);
            updateCommand.Parameters.AddWithValue("@State", customer.State);
            updateCommand.Parameters.AddWithValue("@ZipCode", customer.ZipCode);

            try
            {
                connection.Open();
                int rowsAffected = updateCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

        // Delete a customer from the database
        public static bool DeleteCustomer(Customer customer)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string deleteStatement = "DELETE FROM Customers "
                                    + "WHERE CustomerID = @CustomerID";

            MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@CustomerID", customer.CustomerID);

            try
            {
                connection.Open();
                int rowsAffected = deleteCommand.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
