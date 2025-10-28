using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;
using System;

namespace MMABooks.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        private Customer customer;

        // SetUp method to create a fresh customer for each test
        [SetUp]
        public void SetUp()
        {
            customer = new Customer(1, "John Doe", "123 Main St", "Anytown", "NY", "12345");
        }

        // Test the constructor and initialization
        [Test]
        public void TestCustomerConstructor()
        {
            Assert.IsNotNull(customer);
            Assert.AreEqual(1, customer.CustomerID);
            Assert.AreEqual("John Doe", customer.Name);
            Assert.AreEqual("123 Main St", customer.Address);
            Assert.AreEqual("Anytown", customer.City);
            Assert.AreEqual("NY", customer.State);
            Assert.AreEqual("12345", customer.ZipCode);
        }

        // Test setters and getters
        [Test]
        public void TestCustomerSettersAndGetters()
        {
            customer.Name = "Jane Doe";
            customer.Address = "456 Elm St";
            customer.City = "Othertown";
            customer.State = "CA";
            customer.ZipCode = "67890";

            Assert.AreEqual("Jane Doe", customer.Name);
            Assert.AreEqual("456 Elm St", customer.Address);
            Assert.AreEqual("Othertown", customer.City);
            Assert.AreEqual("CA", customer.State);
            Assert.AreEqual("67890", customer.ZipCode);
        }

        // Test the ToString method
        [Test]
        public void TestCustomerToString()
        {
            string expected = "CustomerID: 1, Name: John Doe, Address: 123 Main St, City: Anytown, State: NY, ZipCode: 12345";
            Assert.AreEqual(expected, customer.ToString());
        }

        // Test AddCustomer method (assuming the database connection is set up)
        [Test]
        public void TestAddCustomer()
        {
            Customer newCustomer = new Customer(0, "Test User", "789 Oak St", "Testville", "TX", "54321");
            bool success = CustomerDb.AddCustomer(newCustomer);
            Assert.IsTrue(success);
        }

        // Test GetCustomer method
        [Test]
        public void TestGetCustomer()
        {
            int testID = 1;
            Customer fetchedCustomer = CustomerDb.GetCustomer(testID);
            Assert.IsNotNull(fetchedCustomer);
            Assert.AreEqual(testID, fetchedCustomer.CustomerID);
        }
    }
}
