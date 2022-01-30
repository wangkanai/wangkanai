namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RequireFalseAttribute : ValidationAttribute
{
    public RequireFalseAttribute()
        : base(() => "Unchecked is required")
    {
    }

    public override bool IsValid(object value) =>
        value switch
        {
            null        => true,
            bool actual => actual == false,
            _           => false
        };
}