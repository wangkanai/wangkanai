namespace System
{
    public class ResponsiveMiddlewareArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareArgumentNullException(string paramName) : base(paramName) { }
    }

    public class ResponsiveMiddlewareNextArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareNextArgumentNullException(string paramName) : base(paramName) { }
    }

    public class ResponsiveMiddlewareOptionArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareOptionArgumentNullException(string paramName) : base(paramName) { }
    }

    public class ResponsiveMiddlewareInvokeArgumentNullException : ArgumentNullException
    {
        public ResponsiveMiddlewareInvokeArgumentNullException(string paramName) : base(paramName) { }
    }
}
