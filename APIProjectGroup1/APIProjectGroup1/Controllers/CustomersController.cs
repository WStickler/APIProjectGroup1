using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIProjectGroup1.Models;
using APIProjectGroup1.Services;
<<<<<<< HEAD
=======
using APIProjectGroup1.Models.DTOs;
>>>>>>> ameer

namespace APIProjectGroup1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
<<<<<<< HEAD
        private readonly NorthwindContext _context;
        private readonly ICustomerService _service;

        public CustomersController(NorthwindContext context)
=======
        private readonly ICustomerService _service;
        
        public CustomersController(ICustomerService service)
>>>>>>> ameer
        {
            _service = service;
        }

       
        [HttpGet("CustomersWithMostorders")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomersWithMostOrders(int n)
        {
            var customers = await _service.GetCustomersWithMostOrders(n);
            return customers.Select(x => Utils.CustomerToDTO(x)).ToList();
        }

<<<<<<< HEAD
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(string id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
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

            await _service.RemoveCustomerAsync(customer);

            return NoContent();
        }

        private bool CustomerExists(string id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
=======
>>>>>>> ameer
    }
}
