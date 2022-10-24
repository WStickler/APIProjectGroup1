using APIProjectGroup1.Models;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerByIdAsync(string CustomerId);
        Task CreateCustomerAsync(Customer c);
        Task<List<Customer>> GetCustomersWithOrders();
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);

        bool CustomerExists(string id);
    }
}
