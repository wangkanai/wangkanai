// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services.Interfaces
{
    /// <summary>
    /// Provides the APIs for query client browser rendering engine.
    /// </summary>
    public interface IEngineService
    {
        /// <summary>
        /// Get the <see cref="Engine"/> of the request client.
        /// </summary>
        public Engine Name { get; }
    }
}
