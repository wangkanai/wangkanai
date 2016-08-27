namespace Wangkanai.Extensions.BrowserDetection.Platforms
{
    internal class OperaMiniBrowser : DeviceBrowser
    {
        public override bool IsValid(HttpRequest request)
        {
            // opera mini special case
            if (!request.Headers.Any(header => header.Value.Any(value => value.Contains("OperaMini"))))                return false;

            DeviceInfo = DeviceBuilder.Mobile();
            return true;
        }
    }
}