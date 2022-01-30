using Xunit;

namespace Wangkanai.Extensions;

public class CollectionExtensionsTests
{
    [Fact]
    public void IsNullOrEmpty()
    {
        // Null
        List<string> strings = null;
        List<int>    ints    = null;
        Assert.True(strings.IsNullOrEmpty());
        Assert.True(ints.IsNullOrEmpty());

        // Empty
        strings = new List<string>();
        ints    = new List<int>();
        Assert.True(strings.IsNullOrEmpty());
        Assert.True(ints.IsNullOrEmpty());

        // Exist
        strings.Add("hello");
        ints.Add(int.MaxValue);
        Assert.False(strings.IsNullOrEmpty());
        Assert.False(ints.IsNullOrEmpty());
    }
}