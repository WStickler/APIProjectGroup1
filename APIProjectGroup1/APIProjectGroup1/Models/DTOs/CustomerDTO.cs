namespace APIProjectGroup1.Models.DTOs
{
    public class CustomerDTO
    {
        public CustomerDTO()
        {
            Orders = new List<OrderDTO>();
        }
        public string Id { get; set; }
        public string ContactTitle { get; set; }
        public string ContactName { get; set; }
        public string? CompanyName { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int TotalOrder { get; init; }

        public virtual ICollection<OrderDTO> Orders { get; set; }
    }
}
