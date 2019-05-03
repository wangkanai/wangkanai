// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Wangkanai.Detection;
using Xunit;

namespace Wangkanai.Responsive.Test.Core
{
    public class ResponsiveViewLocationExpanderTests
    {
        [Fact]
        public void Ctor_Default_Success()
        {
            var locationExpander = new ResponsiveViewLocationExpander();
        }

        [Fact]
        public void Ctor_ResponsiveViewLocationFormat_Success()
        {
            var locationExpander = new ResponsiveViewLocationExpander(ResponsiveViewLocationFormat.Subfolder);
        }

        [Fact]
        public void Ctor_InvalidFormat_InvalidEnumArgumentException()
        {
            var max = int.MaxValue;
            var locationFormat = (ResponsiveViewLocationFormat)max;
            Assert.Throws<InvalidEnumArgumentException>(() => new ResponsiveViewLocationExpander(locationFormat));
        }

        [Fact]
        public void PopulateValues_ViewLocationExpanderContext_Success()
        {
            string deviceKey = "device"; // May this one can be public in ResponsiveViewLocationExpander.cs.
            var context = SetupViewLocationExpanderContext();
            var locationExpander = new ResponsiveViewLocationExpander();
            locationExpander.PopulateValues(context);

            Assert.NotEqual(0, context.Values.Count);
            Assert.Same(context.ActionContext.HttpContext.GetDevice().Preferred, context.Values[deviceKey]);
        }

        [Fact]
        public void PopulateValues_Null_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsiveViewLocationExpander();
            Assert.Throws<ViewLocationExpanderPopulateValuesArgumentNullException >(() => locationExpander.PopulateValues(null));
        }

        public static IEnumerable<object[]> ViewLocationExpanderTestData
        {
            get
            {
                yield return new object[]
                {
                    ResponsiveViewLocationFormat.Suffix,
                    new[]
                    {
                        "/Views/{1}/{0}.cshtml",
                        "/Views/Shared/{0}.cshtml"
                    },
                    new[]
                    {
                        // Why is there is Tablet expected here?
                        //"/Views/{1}/{0}.Tablet.cshtml",
                        "/Views/{1}/{0}.cshtml",
                        //"/Views/Shared/{0}.Tablet.cshtml",
                        "/Views/Shared/{0}.cshtml",
                    }
                };

                yield return new object[]
                {
                    ResponsiveViewLocationFormat.Subfolder,
                    new[]
                    {
                        "/Views/{1}/{0}.cshtml",
                        "/Views/Shared/{0}.cshtml"
                    },
                    new[]
                    {
                        //"/Views/{1}/Tablet/{0}.cshtml",
                        "/Views/{1}/{0}.cshtml",
                        //"/Views/Shared/Tablet/{0}.cshtml",
                        "/Views/Shared/{0}.cshtml",
                    }
                };
            }
        }

        [Theory]
        [MemberData(nameof(ViewLocationExpanderTestData))]
        public void ExpandViewLocations_ViewLocationExpanderContext_IEnumerable_ReturnsExpected(
            ResponsiveViewLocationFormat format,
            IEnumerable<string> viewLocations,
            IEnumerable<string> expectedViewLocations)
        {
            var context = SetupViewLocationExpanderContext();
            var locationExpander = new ResponsiveViewLocationExpander(format);
            locationExpander.PopulateValues(context);
            var resultLocations = locationExpander.ExpandViewLocations(context, viewLocations).ToList();

            Assert.Equal(expectedViewLocations, resultLocations.ToList());
        }

        [Fact]
        public void ExpandViewLocations_NoDevice_ReturnsExpected()
        {
            var context = SetupViewLocationExpanderContext();
            var viewLocations = new List<string>() { "/Views/{1}/{0}.cshtml", "/Views/Shared/{0}.cshtml" };
            var locationExpander = new ResponsiveViewLocationExpander();
            var resultLocations = locationExpander.ExpandViewLocations(context, viewLocations);

            Assert.Equal(viewLocations, resultLocations);
        }

        [Fact]
        public void ExpandViewLocations_Null_IEnumerable_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsiveViewLocationExpander();
            Assert.Throws<ViewLocationExpanderContextArgumentNullException>(() => locationExpander.ExpandViewLocations(null, new List<string>()));
        }

        [Fact]
        public void ExpandViewLocations_ViewLocationExpanderContext_Null_ThrowsArgumentNullException()
        {
            var locationExpander = new ResponsiveViewLocationExpander();
            Assert.Throws<ViewLocationExpanderViewsArgumentNullException>(() => locationExpander.ExpandViewLocations(SetupViewLocationExpanderContext(), null));
        }

        private ViewLocationExpanderContext SetupViewLocationExpanderContext()
        {
            var context = new ViewLocationExpanderContext(new ActionContext(), "View", "Controller", "Area", "Page", true);
            context.Values = new Dictionary<string, string>();
            context.ActionContext.HttpContext = new DefaultHttpContext();
            context.ActionContext.HttpContext.SetDevice(new UserPerference() { Resolver = DeviceType.Tablet.ToString() });

            return context;
        }
    }
}
