using APIProjectGroup1.Models;
using Microsoft.EntityFrameworkCore;

namespace APIProjectGroup1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context;
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

        public bool CustomerExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(string CustomerId)
        {
            return await _context.Customers.FindAsync(CustomerId);
        }

        public  List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
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
    }
}
