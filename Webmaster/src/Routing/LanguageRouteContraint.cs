// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Routing.Constraints;

namespace Wangkanai.Webmaster.Routing;

public sealed class EnglishLanguageRouteConstraint : RegexRouteConstraint
{
	public EnglishLanguageRouteConstraint() : base(new Regex("^[a-zA-Z]*$", RegexOptions.Compiled, Constants.RegexTimeout)) { }
}

public sealed class ThaiLanguageRouteConstraint : RegexRouteConstraint
{
	public ThaiLanguageRouteConstraint() : base(new Regex(@"^\p{IsThai}*$", RegexOptions.Compiled, Constants.RegexTimeout)) { }
}

public sealed class LaoLanguageRouteConstraint : RegexRouteConstraint
{
	public LaoLanguageRouteConstraint() : base(new Regex(@"^\p{IsLao}*$", RegexOptions.Compiled, Constants.RegexTimeout)) { }
}

public sealed class MyanmarLanguageRouteConstraint : RegexRouteConstraint
{
	public MyanmarLanguageRouteConstraint() : base(new Regex("^[U+1000–U+109F]*$", RegexOptions.Compiled, Constants.RegexTimeout)) { }
}
