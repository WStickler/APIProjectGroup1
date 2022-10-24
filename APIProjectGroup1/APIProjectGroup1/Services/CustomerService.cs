using APIProjectGroup1.Models;

namespace APIProjectGroup1.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly NorthwindContext _context;
        public CustomerService()
        {
            _context = new NorthwindContext();
        }
        public CustomerService(NorthwindContext context)
        {
            _context = context;
        }
    }
}
