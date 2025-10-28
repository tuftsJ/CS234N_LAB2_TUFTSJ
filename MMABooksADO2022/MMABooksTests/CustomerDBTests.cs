using MMABooksBusinessClasses;
using MMABooksDBClasses;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MMABooks.Tests
{
    [TestFixture]
    public class CustomerDbTests
    {
        private Customer customer;

        // SetUp method to create a fresh customer for each test
        [SetUp]
        public void SetUp()
        {
            customer = new Customer(0, "John Doe", "123 Main St", "Anytown", "NY", "12345");
        }

        // Test AddCustomer method
        [Test]
        public void TestAddCustomer()
        {
            bool result = CustomerDb.AddCustomer(customer);
            Assert.IsTrue(result, "The customer should be added successfully.");
        }

        // Test GetCustomer method by CustomerID
        [Test]
        public void TestGetCustomer()
        {
            // First, add the customer
            bool addResult = CustomerDb.AddCustomer(customer);
            Assert.IsTrue(addResult, "Customer should be added successfully.");

            // Now, fetch the customer by ID
            Customer fetchedCustomer = CustomerDb.GetCustomer(customer.CustomerID);
            Assert.IsNotNull(fetchedCustomer, "Customer should not be null.");
            Assert.AreEqual(customer.CustomerID, fetchedCustomer.CustomerID, "The fetched customer ID should match.");
            Assert.AreEqual(customer.Name, fetchedCustomer.Name, "The fetched customer Name should match.");
        }

        // Test GetCustomers method (fetch all customers)
        [Test]
        public void TestGetCustomers()
        {
            List<Customer> customers = CustomerDb.GetCustomers();
            Assert.IsNotNull(customers, "The list of customers should not be null.");
            Assert.IsTrue(customers.Count > 0, "There should be at least one customer in the database.");
        }

        // Test UpdateCustomer method
        [Test]
        public void TestUpdateCustomer()
        {
            // Add the customer first
            bool addResult = CustomerDb.AddCustomer(customer);
            Assert.IsTrue(addResult, "Customer should be added successfully.");

            // Modify the customer data
            customer.Name = "Jane Doe";
            customer.Address = "456 Oak St";
            customer.City = "Newtown";
            customer.State = "CA";
            customer.ZipCode = "54321";

            // Update the customer
            bool updateResult = CustomerDb.UpdateCustomer(customer);
            Assert.IsTrue(updateResult, "The customer should be updated successfully.");

            // Fetch the updated customer and verify changes
            Customer updatedCustomer = CustomerDb.GetCustomer(customer.CustomerID);
            Assert.AreEqual("Jane Doe", updatedCustomer.Name, "The customer Name should have been updated.");
            Assert.AreEqual("456 Oak St", updatedCustomer.Address, "The customer Address should have been updated.");
            Assert.AreEqual("Newtown", updatedCustomer.City, "The customer City should have been updated.");
            Assert.AreEqual("CA", updatedCustomer.State, "The customer State should have been updated.");
            Assert.AreEqual("54321", updatedCustomer.ZipCode, "The customer ZipCode should have been updated.");
        }

        // Test DeleteCustomer method
        [Test]
        public void TestDeleteCustomer()
        {
            // Add the customer first
            bool addResult = CustomerDb.AddCustomer(customer);
            Assert.IsTrue(addResult, "Customer should be added successfully.");

            // Now, delete the customer
            bool deleteResult = CustomerDb.DeleteCustomer(customer);
            Assert.IsTrue(deleteResult, "The customer should be deleted successfully.");

            // Try to fetch the customer again
            Customer deletedCustomer = CustomerDb.GetCustomer(customer.CustomerID);
            Assert.IsNull(deletedCustomer, "The customer should no longer exist in the database.");
        }

        // Test handling database errors during AddCustomer
        [Test]
        public void TestAddCustomerWithInvalidData()
        {
            Customer invalidCustomer = new Customer(0, null, "123 Invalid St", "Nowhere", "XX", "00000");

            try
            {
                bool result = CustomerDb.AddCustomer(invalidCustomer);
                Assert.Fail("The method should throw an exception for invalid customer data.");
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Cannot add customer"), "Expected error message should be thrown.");
            }
        }

        // Test handling database errors during UpdateCustomer
        [Test]
        public void TestUpdateCustomerWithInvalidData()
        {
            // Add a valid customer first
            bool addResult = CustomerDb.AddCustomer(customer);
            Assert.IsTrue(addResult, "Customer should be added successfully.");

            // Now modify and update with invalid data (e.g., empty name)
            customer.Name = null;

            try
            {
                bool updateResult = CustomerDb.UpdateCustomer(customer);
                Assert.Fail("The method should throw an exception when updating with invalid data.");
            }
            catch (MySqlException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Cannot update customer"), "Expected error message should be thrown.");
            }
        }

        // Test DeleteCustomer with non-existing customer
        [Test]
        public void TestDeleteCustomerWithNonExistingCustomer()
        {
            // Attempt to delete a non-existing customer (ID = 999)
            Customer nonExistingCustomer = new Customer(999, "Non Existing", "999 Nowhere", "Ghosttown", "XX", "00000");
            bool deleteResult = CustomerDb.DeleteCustomer(nonExistingCustomer);
            Assert.IsFalse(deleteResult, "Deleting a non-existing customer should return false.");
        }

        // Test GetCustomer with non-existing ID
        [Test]
        public void TestGetCustomerWithNonExistingID()
        {
            // Try to fetch a customer that doesn't exist
            Customer nonExistingCustomer = CustomerDb.GetCustomer(999);
            Assert.IsNull(nonExistingCustomer, "Fetching a non-existing customer should return null.");
        }
    }
}
