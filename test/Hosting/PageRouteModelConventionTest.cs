using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging.Abstractions;
using Wangkanai.Detection.Mocks;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class PageRouteModelConventionTest
    {
        [Fact]
        public void Apply_Null_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => CreatePageRouteModel(null, null));
            Assert.Throws<ArgumentOutOfRangeException>(() => CreatePageRouteModel("", null));
            Assert.Throws<ArgumentOutOfRangeException>(() => CreatePageRouteModel("", ""));
        }
        
        [Theory]
        [InlineData("", "/Index.mobile", "/Pages/Index.mobile.cshtml")]
        [InlineData("", "/Index.tablet", "/Pages/Index.tablet.cshtml")]
        public void Apply_Index(string template, string routeTemplate, string relativePath)
        {
            var model         = CreatePageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        [Theory]
        [InlineData("Privacy", "/Privacy.mobile", "/Pages/Privacy.mobile.cshtml")]
        [InlineData("Privacy", "/Privacy.tablet", "/Pages/Privacy.tablet.cshtml")]
        public void Apply_Privacy(string template, string routeTemplate, string relativePath)
        {
            var model         = CreatePageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        [Theory]
        [InlineData("Admin/", "/Admin/Index.mobile","/Areas/Admin/Pages/Index.mobile.cshtml")]
        [InlineData("Admin/", "/Admin/Index.tablet","/Areas/Admin/Pages/Index.tablet.cshtml")]
        public void Apply_Admin_Index(string template, string routeTemplate, string relativePath)
        {
            var model         = CreateAreaPageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        [Theory]
        [InlineData("Admin/Report", "/Admin/Report.mobile","/Areas/Admin/Pages/Report.mobile.cshtml")]
        [InlineData("Admin/Report", "/Admin/Report.tablet","/Areas/Admin/Pages/Report.tablet.cshtml")]
        public void Apply_Admin_Report(string template, string routeTemplate, string relativePath)
        {
            var model         = CreateAreaPageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
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
}