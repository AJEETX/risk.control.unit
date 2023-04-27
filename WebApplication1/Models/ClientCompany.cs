namespace WebApplication1.Models
{
    public class ClientCompany
    {
        public string ClientCompanyId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address Address { get; set; } 

    }
    public class Address
    {
        public string Branch { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public State? State { get; set; }
        public Country? Country { get; set; }
        public PinCode? PinCode { get; set; }
    }
}
