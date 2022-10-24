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

       
        [HttpGet("CustomersWithMostorders")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomersWithMostOrders(int n)
        {
            var customers = await _service.GetCustomersWithMostOrders(n);
            return customers.Select(x => Utils.CustomerToDTO(x)).ToList();
        }

    }
}
