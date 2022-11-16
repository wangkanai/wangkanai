// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace Wangkanai.Blazor.Components.Sections;

/// <summary>
/// Renders content provided by <see cref="BodyContent"/> components.
/// </summary>
public sealed class BodyOutlet : BlazorComponentBase
{
    private const string GetAndRemoveExisingClass = "Blazor._internal.BodyClass.getAndRemoveExisingClass";
    
    internal const string BodySectionOutletName = "body";
    internal const string CssClassOutletName    = "class";
    
    private string? _defaultClass;

    private IJSRuntime JSRuntime { get; set; } = default;
    
    /// <inheritdoc />
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _defaultClass = await JSRuntime.InvokeAsync<string>(GetAndRemoveExisingClass);
            StateHasChanged();
        }
    }
}