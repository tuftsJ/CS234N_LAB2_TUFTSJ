using NUnit.Framework;
using MMABooksBusinessClasses;
using MMABooksDBClasses;
using System;
using System.Linq;

namespace MMABooks.Tests
{
    [TestFixture]
    public class ProductDbTests
    {
        private Product testProduct;

        // SetUp method to create fresh product for each test
        [SetUp]
        public void SetUp()
        {
            testProduct = new Product("Test Product", 100, "P001", 19.99m);
        }

        // Test AddProduct method
        [Test]
        public void TestAddProduct()
        {
            // Act
            bool result = ProductDb.AddProduct(testProduct);

            // Assert
            Assert.IsTrue(result);

            // Clean up - Delete the added product
            ProductDb.DeleteProduct(testProduct);
        }

        // Test GetProduct method (fetches by ProductCode)
        [Test]
        public void TestGetProduct()
        {
            // Arrange
            ProductDb.AddProduct(testProduct);  // Adding the product to the DB

            // Act
            Product retrievedProduct = ProductDb.GetProduct(testProduct.ProductCode);

            // Assert
            Assert.IsNotNull(retrievedProduct);
            Assert.AreEqual(testProduct.ProductCode, retrievedProduct.ProductCode);

            // Clean up - Delete the added product
            ProductDb.DeleteProduct(testProduct);
        }

        // Test GetProducts method (retrieves all products)
        [Test]
        public void TestGetProducts()
        {
            // Arrange
            ProductDb.AddProduct(testProduct);  // Adding the product to the DB

            // Act
            var products = ProductDb.GetProducts();

            // Assert
            Assert.IsTrue(products.Any(p => p.ProductCode == testProduct.ProductCode));

            // Clean up - Delete the added product
            ProductDb.DeleteProduct(testProduct);
        }

        // Test UpdateProduct method
        [Test]
        public void TestUpdateProduct()
        {
            // Arrange
            Product oldProduct = new Product("Old Product", 10, "P001", 19.99m);
            ProductDb.AddProduct(oldProduct);

            // Creating a new updated product
            Product updatedProduct = new Product("Updated Product", 20, "P001", 29.99m);

            // Act
            bool result = ProductDb.UpdateProduct(oldProduct, updatedProduct);

            // Assert
            Assert.IsTrue(result);

            // Clean up - Delete the updated product
            ProductDb.DeleteProduct(updatedProduct);
        }

        // Test DeleteProduct method
        [Test]
        public void TestDeleteProduct()
        {
            // Arrange
            ProductDb.AddProduct(testProduct);  // Adding the product to the DB

            // Act
            bool result = ProductDb.DeleteProduct(testProduct);

            // Assert
            Assert.IsTrue(result);
        }
    }
}
