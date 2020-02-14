using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
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
            var model      = new PageRouteModel(relativePath, viewEnginePath, areaName);
            var convention = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            Assert.Equal(template, model.Selectors.Single().AttributeRouteModel.Template);
        }
    }
}