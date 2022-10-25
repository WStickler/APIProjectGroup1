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
        [Category("Create")]
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
        [Category("Create")]
        [Category("Sad Path")]
        [Test]
        public async Task GetCustomer_GivenNull_ReturnsError()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.GetCustomerByIdAsync(It.IsAny<string>()).Result)
                .Returns((Customer) null);

            var result = await _controller.GetCustomer(It.IsAny<string>());

            Assert.That(result.Value, Is.TypeOf<Customer>());
        }
    }
}
