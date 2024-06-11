﻿using System;
using System.Collections.Generic;
using MyBussinessApplication.controller;
using MyBussinessApplication.model;
using MyBussinessApplication.service;

namespace MyBussinessApplication
{
    class Program
    {
        public static string connString = "Server=127.0.0.1;Database=prodb;User=root;Password=";

        static void Main(string[] args)
        {
            IProductRepository productRepository = new ProductService(connString);
            ProductController productController = new ProductController(productRepository);
            IOrderRepository orderRepository = new OrderService(connString);
            OrderController orderController = new OrderController(orderRepository);
            while (true)
            {
                Console.WriteLine("My Business");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Show Product");
                Console.WriteLine("3. Add Order");
                Console.WriteLine("4. Show All Orders");
                Console.WriteLine("5. Update Order");
                Console.WriteLine("6. Get Order by id");
                Console.WriteLine("7. Exit");

                Console.WriteLine("Enter your choice");
                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter product name: ");
                        string name = Console.ReadLine();

                        Console.WriteLine("Enter product price: ");
                        decimal price = decimal.Parse(Console.ReadLine());

                        Console.WriteLine("Enter product description: ");
                        string description = Console.ReadLine();

                        Product product = new Product(name, price, description);
                        productController.AddProduct(product);
                        Console.WriteLine("Product added successfully!");
                        break;
                    case 2:
                        Console.WriteLine("All products");
                        List<Product> products = productController.GetProducts();
                        foreach (var product1 in products)
                        {
                            Console.WriteLine($"Id: {product1.Id}, Name: {product1.Name}, Description: {product1.Description}");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Enter customer id: ");
                        int customerId = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter status: ");
                        string status = Console.ReadLine();
                        Console.WriteLine("Enter number of products: ");
                        int numberOfProducts = Convert.ToInt32(Console.ReadLine());
                        List<Orderdetail> orderDetails = new List<Orderdetail>();
                        for (int i = 0; i < numberOfProducts; i++)
                        {
                            Console.WriteLine($"Enter product id for product {i + 1}:  ");
                            int productId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine($"Enter quantity: ");
                            int quantity = Convert.ToInt32(Console.ReadLine());
                            orderDetails.Add(new Orderdetail { ProductId = productId, Quantity = quantity });
                        }

                        Order newOrder = new Order
                        {
                            CustomerId = customerId,
                            OrderDate = DateTime.Now,
                            Orderdetails = orderDetails,
                            Status = status
                        };
                        orderController.AddOrder(newOrder);
                        Console.WriteLine("Order added successfully");
                        break;

                    case 4:
                        List<Order> orders = orderController.GetAllOrders();
                        foreach (var order in orders)
                        {
                            Console.WriteLine($"Order id: {order.Id}, Customer Id: {order.CustomerId}, Order Date: {order.OrderDate}, Status: {order.Status}");
                            foreach (var details in order.Orderdetails)
                            {
                                Console.WriteLine($"Product id: {details.ProductId}, Quantity: {details.Quantity}");
                            }
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter order id to update: ");
                        int orderId = Convert.ToInt32(Console.ReadLine());

                        if (orderController.GetOrderById(orderId) != null)
                        {
                            Console.WriteLine("Enter status to update: ");
                            string statusUp = Console.ReadLine();

                            Order updateOrder = new Order { Id = orderId, Status = statusUp };
                            orderController.UpdateOrderById(updateOrder);
                            Console.WriteLine("Update status successfully");
                        }
                        else
                        {
                            Console.WriteLine("Not found order id!");
                        }




                        break;

                    case 6:
                        Console.WriteLine("Enter order id: ");
                        int orderIdGet = Convert.ToInt32(Console.ReadLine());
                        Order orderGet = orderController.GetOrderById(orderIdGet);
                        if (orderGet != null)
                        {
                            Console.WriteLine($"Order id: {orderGet.Id}, Customer id: {orderGet.CustomerId}, Order date: {orderGet.OrderDate}, Status: {orderGet.Status}");
                            foreach (var detailsGet in orderGet.Orderdetails)
                            {
                                Console.WriteLine($"Product id: {detailsGet.ProductId}, Quantity: {detailsGet.Quantity}");
                            }
                        }

                        break;
                    case 7:

                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}