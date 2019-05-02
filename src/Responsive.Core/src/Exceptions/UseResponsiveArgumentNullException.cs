namespace System
{
    public class UseResponsiveArgumentNullException : ArgumentNullException
    {
        public UseResponsiveArgumentNullException(string paramName) : base(paramName) { }
    }

    public class UseResponsiveAppArgumentNullException : ArgumentNullException
    {
        public UseResponsiveAppArgumentNullException(string paramName) : base(paramName) { }
    }

    public class UseResponsiveOptionArgumentNullException : ArgumentNullException
    {
        public UseResponsiveOptionArgumentNullException(string paramName) : base(paramName) { }
    }
}
