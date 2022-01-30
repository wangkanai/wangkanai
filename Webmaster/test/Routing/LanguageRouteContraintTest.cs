// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

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
        var contraint = new EnglishLanguageRouteContraint();
        var values    = new RouteValueDictionary(new { controller = routeValue });

        // act
        bool match = Match(contraint, values);

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
        var contraint = new ThaiLanguageRouteContraint();
        var values    = new RouteValueDictionary(new { controller = routeValue });

        // act
        bool match = Match(contraint, values);

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
        var contraint = new LaoLanguageRouteContraint();
        var values    = new RouteValueDictionary(new { controller = routeValue });

        // act
        bool match = Match(contraint, values);

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
        var contraint = new MyanmarLanguageRouteContraint();
        var values    = new RouteValueDictionary(new { controller = routeValue });

        // act
        bool match = Match(contraint, values);

        // assert
        Assert.Equal(shouldMatch, match);
    }

    private bool Match(RegexRouteConstraint contraint, RouteValueDictionary values)
    {
        return contraint.Match(
            new DefaultHttpContext(),
            route: new Mock<IRouter>().Object,
            routeKey: "controller",
            values: values,
            routeDirection: RouteDirection.IncomingRequest);
    }
}