namespace Exino.Application.Common.Utilities
{
    public class ConfigModel
    {
        public Jwt? Jwt { get; set; }
    }

    public class Jwt
    {
        public string? SigningSecret { get; set; }

        public int? ExpiryDuration { get; set; }

        public string? ValidIssuer { get; set; }

        public string? ValidAudience { get; set; }
    }
}
