// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;

using Moq;

using Xunit;

namespace Wangkanai.Webmaster.Routing;

public class LanguageRouteContraintTests
{
	[Theory]
	[InlineData("bangkok", true)]
	[InlineData("Bangkok", true)]
	[InlineData("Bangkok1", false)]
	[InlineData("กรุงเทพ", false)]
	[InlineData("กรุงเทพ1", false)]
	[InlineData("กรุงเทพabc", false)]
	[InlineData("Bangkokกทม", false)]
	[InlineData("北京", false)]
	[InlineData("北京beijing", false)]
	public void English(string routeValue, bool shouldMatch)
	{
		// arrage
		var constraint = new EnglishLanguageRouteConstraint();
		var values     = new RouteValueDictionary(new { controller = routeValue });

		// act
		var match = Match(constraint, values);

		// assert
		Assert.Equal(shouldMatch, match);
	}

	[Theory]
	[InlineData("กรุงเทพ", true)]
	[InlineData("bangkok", false)]
	[InlineData("ປະຊາຊົນລາວ", false)]
	[InlineData("Bangkok1", false)]
	[InlineData("กรุงเทพ1", false)]
	[InlineData("กรุงเทพabc", false)]
	[InlineData("Bangkokกทม", false)]
	[InlineData("北京", false)]
	[InlineData("北京beijing", false)]
	public void Thai(string routeValue, bool shouldMatch)
	{
		// arrage
		var constraint = new ThaiLanguageRouteConstraint();
		var values     = new RouteValueDictionary(new { controller = routeValue });

		// act
		var match = Match(constraint, values);

		// assert
		Assert.Equal(shouldMatch, match);
	}

	[Theory]
	[InlineData("ລາວ", true)]
	[InlineData("กรุงเทพ", false)]
	[InlineData("bangkok", false)]
	[InlineData("Bangkok1", false)]
	[InlineData("ປະຊາຊົນລາວ1", false)]
	[InlineData("ປະຊາຊົນລາວabc", false)]
	[InlineData("Bangkokกทม", false)]
	[InlineData("北京", false)]
	[InlineData("北京beijing", false)]
	public void Lao(string routeValue, bool shouldMatch)
	{
		// arrage
		var constraint = new LaoLanguageRouteConstraint();
		var values     = new RouteValueDictionary(new { controller = routeValue });

		// act
		var match = Match(constraint, values);

		// assert
		Assert.Equal(shouldMatch, match);
	}

	[Theory]
	[InlineData("မြန်မာ", false)] // some wrong with Burmese
	[InlineData("กรุงเทพ", false)]
	[InlineData("bangkok", false)]
	[InlineData("Bangkok1", false)]
	[InlineData("ປະຊາຊົນລາວ1", false)]
	[InlineData("ປະຊາຊົນລາວabc", false)]
	[InlineData("Bangkokกทม", false)]
	[InlineData("北京", false)]
	[InlineData("北京beijing", false)]
	public void Myanmar(string routeValue, bool shouldMatch)
	{
		// arrage
		var constraint = new MyanmarLanguageRouteConstraint();
		var values     = new RouteValueDictionary(new { controller = routeValue });

		// act
		var match = Match(constraint, values);

		// assert
		Assert.Equal(shouldMatch, match);
	}

	private bool Match(RegexRouteConstraint contraint, RouteValueDictionary values)
	{
		return contraint.Match(
			new DefaultHttpContext(),
			new Mock<IRouter>().Object,
			"controller",
			values,
			RouteDirection.IncomingRequest);
	}
}