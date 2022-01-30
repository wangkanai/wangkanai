using System.Linq;

namespace Wangkanai.Blazor;

public class ClassMapper : BaseMapper
{
    public string AsString()
        => string.Join(" ", Items.Select(i => i()).Where((i => i != null)));

    public override string ToString()
        => AsString();
}