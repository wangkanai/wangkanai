namespace Wangkanai.Webmaster;

public static class AppSetting
{
    public static class Connection
    {
        public const string Sql = "DefaultConnection";
    }

    public static class AzureAd
    {
        public const string Instance     = "Instance";
        public const string Domain       = "Domain";
        public const string TenantId     = "TenantId";
        public const string ClientId     = "ClientId";
        public const string CallbackPath = "CallbackPath";
    }

    public static class SendGrid
    {
        public const string Key   = "Key";
        public const string Email = "Email";
        public const string Name  = "Name";
    }

    public static class Recaptcha
    {
        public const string Key    = "Key";
        public const string Secret = "Secret";
    }

    public static class Google
    {
        public const string ClientId = "ClientId";
        public const string Secret   = "Secret";
    }

    public static class LinkedIn
    {
        public const string ClientId = "ClientId";
        public const string Secret   = "Secret";
    }

    public static class Facebook
    {
        public const string ClientId = "ClientId";
        public const string Secret   = "Secret";
    }
}