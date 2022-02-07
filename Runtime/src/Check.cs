using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(T value)
        => NotNull(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    internal static T NotNull<T>(T value, [InvokerParameterName] [NotNull] string parameterName)
        => value is null
               ? throw new ArgumentNullException(parameterName)
               : value;

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrEmpty(string value)
        => NotNullOrEmpty(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    private static string NotNullOrEmpty(string value, [InvokerParameterName] [NotNull] string parameterName)
        => value.IsNullOrEmpty()
               ? throw new ArgumentNullException($"{parameterName} can not be null or empty", parameterName)
               : value;

    [ContractAnnotation("value:null => halt")]
    public static bool NotNullOrEmpty<T>(ICollection<T> value)
        => NotNullOrEmpty(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    internal static bool NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName] [NotNull] string parameterName)
        => value.IsNullOrEmpty()
               ? throw new ArgumentNullException($"{parameterName} can not be null or empty!", parameterName)
               : true;

    [ContractAnnotation("value:less => halt")]
    public static bool NotLessThan(int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
        => value < expected
               ? throw new ArgumentNullException($"{parameterName} argument can not be bigger than given string's length!")
               : value < expected;

    [ContractAnnotation("value:more => halt")]
    public static bool NotMoreThan(int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
        => value > expected
               ? throw new ArgumentNullException($"{parameterName} argument can not be smaller than given string's length!")
               : value > expected;
}