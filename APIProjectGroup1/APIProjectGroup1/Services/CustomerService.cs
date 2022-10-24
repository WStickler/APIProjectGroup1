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
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public bool CustomerExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(string CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        public async Task<List<Customer>> GetCustomerBySearchTerm(string SearchTerm)
        {
            return await _context.Customers
                    .Where(c => 
                        c.CustomerId.Contains(SearchTerm) ||
                        c.ContactName.Contains(SearchTerm))
                    .ToListAsync();
        }

        public async Task<List<Customer>> GetCustomersWithMostOrders(int n)
        {
           return await _context.Customers
                .Include(x=> x.Orders)
                .OrderByDescending(x=>x.Orders.Count).Take(n)
                .Select(x=>x).ToListAsync();
        }

        public async Task RemoveCustomerAsync(Customer c)
        {
            _context.Customers.Remove(c);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCustomerChangesAsync()
        {
            throw new NotImplementedException();
        }

        public List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        
    }
}
