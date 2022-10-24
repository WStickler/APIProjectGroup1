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
        private ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _service.GetCustomersAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/Search?searchterm=Karl (Searches if "Karl" is in customerId, contactName.
        [HttpGet("Search")]
        public async Task<List<Customer>> GetCustomerBySearch(string searchTerm = "")
        {
            var customerList = await _service.GetCustomerBySearchTerm(searchTerm);

            if (customerList == null)
            {
                return new List<Customer>();
            }

            return customerList;
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            [HttpGet("CustomersWithMostorders")]
            public async Task<ActionResult<List<CustomerDTO>>> GetCustomersWithMostOrders(int n)
            {
                var customers = await _service.GetCustomersWithMostOrders(n);
                return customers.Select(x => Utils.CustomerToDTO(x)).ToList();
            }
        }
}
