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
using System.Runtime.InteropServices;

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

        #region api/Customers
        public class GetCustomersParams
        {
            public string? ContactTitle { get; set; }
            public string? City { get; set; }
            public string? Region { get; set; }
            public string? Country { get; set; }

            public bool AreAllFieldsNull()
            {
                if (ContactTitle == null &&
                    City == null &&
                    Region == null &&
                    Country == null)
                {
                    return true;
                }
                return false;
            }
        }

        // GET: api/Customers allows for filtering by using query params i.e. api/Customer?City=London&Country=UK
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers([FromQuery] GetCustomersParams customerParams)
        {
            List<CustomerDTO> customerDtoList = new List<CustomerDTO>();
            var customerList = await _service.GetCustomersAsync();
            customerList.ForEach(c => customerDtoList.Add(Utils.CustomerToDTO(c)));

            if (customerParams.AreAllFieldsNull())
            {
                return customerDtoList;
            }

            List<CustomerDTO> outputList = new List<CustomerDTO>();
            foreach (var customer in customerDtoList) // Checks that all non-null params are true. if so adds to outputList.
            {
                bool addToOutput = true;

                if (customerParams.ContactTitle != null && !IsParamInString(customerParams.ContactTitle, customer.ContactTitle))
                    addToOutput = false;

                if (customerParams.City != null && !IsParamInString(customerParams.City, customer.City))
                    addToOutput = false;

                if (customerParams.Region != null && !IsParamInString(customerParams.Region, customer.Region))
                    addToOutput = false;

                if (customerParams.Country != null && !IsParamInString(customerParams.Country, customer.Country))
                    addToOutput = false;


                if (addToOutput)
                    outputList.Add(customer);
            }

            return outputList;
        }

        private bool IsParamInString(string param, string? customerPropString)
        {
            if (customerPropString != null && customerPropString == param)
                return true;
            else
                return false;
        }
        #endregion

        #region api/Customer/{id}
        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer(string id)
        {
            var customer = await _service.GetCustomerByIdAsync(id);
            var customerDto = Utils.CustomerToDTO(customer);

            if (customer == null)
            {
                return NotFound();
            }

            return customerDto;
        }
        #endregion

        #region api/Customer/Search
        // GET: api/Customers/Search?searchterm=Karl (Searches if "Karl" is in customerId, contactName, companyName. Case-Insensitive
        [HttpGet("Search")]
        public async Task<List<CustomerDTO>> GetCustomerBySearch(string searchTerm = "")
        {
            List<CustomerDTO> outputList = new List<CustomerDTO>();
            var customerList = await _service.GetCustomersAsync();

            if (customerList == null)
            {
                return new List<CustomerDTO>();
            }

            foreach (var customer in customerList)
            {
                var normalisedSearchTerm = searchTerm.ToLower().Trim();

                if (customer.CustomerId != null && customer.CustomerId.ToLower().Contains(normalisedSearchTerm))
                {
                    outputList.Add(Utils.CustomerToDTO(customer));
                }
                else if (customer.ContactName != null && customer.ContactName.ToLower().Contains(normalisedSearchTerm))
                {
                    outputList.Add(Utils.CustomerToDTO(customer));
                }
                else if (customer.CompanyName != null && customer.CompanyName.ToLower().Contains(normalisedSearchTerm))
                {
                    outputList.Add(Utils.CustomerToDTO(customer));
                }
            }

            return outputList;
        }
        #endregion

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(string id, CustomerDTO customerDto)
        {
            //The id in the URI has to match the URI in the JSON request body we send

            if (id != customerDto.Id)
            {
                return BadRequest();
            }

            Customer customer = await _service.GetCustomerByIdAsync(id);

            customer.CompanyName = customerDto.CompanyName ?? customer.CompanyName;
            customer.ContactName = customerDto.ContactName ?? customer.ContactName;
            customer.ContactTitle = customerDto.ContactTitle ?? customer.ContactTitle;
            customer.Country = customerDto.Country ?? customer.Country;


            try
            {
                await _service.SaveCustomerChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_service.CustomerExists(id))
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
            await _service.CreateCustomerAsync(customer);
            try
            {
                await _service.SaveCustomerChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (_service.CustomerExists(customer.CustomerId))
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

        [HttpGet("CustomersWithMostorders")]
        public async Task<ActionResult<List<CustomerDTO>>> GetCustomersWithMostOrders(int n)
        {
            var customers = _service.GetCustomersAsync().Result
                .Select(x => Utils.CustomerToDTO(x)).ToList();
            return customers;
        }
    }
}
