﻿using APIProjectGroup1.Models;
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

        public bool CustomerExists(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Customer> GetCustomerByIdAsync(string CustomerId)
        {
            throw new NotImplementedException();
        }

        public  List<Customer> GetCustomers()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Customer>> GetCustomersWithMostOrders(int n)
        {
           return await _context.Customers
                .Include(x=> x.Orders)
                .Take(n).OrderByDescending(x=>x.Orders)
                .Select(x=>x).ToListAsync();
        }

        public async Task RemoveCustomerAsync(Customer c)
        {
            throw new NotImplementedException();
        }

        public async Task SaveCustomerChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
