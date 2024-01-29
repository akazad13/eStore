namespace eStore.Application.Common.Utilities
{
    public class ConfigModel
    {
        public Jwt? Jwt { get; set; }
        public AWS? AWS { get; set; }
    }

    public class Jwt
    {
        public string? SigningSecret { get; set; }

        public int? ExpiryDuration { get; set; }

        public string? ValidIssuer { get; set; }

        public string? ValidAudience { get; set; }
    }

    public class AWS
    {
        public string? ImageBucketName { get; set; }

        public string? Region { get; set; }

        public string? PublicKey { get; set; }

        public string? SecretKey { get; set; }
        public string? ServiceURL { get; set; }
    }
}
