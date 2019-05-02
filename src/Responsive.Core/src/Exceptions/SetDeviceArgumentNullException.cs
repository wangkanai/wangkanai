namespace System
{
    public class SetDeviceArgumentNullException : ArgumentNullException
    {
        public SetDeviceArgumentNullException(string paramName) : base(paramName) { }
    }
}
