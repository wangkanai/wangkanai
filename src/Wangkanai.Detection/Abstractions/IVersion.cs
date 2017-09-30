namespace Wangkanai.Detection
{
    public interface IVersion
    {
        string Build { get; }
        string Major { get; }
        string Minor { get; }
        string Patch { get; }
    }
}