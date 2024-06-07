using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Product_Menegistion;

class Program
{
    public static void Main(string[] args)
    {
        IProductRepository productRepository = new ProductService();
        Menu(productRepository);
    }

    static void Menu(IProductRepository productRepository)
    {
        while (true)
        {
            Console.WriteLine("Product Management");
            Console.WriteLine("1. List All Products");
            Console.WriteLine("2. Create Product");
            Console.WriteLine("3. Read Product");
            Console.WriteLine("4. Update Product");
            Console.WriteLine("5. Delete Product");
            Console.WriteLine("6. Exit");
            Console.WriteLine("Select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ListAllProducts(productRepository);
                    break;
                case "2":
                    CreateProduct(productRepository);
                    break;
                case "3":
                    ReadProduct(productRepository);
                    break;
                case "4":
                    UpdateProduct(productRepository);
                    break;
                case "5":
                    DeleteProduct(productRepository);
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid Choice. Please try again.");
                    break;
            }
        }
    }

    static void ListAllProducts(IProductRepository productRepository)
    {
        try
        {
            List<Product> products = productRepository.GetAll();
            foreach (var product in products)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error fetching products: " + ex.Message);
            // Optionally log the exception for further investigation
        }
    }

    static void CreateProduct(IProductRepository productRepository)
    {
        Console.WriteLine("Enter product name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter product price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Enter product description: ");
        string description = Console.ReadLine();

        Product newProduct = new Product
        {
            Name = name,
            Price = price,
            Description = description
        };
        productRepository.Create(newProduct);
        Console.WriteLine("Product created successfully!");
    }

    static void ReadProduct(IProductRepository productRepository)
    {
        Console.WriteLine("Enter product ID: ");
        int id = int.Parse(Console.ReadLine());

        try
        {
            Product product = productRepository.Read(id);
            if (product != null)
            {
                Console.WriteLine($"ID: {product.Id}, Name: {product.Name}, Price: {product.Price}, Description: {product.Description}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error reading product: " + ex.Message);
        }
    }

    static void UpdateProduct(IProductRepository productRepository)
    {
        Console.WriteLine("Enter product ID to update: ");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter new product name: ");
        string name = Console.ReadLine();
        Console.WriteLine("Enter new product price: ");
        decimal price = decimal.Parse(Console.ReadLine());
        Console.WriteLine("Enter new product description: ");
        string description = Console.ReadLine();

        Product updatedProduct = new Product
        {
            Id = id,
            Name = name,
            Price = price,
            Description = description
        };

        try
        {
            productRepository.Update(updatedProduct);
            Console.WriteLine("Product updated successfully!");
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error updating product: " + ex.Message);
        }
    }

    static void DeleteProduct(IProductRepository productRepository)
    {
        Console.WriteLine("Enter product ID to delete: ");
        int id = int.Parse(Console.ReadLine());

        try
        {
            productRepository.Delete(id);
            Console.WriteLine("Product deleted successfully!");
        }
        catch (MySqlException ex)
        {
            Console.WriteLine("Error deleting product: " + ex.Message);
        }
    }
}
