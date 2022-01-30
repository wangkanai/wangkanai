namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RequireTrueAttribute : ValidationAttribute
{
    public RequireTrueAttribute()
        : base(() => "Checked is required")
    {
    }

    public override bool IsValid(object value) =>
        value switch
        {
            null        => true,
            bool actual => actual,
            _           => false
        };
}