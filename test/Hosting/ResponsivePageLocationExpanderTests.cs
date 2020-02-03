using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsivePageLocationExpanderTests
    {
        public static IEnumerable<object[]> ViewLocationExpanderTestData
        {
            get
            {
                yield return new object[]
                             {
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
        public void ExpandViewLocations_ViewLocationExpanderContext_IEnumerable_ReturnsExpected(Device deviceType, IEnumerable<string> viewLocations, IEnumerable<string> expectedViewLocations)
        {
            var context          = SetupViewLocationExpanderContext(deviceType);
            var locationExpander = new ResponsivePageLocationExpander();
            locationExpander.PopulateValues(context);
            var resultLocations = locationExpander.ExpandViewLocations(context, viewLocations).ToList();

            Assert.Equal(expectedViewLocations, resultLocations.ToList());
        }

        [Fact]
        public void Ctor_Default_Success()
        {
            //var locationExpander = new ViewLocationExpander();
        }

        [Fact]
        public void Ctor_ResponsiveViewLocationFormat_Success()
        {
            var locationExpander = new ResponsivePageLocationExpander();
            Assert.NotNull(locationExpander);
        }

        [Fact]
        public void ExpandViewLocations_NoDevice_ReturnsExpected()
        {
            var context          = SetupViewLocationExpanderContext(Device.Tablet);
            var viewLocations    = new List<string> {"/Pages/{1}/{0}.cshtml", "/Pages/Shared/{0}.cshtml"};
            var locationExpander = new ResponsivePageLocationExpander();
            var resultLocations  = locationExpander.ExpandViewLocations(context, viewLocations);

            Assert.Equal(viewLocations, resultLocations);
        }

        [Fact]
        public void ExpandViewLocations_Null_IEnumerable_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsivePageLocationExpander();
            Assert.Throws<ArgumentNullException>(() => 
                locationExpander.ExpandViewLocations(null, new List<string>()));
        }

        [Fact]
        public void ExpandViewLocations_ViewLocationExpanderContext_Null_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsivePageLocationExpander();
            Assert.Throws<ArgumentNullException>(() =>
                locationExpander.ExpandViewLocations(SetupViewLocationExpanderContext(Device.Tablet), null));
        }

        [Fact]
        public void PopulateValues_Null_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsivePageLocationExpander();
            Assert.Throws<ArgumentNullException>(() => 
                locationExpander.PopulateValues(null));
        }

        [Fact]
        public void PopulateValues_ViewLocationExpanderContext_Success()
        {
            var deviceKey        = "device-page"; // May this one can be public in ResponsiveViewLocationExpander.cs.
            var context          = SetupViewLocationExpanderContext(Device.Tablet);
            var locationExpander = new ResponsivePageLocationExpander();
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
}