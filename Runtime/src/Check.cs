using Wangkanai.Extensions;

namespace Wangkanai;

[DebuggerStepThrough]
public static class Check
{
    [ContractAnnotation("value:null => halt")]
    public static T NotNull<T>(T value)
        => NotNull(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    internal static T NotNull<T>(T value, [InvokerParameterName] string parameterName)
        => value is null
               ? throw new ArgumentNullException(parameterName)
               : value;

    [ContractAnnotation("value:null => halt")]
    public static string NotNullOrEmpty(string value)
        => NotNullOrEmpty(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    private static string NotNullOrEmpty(string value, [InvokerParameterName] string parameterName)
        => value.IsNullOrEmpty()
               ? throw new ArgumentNullOrEmptyException($"{parameterName} can not be null or empty", parameterName)
               : value;

    [ContractAnnotation("value:null => halt")]
    public static bool NotNullOrEmpty<T>(ICollection<T> value)
        => NotNullOrEmpty(value, nameof(value));

    [ContractAnnotation("value:null => halt")]
    internal static bool NotNullOrEmpty<T>(ICollection<T> value, [InvokerParameterName] string parameterName)
        => value.IsNullOrEmpty()
               ? throw new ArgumentNullOrEmptyException($"{parameterName} can not be null or empty!", parameterName)
               : true;

    [ContractAnnotation("value:equal => halt")]
    public static bool NotEqual(int value, int expected)
        => NotEqual(value, expected, nameof(value));

    [ContractAnnotation("value:equal => halt")]
    internal static bool NotEqual(int value, int expected, [InvokerParameterName] string parameterName)
        => value != expected
               ? throw new ArgumentEqualException($"{parameterName} argument cannot not equal to the given expected value")
               : true;

    [ContractAnnotation("value:less => halt")]
    public static bool NotLessThan(int value, int expected)
        => NotLessThan(value, expected, nameof(value));

    [ContractAnnotation("value:less => halt")]
    public static bool NotLessThan(int value, int expected, [InvokerParameterName] string parameterName)
        => value < expected
               ? throw new ArgumentLessThanException($"{parameterName} argument can not be bigger than given string's length!")
               : true;

    [ContractAnnotation("value:less => halt")]
    public static bool NotMoreThan(int value, int expected)
        => NotMoreThan(value, expected, nameof(value));

    [ContractAnnotation("value:more => halt")]
    internal static bool NotMoreThan(int value, int expected, [InvokerParameterName] [NotNull] string parameterName)
        => value > expected
               ? throw new ArgumentMoreThanException($"{parameterName} argument can not be smaller than given string's length!")
               : true;
}