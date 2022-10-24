using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class Tests
    {
        private NorthwindContext _context;
        private CustomerService _service;
        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase(databaseName: "Westwind")
                .Options;
            _context = new NorthwindContext(options);
            _service = new CustomerService(_context);

            Customer customer1 = new Customer()
            {
                CustomerId = "SERG",
                ContactName = "Sergiusz Pietrala",
                CompanyName = "Sparta",
                Phone = "01111111111",
                ContactTitle = "Mr",
                City = "Aberystwyth",
                Country = "UK",
            };

            Customer customer2 = new Customer()
            {
                CustomerId = "SYED",
                ContactName = "Syed Ahmed",
                CompanyName = "Sparta",
                Phone = "02222222222",
                ContactTitle = "Mr",
                City = "Birmingham",
                Country = "UK",
            };
            _context.Customers.Add(customer1);
            _context.Customers.Add(customer2);
            _context.SaveChanges();
        }
        [Category("Add Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task WhenAddingCustomer_IncreaseTotalByOneAsync()
        {
            var customersBeforeAdding = _context.Customers.Count();
            var customerToAdd = new Customer()
            {
                CompanyName = "Sparta",
                ContactName = "Peter Bellaby",
                CustomerId = "PETE",
                ContactTitle = "Mr",
                Country = "UK"
            };
            await _service.AddCustomer(customerToAdd);
            var customersAfterAdding = _context.Customers.Count();
            Assert.That(customersBeforeAdding + 1, Is.EqualTo(customersAfterAdding));

            _context.Customers.Remove(customerToAdd);
            _context.SaveChanges();
        }
        [Category("Add Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task WhenAddingCustomer_CustomerIsInTheDatabase()
        {
            var customerToAdd = new Customer()
            {
                CompanyName = "Sparta",
                ContactName = "Peter Bellaby",
                CustomerId = "PETE",
                ContactTitle = "Mr",
                Country = "UK"
            };
            await _service.AddCustomer(customerToAdd);
            Assert.That(_context.Customers.Contains(customerToAdd));

            _context.Customers.Remove(customerToAdd);
            _context.SaveChanges();
        }
        [Category("Delete Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task WhenDeletingCustomer_DecreaseTotalByOneAsync()
        {
            var customersBeforeDeletion = _context.Customers.Count();
            var customerToDelete = _context.Customers.Find("SERG");
            await _service.RemoveCustomerAsync(customerToDelete);
            var customersAfterDeletion = _context.Customers.Count();

            Assert.That(customersBeforeDeletion - 1, Is.EqualTo(customersAfterDeletion));
            _context.Customers.Add(customerToDelete);
            _context.SaveChanges();
        }
        [Category("Delete Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task WhenDeletingCustomer_CustomerIsNotInTheDatabase()
        {
            var customerToDelete = _context.Customers.Find("SERG");
            await _service.RemoveCustomerAsync(customerToDelete);

            Assert.That(!_context.Customers.Contains(customerToDelete));

            _context.Customers.Add(customerToDelete);
            _context.SaveChanges();
        }
        [Category("Get Customer By Id")]
        [Category("Happy Path")]
        [Test]
        public async Task GetCustomerById_ReturnsCorrectCustomer()
        {
            var customerContext = _context.Customers.Find("SERG");
            var customerService = _service.GetCustomerByIdAsync("SERG");

            Assert.That(customerContext, Is.EqualTo(customerService));
        }
        [Category("Get Customer By Id")]
        [Category("Sad Path")]
        [Test]
        public async Task GetIncorrectCustomerById_ReturnsNull ()
        {
            var customerService = _service.GetCustomerByIdAsync("SERG");

            Assert.That(customerService, Is.Null);
        }
        [Category("CustomerExists")]
        [Category("Happy Path")]
        [Test]
        public void CustomerExists_ReturnsTrueIfCustomerExists()
        {
            Assert.That(_service.CustomerExists("SERG"));
        }
        [Category("CustomerExists")]
        [Category("Sad Path")]
        [Test]
        public void CustomerExists_ReturnsFalseIfCustomerDoesntExist()
        {
            Assert.That(!_service.CustomerExists("NOPE"));
        }
    }
}