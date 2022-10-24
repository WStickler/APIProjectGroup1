using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
using APIProjectGroup1.Models.DTOs;

namespace APIProjectGroup1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _service;
        
        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public List<CustomerDTO> GetCustomers()
        {
            var customers = _service.GetCustomers().Result
                .Select(x=>Utils.CustomerToDTO(x)).ToList();
            return customers;
        }

        [HttpGet("MostOrders")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomersWithMostOrders(int n)
        {
            var l = await _service.GetCustomersWithOrders();
            return l.OrderByDescending(x=>x.Orders.Count)
                .Take(n).Select(x=>Utils.CustomerToDTO(x)).ToList();

        }
    }
}
