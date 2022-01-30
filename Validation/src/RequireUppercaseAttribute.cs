namespace System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class RequireUppercaseAttribute : ValidationAttribute
{
    public RequireUppercaseAttribute()
        : base(() => "Uppercase is required")
    {
    }

    public override bool IsValid(object value) =>
        value switch
        {
            null          => true, // Required duty
            string actual => actual.Any(char.IsUpper),
            _             => false
        };
}