// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Linq;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Wangkanai.Responsive.Mocks;

using Xunit;

namespace Wangkanai.Responsive.Hosting;

public class PageRouteModelConventionTest
{
    [Fact]
    public void Apply_Null_Empty()
    {
        Assert.Throws<ArgumentNullException>(() => CreatePageRouteModel(null!, null!));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreatePageRouteModel("", null!));
        Assert.Throws<ArgumentOutOfRangeException>(() => CreatePageRouteModel("", ""));
    }

    [Theory]
    // Why is Index is not a normal page?
    // [InlineData("/Pages/Index.cshtml", "/Index")]
    [InlineData("/Pages/Privacy.cshtml", "/Privacy")]
    [InlineData("/Areas/Admin/Pages/Report.cshtml", "/Admin/Report")]
    public void Apply_Normal_Page(string relativePath, string routeTemplate)
    {
        var model      = CreatePageRouteModel(relativePath, routeTemplate);
        var convention = new ResponsivePageRouteModelConvention();
        convention.Apply(model);
        Assert.Equal(1, model.Selectors.Count);
    }

    [Theory]
    [InlineData("/Pages/Index.mobile.cshtml", "/Index.mobile", "")]
    [InlineData("/Pages/Index.tablet.cshtml", "/Index.tablet", "")]
    public void Apply_Index(string relativePath, string routeTemplate, string template)
    {
        var model      = CreatePageRouteModel(relativePath, routeTemplate);
        var convention = new ResponsivePageRouteModelConvention();
        convention.Apply(model);
        var selector = model.Selectors.Last();
        Assert.Equal(template, selector.AttributeRouteModel.Template);
    }

    [Theory]
    [InlineData("/Pages/Privacy.mobile.cshtml", "/Privacy.mobile", "Privacy")]
    [InlineData("/Pages/Privacy.tablet.cshtml", "/Privacy.tablet", "Privacy")]
    public void Apply_Privacy(string relativePath, string routeTemplate, string template)
    {
        var model      = CreatePageRouteModel(relativePath, routeTemplate);
        var convention = new ResponsivePageRouteModelConvention();
        convention.Apply(model);
        var selector = model.Selectors.Last();
        Assert.Equal(template, selector.AttributeRouteModel.Template);
    }

    [Theory]
    [InlineData("/Areas/Admin/Pages/Index.mobile.cshtml", "/Admin/Index.mobile", "Admin/")]
    [InlineData("/Areas/Admin/Pages/Index.tablet.cshtml", "/Admin/Index.tablet", "Admin/")]
    public void Apply_Admin_Index(string relativePath, string routeTemplate, string template)
    {
        var model      = CreateAreaPageRouteModel(relativePath, routeTemplate);
        var convention = new ResponsivePageRouteModelConvention();
        convention.Apply(model);
        var selector = model.Selectors.Last();
        Assert.Equal(template, selector.AttributeRouteModel.Template);
    }

    [Theory]
    [InlineData("/Areas/Admin/Pages/Report.mobile.cshtml", "/Admin/Report.mobile", "Admin/Report")]
    [InlineData("/Areas/Admin/Pages/Report.tablet.cshtml", "/Admin/Report.tablet", "Admin/Report")]
    public void Apply_Admin_Report(string relativePath, string routeTemplate, string template)
    {
        var model      = CreateAreaPageRouteModel(relativePath, routeTemplate);
        var convention = new ResponsivePageRouteModelConvention();
        convention.Apply(model);
        var selector = model.Selectors.Last();
        Assert.Equal(template, selector.AttributeRouteModel.Template);
    }

    private static PageRouteModel CreatePageRouteModel(string relativePath, string routeTemplate)
        => CreatePageRouteModelFactory()
            .CreateRouteModel(relativePath, routeTemplate);

    private static PageRouteModel CreateAreaPageRouteModel(string relativePath, string routeTemplate)
        => CreatePageRouteModelFactory()
            .CreateAreaRouteModel(relativePath, routeTemplate);

    private static MockPageRouteModel CreatePageRouteModelFactory()
        => new MockPageRouteModel(new RazorPagesOptions());
}