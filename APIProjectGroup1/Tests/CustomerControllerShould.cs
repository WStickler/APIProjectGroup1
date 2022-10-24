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

            Assert.That(result.Value, Is.EqualTo(customer));
        }
        [Category("Get Customer")]
        [Category("Sad Path")]
        [Test]
        public async Task GetCustomer_GivenNull_ReturnsNull()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns((Customer)null);

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
            x.GetCustomerBySearchTerm(It.IsAny<string>()).Result)
                .Returns(new List<Customer>() { customer, customer2 });

            var result = await _controller.GetCustomerBySearch(It.IsAny<string>());

            Assert.That(result, Is.EqualTo(new List<Customer>() { customer, customer2 }));
            Assert.That(result, Is.TypeOf<List<Customer>>());
        }
        [Category("Get Customer By Search")]
        [Category("Sad Path")]
        [Test]
        public async Task GetCustomerBySearch_GivenIncorrect_ReturnsNull()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerBySearchTerm(It.IsAny<string>()).Result)
                .Returns((List<Customer>)null);

            var result = await _controller.GetCustomerBySearch(It.IsAny<string>());

            Assert.That(result, Is.Null);
        }
        [Ignore("Not implemented")]
        [Category("Post Customer")]
        [Category("Happy Path")]
        [Test]
        public async Task GivenCustomer_WhichIsNotInTheDatabase_ReturnsCreatedAtAction()
        {
            var mockObject = new Mock<ICustomerService>();
            var customer = new Customer() { CustomerId = "SERG", ContactName = "Sergiusz Pietrala" };
            _controller = new CustomersController(mockObject.Object);
            //mockObject.Setup(x =>
            //x.CreateCustomerAsync(It.IsAny<Customer>()))
            //    .Returns();
        }
    }
}
