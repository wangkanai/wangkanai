// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Microsoft.AspNetCore.Http.Extensions;

using Wangkanai.Cryptography;
using Wangkanai.Extensions;
using Wangkanai.Webmaster.Models;

namespace Wangkanai.Helpers;

public sealed class Gravatar
{
    private readonly string _email;
    private readonly IconSize _size;
    private readonly GravatarRating _rating;
    private readonly GravatarMode _mode;

    public Gravatar(string email)
        => _email = email;

    public Gravatar(string email, IconSize size)
        : this(email)
        => _size = size;

    public Gravatar(string email, IconSize size, GravatarRating rating)
        : this(email, size)
        => _rating = rating;

    public Gravatar(string email, IconSize size, GravatarRating rating, GravatarMode mode)
        : this(email, size, rating)
        => _mode = mode;

    public override string ToString()
    {
        var uri = GetUri(_email);
        var query = new QueryBuilder();
        if (_size > 0)
            query.Add("s", _size.Value());
        if (_rating != GravatarRating.g)
            query.Add("r", _rating.Value());
        if (_mode != GravatarMode.Default)
            query.Add("d", _mode.Value());
        return uri + Uri.EscapeUriString(query.ToQueryString().ToString());
    }

    public static string HashEmail([NotNull] string email)
        => email.IsNullOrEmpty()
               ? "00000000000000000000000000000000"
               : email.HashMd5();

    public static string GetUri([NotNull] string email)
        => $"https://www.gravatar.com/avatar/{HashEmail(email)}";
}