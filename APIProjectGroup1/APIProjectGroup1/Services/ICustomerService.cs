using APIProjectGroup1.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string CustomerId);
        Task<List<Customer>> GetCustomerBySearchTerm(string SearchTerm);
        Task CreateCustomerAsync(Customer c);
        Task<List<Customer>> GetCustomersWithOrders();
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);
        bool CustomerExists(string id);
    }
}
