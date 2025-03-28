using System;
using System.Collections.Generic;

// Address Class
public class Address
{
    private string StreetAddress { get; set; }
    private string City { get; set; }
    private string State { get; set; }
    private string Country { get; set; }

    // Constructor to initialize the address fields
    public Address(string streetAddress, string city, string state, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        Country = country;
    }

    // Method to check if the address is in the USA
    public bool IsInUSA()
    {
        return Country.ToLower() == "usa";
    }

    // Method to return the full address as a string
    public string GetFullAddress()
    {
        return $"{StreetAddress}\n{City}, {State}\n{Country}";
    }
}

// Product Class
public class Product
{
    private string Name { get; set; }
    private int ProductId { get; set; }
    private double Price { get; set; }
    private int Quantity { get; set; }

    // Constructor to initialize product details
    public Product(string name, int productId, double price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    // Method to calculate the total cost of the product
    public double GetTotalCost()
    {
        return Price * Quantity;
    }

    // Method to return the product info as a string
    public string GetProductInfo()
    {
        return $"{Name} (ID: {ProductId}) - ${Price} x {Quantity}";
    }
}

// Customer Class
public class Customer
{
    private string Name { get; set; }
    private Address CustomerAddress { get; set; }

    // Constructor to initialize customer details
    public Customer(string name, Address customerAddress)
    {
        Name = name;
        CustomerAddress = customerAddress;
    }

    // Method to check if the customer is in the USA
    public bool IsInUSA()
    {
        return CustomerAddress.IsInUSA();
    }

    // Method to get customer details
    public string GetCustomerInfo()
    {
        return $"{Name}\n{CustomerAddress.GetFullAddress()}";
    }
}

// Order Class
public class Order
{
    private List<Product> Products { get; set; }
    private Customer Customer { get; set; }
    private const double USA_ShippingCost = 5.0;
    private const double International_ShippingCost = 35.0;

    // Constructor to initialize the order details
    public Order(Customer customer)
    {
        Products = new List<Product>();
        Customer = customer;
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    // Method to calculate the total cost of the order
    public double GetTotalPrice()
    {
        double productTotal = 0;
        foreach (var product in Products)
        {
            productTotal += product.GetTotalCost();
        }

        double shippingCost = Customer.IsInUSA() ? USA_ShippingCost : International_ShippingCost;

        return productTotal + shippingCost;
    }

    // Method to get the packing label for the order
    public string GetPackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in Products)
        {
            packingLabel += $"{product.GetProductInfo()}\n";
        }
        return packingLabel;
    }

    // Method to get the shipping label for the order
    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{Customer.GetCustomerInfo()}";
    }
}

// Program Class (Main Program)
public class Program
{
    public static void Main()
    {
        // Create an Address for the customer
        Address address1 = new Address("123 Elm St.", "Springfield", "IL", "USA");
        Address address2 = new Address("456 Maple Ave.", "Toronto", "ON", "Canada");

        // Create Customers
        Customer customer1 = new Customer("John Doe", address1);
        Customer customer2 = new Customer("Jane Smith", address2);

        // Create Products
        Product product1 = new Product("Laptop", 101, 999.99, 1);
        Product product2 = new Product("Mouse", 102, 25.99, 2);
        Product product3 = new Product("Keyboard", 103, 75.50, 1);

        // Create Orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        Order order2 = new Order(customer2);
        order2.AddProduct(product2);
        order2.AddProduct(product3);

        // Display details for each order
        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    // Method to display the order details
    private static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"Total Price: ${order.GetTotalPrice():0.00}\n");
    }
}
