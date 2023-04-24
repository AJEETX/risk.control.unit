namespace WebApplication1.Models.ViewModel
{
    public class UsersViewModel
    {
        public string UserId { get; set; }
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int? StateId { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public int? CountryId { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
