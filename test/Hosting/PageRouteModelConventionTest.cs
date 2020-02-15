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
        public void Apply_Null()
        {
            var relativePath   = "";
            var viewEnginePath = "";
            var areaName       = "";

            var model      = new PageRouteModel(relativePath, viewEnginePath, areaName);
            var convention = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            Assert.Same("", model.Selectors.Single().AttributeRouteModel.Template);
        }

        [Theory]
        [InlineData("Admin", "Admin/Index", "/Areas/Admin/Pages/Index.mobile.cshtml", "/Index.mobile")]
        [InlineData("Admin", "Admin/Index", "/Areas/Admin/Pages/Index.tablet.cshtml", "/Index.tablet")]
        [InlineData(null, "About/Index", "/Pages/About/Index.mobile.cshtml", "/About/Index.mobile")]
        [InlineData(null, "About/Index", "/Pages/About/Index.tablet.cshtml", "/About/Index.tablet")]
        [InlineData(null, "Index", "/Pages/Index.mobile.cshtml", "/Index.mobile")]
        [InlineData(null, "Index", "/Pages/Index.tablet.cshtml", "/Index.tablet")]
        [InlineData(null, "Privacy", "/Pages/Privacy.mobile.cshtml", "/Privacy.mobile")]
        [InlineData(null, "Privacy", "/Pages/Privacy.tablet.cshtml", "/Privacy.tablet")]
        public void Apply_Area(string areaName, string template, string relativePath, string viewEnginePath)
        {
            var options    = new RazorPagesOptions();
            var factory    = new MockPageRouteModel(options);
            var model      = factory.CreateAreaRouteModel(relativePath, template);
            var convention = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            Assert.Equal(template, model.Selectors.Single().AttributeRouteModel.Template);
        }

        [Fact]
        public void Apply_Index_Mobile()
        {
            var relativePath  = "/Pages/Index.mobile.cshtml";
            var routeTemplate = "/Index.mobile";
            var template      = "";
            var model         = CreatePageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        [Fact]
        public void Apply_Privacy_Mobile()
        {
            var relativePath  = "/Pages/Privacy.mobile.cshtml";
            var routeTemplate = "/Privacy.mobile";
            var template      = "Privacy";
            var model         = CreatePageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        [Fact]
        public void Apply_Admin_Index_Mobile()
        {
            var relativePath  = "/Areas/Admin/Pages/Index.mobile.cshtml";
            var routeTemplate = "/Index.mobile";
            var template      = "Admin/Index";
            var model         = CreatePageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }

        private static PageRouteModel CreatePageRouteModel(string relativePath, string routeTemplate)
        {
            var options = new RazorPagesOptions();
            var factory = new MockPageRouteModel(options);
            var model   = factory.CreateRouteModel(relativePath, routeTemplate);
            return model;
        }

        private static PageRouteModel CreateAreaPageRouteModel(string relativePath, string routeTemplate)
        {
            var options = new RazorPagesOptions();
            var factory = new MockPageRouteModel(options);
            var model   = factory.CreateAreaRouteModel(relativePath, routeTemplate);
            return model;
        }
    }
}