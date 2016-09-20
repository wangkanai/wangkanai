// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using Wangkanai.Detection.Abstractions;

namespace Wangkanai.Detection
{
    public interface IPlatformResolver : IResolver
    {
        IPlatform Platform { get; }
    }
}