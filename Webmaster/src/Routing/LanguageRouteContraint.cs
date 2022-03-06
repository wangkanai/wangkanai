// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Routing.Constraints;

using System.Text.RegularExpressions;

namespace Wangkanai.Webmaster.Routing;

public class EnglishLanguageRouteConstraint : RegexRouteConstraint
{
    public EnglishLanguageRouteConstraint() : base(new Regex("^[a-zA-Z]*$"))
    {
    }
}

public class ThaiLanguageRouteConstraint : RegexRouteConstraint
{
    public ThaiLanguageRouteConstraint() : base(new Regex(@"^\p{IsThai}*$"))
    {
    }
}

public class LaoLanguageRouteConstraint : RegexRouteConstraint
{
    public LaoLanguageRouteConstraint() : base(new Regex(@"^\p{IsLao}*$"))
    {
    }
}

public class MyanmarLanguageRouteConstraint : RegexRouteConstraint
{
    public MyanmarLanguageRouteConstraint() : base(new Regex("^[U+1000–U+109F]*$"))
    {
    }
}