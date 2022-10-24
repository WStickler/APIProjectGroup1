using APIProjectGroup1.Controllers;
using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
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
        [Ignore("Not implemented yet")]
        [Category("Create")]
        [Category("Happy Path")]
        [Test]
        public void ReturnTrueWhenCustomerIsCreated()
        {
            var mockObject = new Mock<ICustomerService>();
            _controller = new CustomersController(mockObject.Object);
            mockObject.Setup(x =>
            x.CreateCustomerAsync(It.IsAny<Customer>()))
                .Returns(Task.FromResult(new Customer()));
        }
    }
}
