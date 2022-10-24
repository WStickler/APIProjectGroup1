using APIProjectGroup1.Models;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string CustomerId);
        Task<List<Customer>> GetCustomerBySearchTerm(string SearchTerm);
        Task CreateCustomerAsync(Customer c);
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);
    }
}
