using Microsoft.AspNetCore.Components;

namespace Wangkanai.Blazor;

public class BaseWangkanaiTable : BaseWangkanaiDomComponent
{
    [Parameter]
    public RenderFragment ChildContent { get; set; }
}