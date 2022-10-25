using APIProjectGroup1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIProjectGroup1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context
            ;
        public CustomerService()
        {
            _context = new NorthwindContext();
        }
        public CustomerService(NorthwindContext context)
        {
            _context = context;
        }
    
        public async Task CreateCustomerAsync(Customer c)
        {
            _context.Customers.Add(c);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public bool CustomerExists(string id)
        {
            return (bool)(_context.Customers?.Any(c => c.CustomerId == id));
        }

        public async Task<Customer> GetCustomerByIdAsync(string CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        public async Task<List<Customer>> GetCustomersWithOrders()
        {
            return await _context.Customers.Include(x => x.Orders).ToListAsync();
        }

        public async Task RemoveCustomerAsync(Customer c)
        {
            _context.Customers.Remove(c);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCustomerChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
