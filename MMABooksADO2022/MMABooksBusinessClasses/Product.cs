using System;

namespace MMABooksBusinessClasses
{
    public class Product
    {
        // Default constructor
        public Product() { }

        // Parameterized constructor
        public Product(string description, int onHandQuantity, string productCode, decimal unitPrice)
        {
            Description = description;
            OnHandQuantity = onHandQuantity;
            ProductCode = productCode;
            UnitPrice = unitPrice;
        }

        // Properties
        public string Description { get; set; }
        public int OnHandQuantity { get; set; }
        public string ProductCode { get; set; }
        public decimal UnitPrice { get; set; }

        // Setters with validation
        public void SetProductCode(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                throw new ArgumentException("Product code cannot be null or empty.");
            }
            ProductCode = productCode;
        }

        public void SetDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.");
            }
            Description = description;
        }

        public void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
            {
                throw new ArgumentException("Unit price must be greater than zero.");
            }
            UnitPrice = unitPrice;
        }

        public void SetOnHandQuantity(int onHandQuantity)
        {
            if (onHandQuantity < 0)
            {
                throw new ArgumentException("On hand quantity cannot be negative.");
            }
            OnHandQuantity = onHandQuantity;
        }

        // Override ToString() method for displaying product information
        public override string ToString()
        {
            return $"Product Code: {ProductCode}, Description: {Description}, " +
                   $"Unit Price: {UnitPrice:C}, Quantity On Hand: {OnHandQuantity}";
        }
    }
}
