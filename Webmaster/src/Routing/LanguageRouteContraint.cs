// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Routing.Constraints;

namespace Wangkanai.Webmaster.Routing;

public sealed class EnglishLanguageRouteConstraint : RegexRouteConstraint
{
    public EnglishLanguageRouteConstraint() : base(new Regex("^[a-zA-Z]*$"))
    {
    }
}

public sealed class ThaiLanguageRouteConstraint : RegexRouteConstraint
{
    public ThaiLanguageRouteConstraint() : base(new Regex(@"^\p{IsThai}*$"))
    {
    }
}

public sealed class LaoLanguageRouteConstraint : RegexRouteConstraint
{
    public LaoLanguageRouteConstraint() : base(new Regex(@"^\p{IsLao}*$"))
    {
    }
}

public sealed class MyanmarLanguageRouteConstraint : RegexRouteConstraint
{
    public MyanmarLanguageRouteConstraint() : base(new Regex("^[U+1000–U+109F]*$"))
    {
    }
}