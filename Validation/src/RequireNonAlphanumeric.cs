namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RequireNonAlphanumeric : ValidationAttribute
{
    public RequireNonAlphanumeric()
        : base(() => "Non Alphanumeric is required")
    {
    }

    public override bool IsValid(object value) =>
        value switch
        {
            null          => true,
            string actual => !actual.All(char.IsLetterOrDigit),
            _             => false
        };
}