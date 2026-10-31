using Domain.Data;
using Domain.Models;
using Dapper;
using Npgsql;

namespace Infrastructure.Repositories;

public class CustomerRepository
{
    public async Task AddCustomer(Customer customer)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "insert into customers(fullaname) values (@fullname)";
        await con.ExecuteAsync(query, new{fullname = customer.Fullname});
    }

    public async Task<Customer> GetCustomerById(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select * from customers where id = @id";
        var customerbyId = await con.QuerySingleAsync<Customer>(query, new { id });
        return customerbyId;
    }

    public async Task<List<Customer>> GetAllCustomers()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select * from customers";
        var customers = await con.QueryAsync<Customer>(query);
        return customers.ToList();
    }

    public async Task DeleteCustomer(int id)
    {
        using var con = Database.Connection();
        con.Open();
        var quert = "delete from customers where id = @id";
        await con.ExecuteAsync(quert, new{id});
    }

    public async Task<int> CountOfCustomers()
    {
        using var con = Database.Connection();
        con.Open();
        var query = "select count(*) from customers";
        var count = await con.ExecuteScalarAsync<int>(query);
        return count;
    }

    public async Task<List<Customer>> GetCustomersByOrderAmount()
    {
        using var con = Database.Connection();
        con.Open();
        var query = @"
        select c.id, c.fullname, sum(o.amount) as total_amount, count(o.id) as orders_count
        from customers c
        join orders o on o.customer_id = c.id
        group by c.id, c.fullname
        order by total_amount desc
        limit 3";
        var customers = await con.QueryAsync<Customer>(query);
        return customers.ToList();
    }

}
