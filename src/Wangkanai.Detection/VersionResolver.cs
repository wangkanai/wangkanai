// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System;

namespace Wangkanai.Detection
{
    public sealed class VersionResolver : IVersionResolver
    {
        public IVersion Version => throw new NotImplementedException();

        public IUserAgent UserAgent => throw new NotImplementedException();
    }
}