// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Responsive
{
    public class UserPreferenceTest
    {
        [Fact]
        public void Preferred_Cookie_over_Resolver()
        {
            var preference = new UserPreference(Device.Mobile, Device.Desktop);

            Assert.Equal("Desktop", preference.Preferred);
        }
    }
}
