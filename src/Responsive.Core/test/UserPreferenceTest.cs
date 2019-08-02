// Copyright (c) 2019 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using Wangkanai.Detection;
using Xunit;

namespace Wangkanai.Responsive.Test.Core
{
    public class UserPreferenceTest
    {
        [Fact]
        public void Preferred_Cookie_over_Resolver()
        {
            var preference = new UserPreference(DeviceType.Mobile, DeviceType.Desktop);

            Assert.Equal("Desktop", preference.Preferred);
        }
    }
}
