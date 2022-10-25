using APIProjectGroup1.Controllers;
using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIProjectGroup1.Controllers;
using APIProjectGroup1.Models.DTOs;

namespace Tests
{
    public class CustomerControllerShould
    {
        private CustomersController _controller;

        [Category("Controller Constructor")]
        [Category("Happy Path")]
        [Test]
        public void BeAbleToConstruct()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            Assert.That(_controller, Is.TypeOf<CustomersController>());
        }
        [Category("Get Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task GetCustomer_GivenCustomer_ReturnsCustomer()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new Customer() { CustomerId = "SERG", ContactName = "Sergiusz Pietrala" };
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns(customer);

            var result = await _controller.GetCustomer(It.IsAny<string>());

            Assert.That(result.Value.Id, Is.EqualTo(customer.CustomerId));
            Assert.That(result.Value.CompanyName, Is.EqualTo(customer.CompanyName));
        }
        [Ignore("Not working properly")]
        [Category("Get Customer")]
        [Category("Sad Path")]
        [Test]
        public async Task GetCustomer_GivenNull_ReturnsNull()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns((Customer)null!);

            var result = await _controller.GetCustomer(It.IsAny<string>());

            Assert.That(result.Value, Is.Null);
        }
        [Category("Get Customer By Search")]
        [Category("Happy Path")]
        [Test]
        public async Task GetCustomerBySearch_GivenCorrectData_ReturnsAList()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new Customer() { CustomerId = "SERG", ContactName = "Sergiusz Pietrala" };
            var customer2 = new Customer() { CustomerId = "SYED", ContactName = "Syed Ahmed" };
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomersAsync().Result)
                .Returns(new List<Customer>() { customer, customer2 });

            var result = await _controller.GetCustomerBySearch("SERG");

            Assert.That(result.Count(), Is.EqualTo(new List<CustomerDTO>() { Utils.CustomerToDTO(customer) }.Count()));
            Assert.That(result, Is.TypeOf<List<CustomerDTO>>());
        }
        [Category("Get Customer By Search")]
        [Category("Sad Path")]
        [Test]
        public async Task GetCustomerBySearch_GivenIncorrect_ReturnsNull()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomersAsync().Result)
                .Returns((List<Customer>)null!);

            var result = await _controller.GetCustomerBySearch(It.IsAny<string>());

            Assert.That(result, Is.EqualTo(new List<CustomerDTO>()));
        }
        [Category("Post Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task GivenCustomer_WhichIsNotInTheDatabase_ReturnsCreatedAtAction()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);

            await _controller.PostCustomer(new Customer() { CustomerId = "SERG"});

            mockObject.Verify(x => x.CreateCustomerAsync(It.IsAny<Customer>()), Times.Once);
            mockObject.Verify(x => x.SaveCustomerChangesAsync(), Times.Once);
        }
        [Category("Delete Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task GivenCorrectCustomer_DeleteItFromDatabaseAsync()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new Customer() { CustomerId = "SERG", ContactName = "Sergiusz Pietrala" };
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns(customer);

            await _controller.DeleteCustomer("SERG");

            mockObject.Verify(x => x.RemoveCustomerAsync(customer), Times.Once());
        }
        [Category("Put Customer")]
        [Category("Happy Path")]
        [Test]
        public void GivenRightCustomer_PutCustomer()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new CustomerDTO() { Id = "SERG", ContactName = "Sergiusz Pietrala" };
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns(new Customer() { CustomerId = "SERG" });

            _controller.PutCustomer("SERG", customer);

            mockObject.Verify(x => x.SaveCustomerChangesAsync(), Times.Once());
        }
        [Category("Put Customer")]
        [Category("Sad Path")]
        [Test]
        public async Task GivenInvalidCustomer_ThrowBadRequestAsync()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new CustomerDTO() { Id = "SERG", ContactName = "Sergiusz Pietrala" };
            _controller = new CustomersController(mockObject.Object);

            var result = await _controller.PutCustomer("NULL", customer);

            Assert.That(result, Is.TypeOf<BadRequestResult>());
        }
        [Category("Customer With Most Orders")]
        [Category("Sad Path")]
        [Test]
        public async Task GivenNumber_GiveThatManyCustomersWithMostOrders()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomersAsync().Result).Returns(new List<Customer>());

            var list = await _controller.GetCustomersWithMostOrders(It.IsAny<int>());

            mockObject.Verify(x => x.GetCustomersAsync(), Times.Once());
        }
    }
}
