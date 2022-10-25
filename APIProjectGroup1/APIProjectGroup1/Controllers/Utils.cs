using APIProjectGroup1.Models;
using APIProjectGroup1.Models.DTOs;

namespace APIProjectGroup1.Controllers
{
    public class Utils
    {
        public static CustomerDTO CustomerToDTO(Customer c) =>
            new CustomerDTO
            {
                Id = c.CustomerId,
                ContactTitle = c.ContactTitle,
                ContactName = c.ContactName,
                CompanyName = c.CompanyName,
                City = c.City,
                Region = c.Region,
                Country = c.Country,
                TotalOrder = c.Orders.Count(),
                Orders = c.Orders.Select(o => OrderToDTO(o)).ToList()

            };
        public static OrderDTO OrderToDTO(Order o) =>
            new OrderDTO
            {
                OrderID = o.OrderId,
                OrderDate = o.OrderDate,
                ShipDate = o.ShippedDate
            };
    }
}