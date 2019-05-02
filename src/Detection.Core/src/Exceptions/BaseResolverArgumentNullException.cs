namespace System
{
    public class BaseResolverArgumentNullException : ArgumentNullException
    {
        public BaseResolverArgumentNullException(string paramName) : base(paramName) { }
    }
}
