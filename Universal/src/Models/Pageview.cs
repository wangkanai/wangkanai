// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Universal.Options;

namespace Wangkanai.Universal.Models;

/// <summary>
///     Pageview tracking allows you to measure the number of views you had of a particular page on your web site
/// </summary>
public sealed class Pageview : Send
{
    public Pageview()
    {
        option = new PageviewOption();
    }

    /// <param name="page">
    ///     The page path and query string of the page (e.g. /homepage?id=10). This value must start with a /
    ///     character.
    /// </param>
    /// <param name="title">The title of the page (e.g. homepage)</param>
    public Pageview(string page, string title)
        : this()
    {
        option.Page  = page;
        option.Title = title;
    }

    /// <param name="page">
    ///     The page path and query string of the page (e.g. /homepage?id=10). This value must start with a /
    ///     character.
    /// </param>
    /// <param name="title">The title of the page (e.g. homepage)</param>
    /// <param name="hitcallback">
    ///     In some cases, like when you track outbound links, you might want to know when the tracker is
    ///     done sending data
    /// </param>
    public Pageview(string page, string title, string hitcallback)
        : this(page, title)
    {
        option.HitCallback = hitcallback;
    }

    private PageviewOption option { get; }

    public override string ToString()
    {
        return option != null
                   ? string.Format("ga('send','pageview',{0});", option)
                   : "ga('send', 'pageview');";
    }
}