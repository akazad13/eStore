namespace Exino.Application.CQRS.AppUser.DTOs
{
    public class AuthResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JWT { get; set; }
    }
}
