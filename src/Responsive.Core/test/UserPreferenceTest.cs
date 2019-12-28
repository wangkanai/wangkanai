// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Responsive.Core
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
