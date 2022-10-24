using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class Tests
    {
        private NorthwindContext _context;
        private ICustomerService _cService;
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase(databaseName: "NorthwindDB").Options;
            _context = new NorthwindContext(options);
            _cService = new CustomerService(_context);
            _context.Add(new Customer { CustomerId = "WILLS", CompanyName = "Sparta Global", City = "London", Country = "UK" });
        }

        [Test]
        public void RemoveCustomerAsync_RemovesCustomerFromCustomers()
        {
            var customerToDelete = _cService.GetCustomerByIdAsync("WILLS").Result;
            _cService.RemoveCustomerAsync(customerToDelete).Wait();

            Assert.That(_context.Customers.Count(), Is.EqualTo(0));
            Assert.That(_cService.GetCustomerByIdAsync("WILLS").Result, Is.Null);

            //clean up
            _context.Add(new Customer { CustomerId = "WILLS", CompanyName = "Sparta Global", City = "London", Country = "UK" });
        }
    }
}