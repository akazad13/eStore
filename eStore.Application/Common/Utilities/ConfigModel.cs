namespace eStore.Application.Common.Utilities
{
    public class ConfigModel
    {
        public required Jwt Jwt { get; set; }
        public required Aws Aws { get; set; }
    }

    public class Jwt
    {
        public string SigningSecret { get; set; } = string.Empty;

        public int ExpiryDuration { get; set; } = 120;

        public string ValidIssuer { get; set; } = string.Empty;

        public string ValidAudience { get; set; } = string.Empty;
    }

    public class Aws
    {
        public string ImageBucketName { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string PublicKey { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;
        public string ServiceURL { get; set; } = string.Empty;
    }
}
