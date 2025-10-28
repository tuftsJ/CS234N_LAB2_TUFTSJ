using NUnit.Framework;
using MMABooksBusinessClasses;
using System;

namespace MMABooks.Tests
{
    [TestFixture]
    public class ProductTests
    {
        // Test default constructor and parameterized constructor
        [Test]
        public void TestProductConstructor()
        {
            Product product1 = new Product();
            Assert.IsNotNull(product1);
            Assert.AreEqual(null, product1.ProductCode);
            Assert.AreEqual(null, product1.Description);
            Assert.AreEqual(0, product1.OnHandQuantity);
            Assert.AreEqual(0m, product1.UnitPrice);

            string code = "P001";
            string description = "Test Product";
            int quantity = 100;
            decimal price = 9.99m;
            Product product2 = new Product(description, quantity, code, price);
            Assert.IsNotNull(product2);
            Assert.AreEqual(code, product2.ProductCode);
            Assert.AreEqual(description, product2.Description);
            Assert.AreEqual(quantity, product2.OnHandQuantity);
            Assert.AreEqual(price, product2.UnitPrice);
        }

        // Test setters for Product properties
        [Test]
        public void TestProductSetters()
        {
            Product product = new Product("Old Product", 10, "P001", 19.99m);

            // Test setting valid values
            product.ProductCode = "P002";
            product.Description = "Updated Product";
            product.OnHandQuantity = 200;
            product.UnitPrice = 29.99m;

            // Assert new values
            Assert.AreEqual("P002", product.ProductCode);
            Assert.AreEqual("Updated Product", product.Description);
            Assert.AreEqual(200, product.OnHandQuantity);
            Assert.AreEqual(29.99m, product.UnitPrice);

            // Test with another set of valid values
            product.ProductCode = "P003";
            product.Description = "Another Product";
            product.OnHandQuantity = 50;
            product.UnitPrice = 39.99m;

            Assert.AreEqual("P003", product.ProductCode);
            Assert.AreEqual("Another Product", product.Description);
            Assert.AreEqual(50, product.OnHandQuantity);
            Assert.AreEqual(39.99m, product.UnitPrice);
        }

        // Test getter functionality implicitly through the setter tests (it wouldn't pass if getters didn't work)
        [Test]
        public void TestProductGetters()
        {
            Product product = new Product("Test Product", 10, "P001", 19.99m);
            Assert.AreEqual("Test Product", product.Description);
            Assert.AreEqual(10, product.OnHandQuantity);
            Assert.AreEqual("P001", product.ProductCode);
            Assert.AreEqual(19.99m, product.UnitPrice);
        }

        // Test the ToString() method
        [Test]
        public void TestProductToString()
        {
            Product product = new Product("Test Product", 100, "P001", 19.99m);
            string expected = "Product Code: P001, Description: Test Product, " +
                              "Unit Price: $19.99, Quantity On Hand: 100";
            Assert.IsTrue(product.ToString().Contains("P001"));
            Assert.IsTrue(product.ToString().Contains("Test Product"));
            Assert.IsTrue(product.ToString().Contains("19.99"));
            Assert.IsTrue(product.ToString().Contains("100"));
        }

        // Test invalid value handling in setters
        [Test]
        public void TestSetProductCodeInvalidValue()
        {
            Product product = new Product("Test Product", 10, "P001", 19.99m);

            Assert.Throws<ArgumentException>(() => product.ProductCode = "");
            Assert.Throws<ArgumentException>(() => product.ProductCode = null);
        }

        [Test]
        public void TestSetUnitPriceInvalidValue()
        {
            Product product = new Product("Test Product", 10, "P001", 19.99m);

            Assert.Throws<ArgumentException>(() => product.UnitPrice = -1);
            Assert.Throws<ArgumentException>(() => product.UnitPrice = 0);
        }

        [Test]
        public void TestSetOnHandQuantityInvalidValue()
        {
            Product product = new Product("Test Product", 10, "P001", 19.99m);

            Assert.Throws<ArgumentException>(() => product.OnHandQuantity = -1);
        }
    }
}
