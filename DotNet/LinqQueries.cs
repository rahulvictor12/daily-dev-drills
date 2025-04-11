namespace ConsoleApp1
{
	using System;
	using System.Linq;
	using System.Collections.Generic;

	public class Customer
	{
		// Auto Implemented Properties
		public int CustomerId { get; set; }
		public string CustomerName { get; set; }
	}

	public class Order
	{
		// Auto Implemented Properties
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public string Product { get; set; }
	}

	public static class Program
	{
		static void Main(string[] args)
		{
			List<Customer> customers = new List<Customer>(){
				new Customer(){CustomerId = 1, CustomerName = "Rahul"},
				new Customer(){CustomerId = 2, CustomerName = "Anish"},
				new Customer(){CustomerId = 3, CustomerName = "Anvesh"},
				new Customer(){CustomerId = 4, CustomerName = "Ajay"}
			};

			List<Order> orders = new List<Order>
			{
				new Order { OrderId = 1, CustomerId = 1, Product = "Laptop" },
				new Order { OrderId = 2, CustomerId = 1, Product = "Mouse" },
				new Order { OrderId = 3, CustomerId = 2, Product = "Keyboard" }
			};

			// Write a LINQ query (both query syntax and method syntax) to retrieve the list of customers who have not placed any orders.
			var customer = (from cust in customers
							where orders.All(o => o.CustomerId != cust.CustomerId)
							select cust.CustomerName);

			Console.WriteLine($"Customer Who Doesn't have any Orders : {string.Join(" , ", customer)}");

			// Write a LINQ query (method syntax) to find the customer(s) with the highest number of orders.
			var topOrders = orders.GroupBy(x => x.CustomerId).Select(s => new
			{
				CustomerId = s.Key,
				OrderCount = s.Count(),
			}).OrderBy(x => x.OrderCount).ToList();

			var customerWithTopOrders = customers.Where(x => x.CustomerId == topOrders.First().CustomerId).First();

			Console.WriteLine("----------------------------------------------");
			Console.WriteLine($"Customer With Top Orders : {customerWithTopOrders.CustomerName}");

			// Write a LINQ query to retrieve all customers who have placed at least one order.
			var customersWithAtleastOneOrder = customers.Where(x => orders.Any(o => o.CustomerId == x.CustomerId)).ToList();

			Console.WriteLine("----------------------------------------------");
			Console.WriteLine("Customers With Atleast One Order.");
			foreach (var c in customersWithAtleastOneOrder)
			{
				Console.WriteLine($" {c.CustomerId} : {c.CustomerName} ");
			}

			// Write a LINQ query to join customers with their corresponding orders and display Customer Name and Product.
			var customersAndOrders = from cst in customers
									 join ord in orders on cst.CustomerId equals ord.CustomerId
									 select new { cst.CustomerName, ord.Product };

			Console.WriteLine("----------------------------------------------");
			Console.WriteLine("Customers Along with their Orders.");
			foreach (var cstord in customersAndOrders)
			{
				Console.WriteLine($"{cstord.CustomerName} - {cstord.Product}");
			}

			// Write a LINQ query to group orders by customer name and list each customer's products.
			var customerProducts = from csto in customers
								   join ordr in orders on csto.CustomerId equals ordr.CustomerId
								   group ordr by csto.CustomerName into g
								   select new
								   {
									   Customer = g.Key,
									   Products = g.Select(x => x.Product)
								   };

			Console.WriteLine("----------------------------------------------");
			Console.WriteLine("Customers Along with their Orders(comma seperated).");
			foreach (var cp in customerProducts)
			{
				Console.WriteLine($"Customer : {cp.Customer} - Products : {string.Join(",", cp.Products)}");
			}

			// Write a LINQ query to return customers who ordered a product named "Keyboard".
			var cKeyboard = customers.Where(x => x.CustomerId == orders.Where(x => x.Product == "Keyboard").First().CustomerId).ToList();
			Console.WriteLine("----------------------------------------------");
			Console.WriteLine("Customers Who Brought KeyBoard.");
			Console.WriteLine($"Customers : {string.Join(" ", cKeyboard.Select(x => x.CustomerName))}");
		}
	}
}
