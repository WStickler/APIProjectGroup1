using APIProjectGroup1.Models;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
        List<Customer> GetCustomers();
        Task<Customer> GetCustomerByIdAsync(string CustomerId);

        Task CreateCustomerAsync(Customer c);

        Task SaveCustomerChangesAsync();

        Task RemoveCustomerAsync(Customer c);
    }
}
