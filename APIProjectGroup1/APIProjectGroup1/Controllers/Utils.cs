using APIProjectGroup1.Models;
using APIProjectGroup1.Models.DTOs;

namespace APIProjectGroup1.Controllers
{
    public class Utils
    {
        public static CustomerDTO CustomerToDTO(Customer x) =>
            new CustomerDTO
            {
                Id = x.CustomerId,
                CompanyName = x.CompanyName,
                ContactTitle = x.ContactTitle,
                Country = x.Country,
                TotalOrder = x.Orders.Count(),
                Orders = x.Orders.Select(x => OrderToDTO(x)).ToList()

            };
        public static OrderDTO OrderToDTO(Order x) =>
            new OrderDTO
            {
                OrderID = x.OrderId,
                OrderDate = x.OrderDate,
                ShipDate = x.ShippedDate
            };
    }
}
