using System.Linq;

namespace Wangkanai.Blazor;

public class StyleMapper : BaseMapper
{
    public string AsString()
        => string.Join("; ", Items.Select(i => i()).Where((i => i != null)));

    public override string ToString()
        => AsString();
}