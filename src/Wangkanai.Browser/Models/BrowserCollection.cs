// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Wangkanai.Browser.Models
{
    public class BrowserCollection
    {
        public List<Platform> Platforms { get; }
        public List<Engine> Engines { get; }
        public List<Device> Devices { get; }
        public List<Division> Divisions { get; }
    }
}