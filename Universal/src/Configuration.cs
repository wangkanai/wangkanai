using System.Configuration;

namespace Wangkanai.Universal
{
    public class Configuration : ConfigurationSection
    {
        private static readonly string path = "UniversalAnalyticSettings/Account";
        public static Configuration GetConfiguration()
        {
            Configuration config = ConfigurationManager.GetSection(path) as Configuration;
            if (config != null) return config;
            return new Configuration();
        }

        [ConfigurationProperty("account", DefaultValue = "UA-XXXX-Y", IsRequired = true)]
        public string Account
        {
            get { return (string)this["account"]; }
            set { this["account"] = value; }
        }
        [ConfigurationProperty("property", DefaultValue = "auto", IsRequired = false)]
        public string Property
        {
            get { return (string)this["property"]; }
            set { this["property"] = value; }
        }
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }
        [ConfigurationProperty("displayfeatures", IsRequired = false)]
        public bool DisplayFeatures
        {
            get { return (bool)this["displayfeatures"]; }
            set { this["displayfeatures"] = value; }
        }
        [ConfigurationProperty("forceSSL", IsRequired = false)]
        public bool ForceSSL
        {
            get { return (bool)this["forceSSL"]; }
            set { this["displayfeature"] = value; }
        }
        [ConfigurationProperty("anonymizeIp", IsRequired = false)]
        public bool AnonymizeIp
        {
            get { return (bool)this["anonymizeIp"]; }
            set { this["anonymizeIp"] = value; }
        }
        [ConfigurationProperty("sampleRate", IsRequired = false)]
        public int SampleRate
        {
            get { return (int)this["sampleRate"]; }
            set { this["sampleRate"] = value; }
        }
        [ConfigurationProperty("siteSpeedSampleRate", IsRequired = false)]
        public int SiteSpeedSampleRate
        {
            get { return (int)this["siteSpeedSampleRate"]; }
            set { this["siteSpeedSampleRate"] = value; }
        }
        [ConfigurationProperty("alwaysSendReferrer", IsRequired = false)]
        public bool AlwaysSendReferrer
        {
            get { return (bool)this["alwaysSendReferrer"]; }
            set { this["alwaysSendReferrer"] = value; }
        }
        [ConfigurationProperty("allowAnchor", IsRequired = false)]
        public bool AllowAnchor
        {
            get { return (bool)this["allowAnchor"]; }
            set { this["allowAnchor"] = value; }
        }
        [ConfigurationProperty("cookieDomain", IsRequired = false)]
        public string CookieDomain
        {
            get { return (string)this["cookieDomain"]; }
            set { this["cookieDomain"] = value; }
        }
        [ConfigurationProperty("cookieName", IsRequired = false)]
        public string CookieName
        {
            get { return (string)this["cookieName"]; }
            set { this["cookieName"] = value; }
        }
        [ConfigurationProperty("cookieExpires", IsRequired = false)]
        public int CookieExpires
        {
            get { return (int)this["cookieExpires"]; }
            set { this["cookieExpires"] = value; }
        }
        [ConfigurationProperty("legacyCookieDomain", IsRequired = false)]
        public string LegacyCookieDomain
        {
            get { return (string)this["legacyCookieDomain"]; }
            set { this["legacyCookieDomain"] = value; }
        }
        [ConfigurationProperty("EnhancedLink", IsRequired = false)]
        public bool EnhancedLink
        {
            get { return (bool)this["EnhancedLink"]; }
            set { this["EnhancedLink"] = value; }
        }
        [ConfigurationProperty("EnhancedCookieName", IsRequired = false)]
        public string EnhancedCookieName
        {
            get { return (string)this["EnhancedCookieName"]; }
            set { this["EnhancedCookieName"] = value; }
        }
        [ConfigurationProperty("EnhancedDuration", IsRequired = false)]
        public int EnhancedDuration
        {
            get { return (int)this["EnhancedDuration"]; }
            set { this["EnhancedDuration"] = value; }
        }
        [ConfigurationProperty("EnhancedLevels", IsRequired = false)]
        public int EnhancedLevels
        {
            get { return (int)this["EnhancedLevels"]; }
            set { this["EnhancedLevels"] = value; }
        }
    }
}
