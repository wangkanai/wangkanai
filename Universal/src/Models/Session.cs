// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Universal.Models;

public sealed class Session
{
    public Session()
    {
        Events = new List<Event>();
    }

    /// <summary>
    ///     Name of the tracker object
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Anonymously identifies a browser instance. By default, this value is stored as part of the first-party analytics
    ///     tracking cookie with a two-year expiration.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    ///     This is intended to be a known identifier for a user/visitor provided by the site owner/tracking library user. It
    ///     may not itself be PII. The value should never be persisted in GA cookies or other Wangkanai.Universal provided
    ///     storage.
    /// </summary>
    public string UserId { get; set; }

    public List<Event> Events   { get; set; }
    public Pageview    Pageview { get; set; }

    public override string ToString()
    {
        var script = new StringBuilder();
        script.AppendLine(Pageview != null ? Send(Pageview) : Send());
        foreach (var e in Events)
            if (e != null)
                script.AppendLine(Send(e));

        return script.ToString();
    }

    private string Send()
    {
        return "ga('send', 'pageview');";
    }

    private string Send(Pageview pageview)
    {
        return string.Format("ga('send','pageview'{0});", pageview == null ? "" : "," + pageview);
    }

    private string Send(Event pageEvent)
    {
        return string.Format("ga('send',{0});", pageEvent);
    }
}