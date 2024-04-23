namespace eStore.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryResponse
    {
        public long Id { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? ImageSource { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Locale { get; set; }
        public DateTime? Birthdate { get; set; }
        public string? Gender { get; set; }
        public string? JWT { get; set; }
    }
}
