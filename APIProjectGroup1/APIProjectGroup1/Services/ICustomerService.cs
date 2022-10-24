using APIProjectGroup1.Models;

namespace APIProjectGroup1.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(string CustomerId);
<<<<<<< HEAD
        Task CreateCustomerAsync(Customer c);
        Task<List<Customer>> GetCustomersWithMostOrders(int n);
=======
        Task<List<Customer>> GetCustomerBySearchTerm(string SearchTerm);
        Task CreateCustomerAsync(Customer c);
>>>>>>> 0d140b68d10f1c746356298cfac814c3a00faf65
        Task SaveCustomerChangesAsync();
        Task RemoveCustomerAsync(Customer c);

        bool CustomerExists(string id);
    }
}
