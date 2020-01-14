// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace Wangkanai.Detection.DependencyInjection.Options
{
    public class DetectionOptions
    {
        public bool Demo { get; set; } = false;
        public ResponsiveOptions Responsive { get; set; } = new ResponsiveOptions();
    }
}
