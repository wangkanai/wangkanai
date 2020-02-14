using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class PageRouteModelConventionTest
    {
        [Fact]
        public void Apply_Responsive()
        {
            var relativePath   = "";
            var viewEnginePath = "";
            var areaName       = "";

            var model      = new PageRouteModel(relativePath, viewEnginePath, areaName);
            var convention = new ResponsivePageRouteModelConvention();
            convention.Apply(model);
            Assert.Same("", model.Selectors.Single().AttributeRouteModel.Template);
        }
    }
}