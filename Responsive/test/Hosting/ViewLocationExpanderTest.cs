// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Hosting;

public class ResponsiveViewLocationExpanderTest
{
    public static IEnumerable<object[]> ViewLocationExpanderTestData
    {
        get
        {
            yield return new object[]
            {
                ResponsiveViewLocationFormat.Suffix,
                Device.Tablet,
                new[]
                {
                    "/Views/{1}/{0}.cshtml",
                    "/Views/Shared/{0}.cshtml"
                },
                new[]
                {
                    "/Views/{1}/{0}.Tablet.cshtml",
                    "/Views/{1}/{0}.cshtml",
                    "/Views/Shared/{0}.Tablet.cshtml",
                    "/Views/Shared/{0}.cshtml"
                }
            };

            yield return new object[]
            {
                ResponsiveViewLocationFormat.Subfolder,
                Device.Tablet,
                new[]
                {
                    "/Views/{1}/{0}.cshtml",
                    "/Views/Shared/{0}.cshtml"
                },
                new[]
                {
                    "/Views/{1}/Tablet/{0}.cshtml",
                    "/Views/{1}/{0}.cshtml",
                    "/Views/Shared/Tablet/{0}.cshtml",
                    "/Views/Shared/{0}.cshtml"
                }
            };
        }
    }

    public static IEnumerable<object[]> RazorPagesLocationTestData
    {
        get
        {
            yield return new object[]
            {
                ResponsiveViewLocationFormat.Suffix,
                Device.Tablet,
                new[]
                {
                    "/Pages/{1}/{0}.cshtml",
                    "/Pages/Shared/{0}.cshtml"
                },
                new[]
                {
                    "/Pages/{1}/{0}.Tablet.cshtml",
                    "/Pages/{1}/{0}.cshtml",
                    "/Pages/Shared/{0}.Tablet.cshtml",
                    "/Pages/Shared/{0}.cshtml"
                }
            };
        }
    }

    [Theory]
    [MemberData(nameof(ViewLocationExpanderTestData))]
    public void ExpandViewLocations_ViewLocationExpanderContext_IEnumerable_ReturnsExpected(ResponsiveViewLocationFormat format, Device deviceType, IEnumerable<string> viewLocations, IEnumerable<string> expectedViewLocations)
    {
        var context          = SetupViewLocationExpanderContext(deviceType);
        var locationExpander = new ResponsiveViewLocationExpander(format);
        locationExpander.PopulateValues(context);
        var resultLocations = locationExpander.ExpandViewLocations(context, viewLocations).ToList();

        Assert.Equal(expectedViewLocations, resultLocations.ToArray());
    }

    [Fact]
    public void Ctor_InvalidFormat_InvalidEnumArgumentException()
    {
        var max            = int.MaxValue;
        var locationFormat = (ResponsiveViewLocationFormat)max;
        Assert.Throws<InvalidEnumArgumentException>(() => new ResponsiveViewLocationExpander(locationFormat));
    }

    [Fact]
    public void Ctor_ResponsiveViewLocationFormat_Success()
    {
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder);
        Assert.NotNull(locationExpander);
    }

    [Fact]
    public void ExpandViewLocations_NoDevice_ReturnsExpected()
    {
        var context          = SetupViewLocationExpanderContext(Device.Tablet);
        var viewLocations    = new List<string> { "/Views/{1}/{0}.cshtml", "/Views/Shared/{0}.cshtml" };
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix);
        var resultLocations  = locationExpander.ExpandViewLocations(context, viewLocations);

        Assert.Equal(viewLocations, resultLocations);
    }

    [Fact]
    public void ExpandViewLocations_Null_IEnumerable_ThrowsArgumentNullException()
    {
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix);
        Assert.Throws<ArgumentNullException>(() => locationExpander.ExpandViewLocations(null!, new List<string>()));
    }

    [Fact]
    public void ExpandViewLocations_ViewLocationExpanderContext_Null_ThrowsArgumentNullException()
    {
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix);
        Assert.Throws<ArgumentNullException>(() =>
                                                 locationExpander.ExpandViewLocations(SetupViewLocationExpanderContext(Device.Tablet), null!));
    }

    [Fact]
    public void PopulateValues_Null_ThrowsArgumentNullException()
    {
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix);
        Assert.Throws<ArgumentNullException>(() => locationExpander.PopulateValues(null!));
    }

    [Fact]
    public void PopulateValues_ViewLocationExpanderContext_Success()
    {
        var deviceKey        = "device"; // May this one can be public in ResponsiveViewLocationExpander.cs.
        var context          = SetupViewLocationExpanderContext(Device.Tablet);
        var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Suffix);
        locationExpander.PopulateValues(context);

        Assert.NotEqual(0, context.Values.Count);
        Assert.Same(context.ActionContext.HttpContext.GetDevice().ToString(), context.Values[deviceKey]);
    }


    private ViewLocationExpanderContext SetupViewLocationExpanderContext(Device deviceType)
    {
        var action = new ActionContext();
        var context = new ViewLocationExpanderContext(action, "View", "Controller", "Area", "Page", true)
        {
            Values = new Dictionary<string, string>()
        };
        context.ActionContext.HttpContext = new DefaultHttpContext();
        context.ActionContext.HttpContext.SetDevice(deviceType);

        return context;
    }
}