﻿using APIProjectGroup1.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
<<<<<<< HEAD
=======
        Task<List<Customer>> GetCustomersAsync();
>>>>>>> dev
        Task<Customer> GetCustomerByIdAsync(string CustomerId);
        Task<List<Customer>> GetCustomerBySearchTerm(string SearchTerm);
        Task CreateCustomerAsync(Customer c);
        Task<List<Customer>> GetCustomersWithMostOrders(int n);
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);

        bool CustomerExists(string id);
<<<<<<< HEAD
        Task<List<Customer>> GetCustomersAsync();
=======

>>>>>>> dev
    }
}
