using Domain.Data;
using Domain.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Repositories;

public class OrderRepository
{
    public async Task AddOrder(Order order)
    {
        using var con = Database.Connection();
        con.Open();
        var query = 
        "insert into orders(customerId , amount , createdAt) values (@customerId , @amount , @createdAt)";
        await con.ExecuteAsync(query, order);
    }

    public async Task<Order> GetOrderById(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select * from orders where id = @id";
        var OrderbyId = await con.QuerySingleAsync<Order>(query, new { id });
        return OrderbyId;
    }

    public async Task<List<Order>> GetAllOrdersOfClient()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select * from orders where customerId = @customerId";
        var orders = await con.QueryAsync<Order>(query);
        return orders.ToList();
    }

    public async Task DeleteOrder(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var quert = "delete from orders where id = @id";
        await con.ExecuteAsync(quert, new{id});
    }

    public async Task<List<Order>> GetOrdersWithJoin()
    {
        using var con = Database.Connection();
        con.Open();
        var query = @"
        select o.id , c.fullname , o.amount , o.createdAt
        from orders o 
        left join customers c on o.customerId = c.id";
        var orders = await con.QueryAsync<Order>(query);
        return orders.ToList();
    }

    public async Task<decimal> SumOfOrders()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select sum(amount) from orders";
        var total = await con.ExecuteScalarAsync<decimal>(query);
        return total;
    }
}
