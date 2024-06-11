using System.Collections.Generic;
using MyBizApplication.model;
using MySql.Data.MySqlClient;

namespace MyBizApplication.sevice
{
    public class OderService : IOderRepository
    {
        private readonly string connectionString;

        public OderService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddOrder(Order order)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    MySqlCommand cmd = conn.CreateCommand();
                    cmd.Transaction = transaction;
                    cmd.CommandText = "INSERT INTO orders (customer_id, order_date) VALUES (@customerId, @orderDate)";
                    cmd.Parameters.AddWithValue("@customerId", order.CustomerId);
                    cmd.Parameters.AddWithValue("@orderDate", order.OrderDate);
                    cmd.ExecuteNonQuery();

                    int orderId = (int)cmd.LastInsertedId;

                    foreach (var detail in order.OrderDetails)
                    {
                        MySqlCommand detailCmd = conn.CreateCommand();
                        detailCmd.Transaction = transaction;
                        detailCmd.CommandText = "INSERT INTO order_details (order_id, product_id, quantity) VALUES (@orderId, @productId, @quantity)";
                        detailCmd.Parameters.AddWithValue("@orderId", orderId);
                        detailCmd.Parameters.AddWithValue("@productId", detail.ProductId);
                        detailCmd.Parameters.AddWithValue("@quantity", detail.Quantity);
                        detailCmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        public List<Order> GetAllOrders()
        {
            List<Order> orders = new List<Order>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM orders";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Order order = new Order
                        {
                            Id = reader.GetInt32("id"),
                            CustomerId = reader.GetInt32("customer_id"),
                            OrderDate = reader.GetDateTime("order_date"),
                            OrderDetails = new List<OderDetail>()
                        };
                        orders.Add(order);
                    }
                }
                foreach (var order in orders)
                {
                    MySqlCommand detailCmd = conn.CreateCommand();
                    detailCmd.CommandText = "SELECT * FROM order_details WHERE order_id = @orderId";
                    detailCmd.Parameters.AddWithValue("@orderId", order.Id);
                    using (MySqlDataReader detailReader = detailCmd.ExecuteReader())
                    {
                        while (detailReader.Read())
                        {
                            OderDetail detail = new OderDetail
                            {
                                ProductId = detailReader.GetInt32("product_id"),
                                Quantity = detailReader.GetInt32("quantity")
                            };
                            order.OrderDetails.Add(detail);
                        }
                    }
                }
            }

            return orders;
        }

        public Order GetOrderById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
