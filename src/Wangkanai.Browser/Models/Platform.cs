namespace Wangkanai.Browser
{
    public class Platform
    {
        public string Name { get; set; }        
        public Version Version { get; set; }
        public byte Bits { get; set; }
        public PlatformFamily Family { get; set; }
    }
}