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
            var routeTemplate = "/Admin/Index.mobile";
            var template      = "Admin/";
            var model         = CreateAreaPageRouteModel(relativePath, routeTemplate);
            var convention    = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            var selector = model.Selectors.Last();
            Assert.Equal(template, selector.AttributeRouteModel.Template);
        }
        
        [Fact]
        public void Apply_Admin_Report_Mobile()
        {
            var relativePath  = "/Areas/Admin/Pages/Report.mobile.cshtml";
            var routeTemplate = "/Admin/Report.mobile";
            var template      = "Admin/Report";
            var model         = CreateAreaPageRouteModel(relativePath, routeTemplate);
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