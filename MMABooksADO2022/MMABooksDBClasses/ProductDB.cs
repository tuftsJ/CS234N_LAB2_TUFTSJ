using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using MMABooksBusinessClasses;

namespace MMABooksDBClasses
{
    public static class ProductDb
    {
        // Method to get all products from the database
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            MySqlConnection connection = MMABooksDB.GetConnection(); // Assuming MMABooksDB.GetConnection() provides the DB connection
            string selectStatement = "SELECT ProductCode, Description, OnHandQuantity, UnitPrice "
                                   + "FROM Products "
                                   + "ORDER BY ProductCode";
            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                while (reader.Read())
                {
                    Product p = new Product();
                    p.ProductCode = reader["ProductCode"].ToString();
                    p.Description = reader["Description"].ToString();
                    p.OnHandQuantity = Convert.ToInt32(reader["OnHandQuantity"]);
                    p.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    products.Add(p);
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
            return products;
        }

        // Method to get a product by its ProductCode
        public static Product GetProduct(string productCode)
        {
            Product product = null;
            MySqlConnection connection = MMABooksDB.GetConnection();
            string selectStatement = "SELECT ProductCode, Description, OnHandQuantity, UnitPrice "
                                   + "FROM Products "
                                   + "WHERE ProductCode = @ProductCode";
            MySqlCommand selectCommand = new MySqlCommand(selectStatement, connection);
            selectCommand.Parameters.AddWithValue("@ProductCode", productCode);

            try
            {
                connection.Open();
                MySqlDataReader reader = selectCommand.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product();
                    product.ProductCode = reader["ProductCode"].ToString();
                    product.Description = reader["Description"].ToString();
                    product.OnHandQuantity = Convert.ToInt32(reader["OnHandQuantity"]);
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
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
            return product;
        }

        // Method to add a product to the database
        public static bool AddProduct(Product product)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string insertStatement = "INSERT INTO Products (ProductCode, Description, OnHandQuantity, UnitPrice) "
                                   + "VALUES (@ProductCode, @Description, @OnHandQuantity, @UnitPrice)";
            MySqlCommand insertCommand = new MySqlCommand(insertStatement, connection);
            insertCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            insertCommand.Parameters.AddWithValue("@Description", product.Description);
            insertCommand.Parameters.AddWithValue("@OnHandQuantity", product.OnHandQuantity);
            insertCommand.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

            try
            {
                connection.Open();
                int rowsAffected = insertCommand.ExecuteNonQuery();
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

        // Method to delete a product from the database
        public static bool DeleteProduct(Product product)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string deleteStatement = "DELETE FROM Products WHERE ProductCode = @ProductCode "
                                   + "AND Description = @Description AND OnHandQuantity = @OnHandQuantity "
                                   + "AND UnitPrice = @UnitPrice";
            MySqlCommand deleteCommand = new MySqlCommand(deleteStatement, connection);
            deleteCommand.Parameters.AddWithValue("@ProductCode", product.ProductCode);
            deleteCommand.Parameters.AddWithValue("@Description", product.Description);
            deleteCommand.Parameters.AddWithValue("@OnHandQuantity", product.OnHandQuantity);
            deleteCommand.Parameters.AddWithValue("@UnitPrice", product.UnitPrice);

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

        // Method to update an existing product in the database
        public static bool UpdateProduct(Product oldProduct, Product newProduct)
        {
            MySqlConnection connection = MMABooksDB.GetConnection();
            string updateStatement = "UPDATE Products SET ProductCode = @NewProductCode, "
                                   + "Description = @NewDescription, OnHandQuantity = @NewOnHandQuantity, "
                                   + "UnitPrice = @NewUnitPrice "
                                   + "WHERE ProductCode = @OldProductCode AND Description = @OldDescription "
                                   + "AND OnHandQuantity = @OldOnHandQuantity AND UnitPrice = @OldUnitPrice";
            MySqlCommand updateCommand = new MySqlCommand(updateStatement, connection);

            updateCommand.Parameters.AddWithValue("@OldProductCode", oldProduct.ProductCode);
            updateCommand.Parameters.AddWithValue("@OldDescription", oldProduct.Description);
            updateCommand.Parameters.AddWithValue("@OldOnHandQuantity", oldProduct.OnHandQuantity);
            updateCommand.Parameters.AddWithValue("@OldUnitPrice", oldProduct.UnitPrice);

            updateCommand.Parameters.AddWithValue("@NewProductCode", newProduct.ProductCode);
            updateCommand.Parameters.AddWithValue("@NewDescription", newProduct.Description);
            updateCommand.Parameters.AddWithValue("@NewOnHandQuantity", newProduct.OnHandQuantity);
            updateCommand.Parameters.AddWithValue("@NewUnitPrice", newProduct.UnitPrice);

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
    }
}
