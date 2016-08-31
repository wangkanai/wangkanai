namespace Wangkanai.Browser
{
    public class Device
    {
        public string Name { get; set; }
        public string Maker { get; set; }
        public DeviceType Type { get; set; }       
        public bool IsCrawler { get; set; } // waiting for logic concept
        public string  Model { get; set; }
        public PointingMethod Pointing { get; set; }
    }
}