using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Shop
{
    internal static class Program
    {
        private const string ConnectionString =
            @"Data Source=DESKTOP-PUQ06I7\SQLEXPRESS;Initial Catalog=shop;Pooling=true;Integrated Security=SSPI";

        private static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var command = args[0];

                if (command == "selectCustomers")
                {
                    var customers = SelectCustomers();
                    foreach (var customer in customers) Console.WriteLine(customer.Name);
                }
                else if (command == "selectOrders")
                {
                    var orders = SelectOrders();
                    foreach (var order in orders) Console.WriteLine($"{order.ProductName} - {order.Price}");
                }
                else if (command == "insertCustomer")
                {
                    var createdCustomerId = InsertCustomer("Роман", "Йошкар-Ола");
                    Console.WriteLine($"Created customer: {createdCustomerId}");
                }
                else if (command == "insertOrder")
                {
                    var createdOrderId = InsertOrder(1, "Шоколад", 600);
                    Console.WriteLine($"Created order: {createdOrderId}");
                }
                else if (command == "updateCustomer")
                {
                    UpdateCustomer(1, "Макс");
                }
                else if (command == "updateOrderPrice")
                {
                    UpdateOrderPrice(2, 100);
                }
                else if (command == "customersCount")
                {
                    var customersCount = CalculateCustomersCount();
                    Console.WriteLine($"Customers count: {customersCount}");
                }
                else if (command == "ordersCount")
                {
                    var ordersCount = CalculateOrdersCount();
                    Console.WriteLine($"Orders count: {ordersCount}");
                }
                else if (command == "ordersSum")
                {
                    var ordersSum = CalculateOrdersSumPerCustomer();
                    foreach (var item in ordersSum) Console.WriteLine($"{item.CustomerName} - {item.OrderPrice}");
                }
            }
        }

        private static List<OrdersPriceSum> CalculateOrdersSumPerCustomer()
        {
            var ordersPriceSumPerCustomerList = new List<OrdersPriceSum>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT [Customer].[Name], SUM([Order].[Price]) AS OrdersPriceSum
                                            FROM [Order]
                                            RIGHT JOIN [Customer] ON [Customer].[CustomerId] = [Order].[CustomerId]
                                            GROUP BY [Customer].[Name]
                                            ORDER BY OrdersPriceSum DESC";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ordersPriceSum = new OrdersPriceSum
                            {
                                CustomerName = Convert.ToString(reader["Name"]),
                                OrderPrice = Convert.ToInt32(reader["OrdersPriceSum"] != DBNull.Value 
                                    ? reader["OrdersPriceSum"] 
                                    : "0")
                            };
                            ordersPriceSumPerCustomerList.Add(ordersPriceSum);
                        }
                    }
                }
            }

            return ordersPriceSumPerCustomerList;
        }

        private static int CalculateCustomersCount()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT COUNT(*)
                                        AS CustomersCount
                                        FROM [Customer]";

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private static int CalculateOrdersCount()
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT COUNT(*)
                                        AS OrdersCount
                                        FROM [Order]";


                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private static List<Customer> SelectCustomers()
        {
            var customers = new List<Customer>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT [CustomerId], [Name], [City]
                                            FROM [Customer]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var customer = new Customer
                            {
                                CustomerId = Convert.ToInt32(reader["CustomerId"]),
                                Name = Convert.ToString(reader["Name"]),
                                City = Convert.ToString(reader["City"])
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }

            return customers;
        }

        private static List<Order> SelectOrders()
        {
            var orders = new List<Order>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT [OrderId], [ProductName], [Price], [CustomerId]
                                            FROM [Order]";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var order = new Order
                            {
                                OrderId = Convert.ToInt32(reader["OrderId"]),
                                ProductName = Convert.ToString(reader["ProductName"]),
                                Price = Convert.ToInt32(reader["Price"]),
                                CustomerId = Convert.ToInt32(reader["CustomerId"])
                            };
                            orders.Add(order);
                        }
                    }
                }
            }

            return orders;
        }

        private static int InsertCustomer(string name, string city)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [Customer] (
                                        [Name],
                                        [City])
                                        VALUES (
                                        @name, 
                                        @city)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;
                    cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = city;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private static int InsertOrder(int customerId, string productName, int price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO [Order] (
                                        [CustomerId],
                                        [ProductName],
                                        [Price])
                                        VALUES (
                                        @customerId,
                                        @productName,
                                        @price)
                                        SELECT SCOPE_IDENTITY()";

                    cmd.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;
                    cmd.Parameters.Add("@productName", SqlDbType.NVarChar).Value = productName;
                    cmd.Parameters.Add("@price", SqlDbType.Int).Value = price;

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        private static void UpdateCustomer(int customerId, string name)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Customer]
                                            SET [Name] = @name
                                            WHERE [CustomerId] = @customerId";

                    command.Parameters.Add("@customerId", SqlDbType.Int).Value = customerId;
                    command.Parameters.Add("@name", SqlDbType.NVarChar).Value = name;

                    command.ExecuteNonQuery();
                }
            }
        }

        private static void UpdateOrderPrice(int orderId, int price)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE [Order]
                                            SET [Name] = @price
                                            WHERE [OrderId] = @orderId";

                    command.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
                    command.Parameters.Add("@price", SqlDbType.Int).Value = price;

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}