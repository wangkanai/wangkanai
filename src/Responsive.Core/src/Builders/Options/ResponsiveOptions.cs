// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Wangkanai.Detection;

namespace Wangkanai.Responsive
{
    /// <summary>
    /// Provides programmatic configuration for the Responsive framework.
    /// </summary>
    public class ResponsiveOptions
    {
        /// <summary>
        /// Get or set the option to configure the default view.
        /// </summary>
        public ViewLocationOptions View { get; set; } = new ViewLocationOptions();
    }
}
