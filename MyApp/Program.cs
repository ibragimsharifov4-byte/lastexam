
using Domain.Models;
using Infrastructure.Repositories;

var customerRepo = new CustomerRepository();
var orderRepo = new OrderRepository();

while (true)
{
    Console.WriteLine("1. Add Customer");
    Console.WriteLine("2. Get All Customers");
    Console.WriteLine("3. Get Customer By Id");
    Console.WriteLine("4. Delete Customer");
    Console.WriteLine("5. Count Customers");
    Console.WriteLine("6. Add Order");
    Console.WriteLine("7. Get Order By Id");
    Console.WriteLine("8. Delete Order");
    Console.WriteLine("9. Sum Of Orders");
    Console.WriteLine("0. Exit");
    Console.Write("Choose: ");

    var choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                Console.Write("Enter fullname: ");
                var fullname = Console.ReadLine()!;

                await customerRepo.AddCustomer(new Customer
                {
                    Fullname = fullname
                });
                Console.WriteLine("Customer added successfully!");
                break;

            case "2":
                var customers = await customerRepo.GetAllCustomers();
                foreach (var customer in customers)
                {
                    Console.WriteLine($"{customer.Id} - {customer.Fullname}");
                }
                break;

            case "3":
                Console.Write("Enter customer id: ");
                int customerId = int.Parse(Console.ReadLine()!);
                var customerById = await customerRepo.GetCustomerById(customerId);
                Console.WriteLine($"{customerById.Id} - {customerById.Fullname}");
                break;

            case "4":
                Console.Write("Enter customer id: ");
                int deleteCustomerId = int.Parse(Console.ReadLine()!);
                await customerRepo.DeleteCustomer(deleteCustomerId);
                Console.WriteLine("Customer deleted!");
                break;

            case "5":
                var count = await customerRepo.CountOfCustomers();
                Console.WriteLine($"Total customers: {count}");
                break;

            case "6":
                Console.Write("Enter customer id: ");
                int orderCustomerId = int.Parse(Console.ReadLine()!);
                Console.Write("Enter amount: ");
                decimal amount = decimal.Parse(Console.ReadLine()!);
                await orderRepo.AddOrder(new Order
                {
                    CustomerId = orderCustomerId,
                    Amount = amount,
                    CreatedAt = DateTime.Now
                });
                Console.WriteLine("Order added successfully!");
                break;

            case "7":
                Console.Write("Enter order id: ");
                int orderId = int.Parse(Console.ReadLine()!);
                var order = await orderRepo.GetOrderById(orderId);
                Console.WriteLine($"Id: {order.Id}");
                Console.WriteLine($"Customer Id: {order.CustomerId}");
                Console.WriteLine($"Amount: {order.Amount}");
                Console.WriteLine($"Created At: {order.CreatedAt}");
                break;

            case "8":
                Console.Write("Enter order id: ");
                int deleteOrderId = int.Parse(Console.ReadLine()!);
                await orderRepo.DeleteOrder(deleteOrderId);
                Console.WriteLine("Order deleted!");
                break;

            case "9":
                var total = await orderRepo.SumOfOrders();
                Console.WriteLine($"Total amount: {total}");
                break;

            case "0":
                Console.WriteLine("Goodbye!");
                return;
        }
}