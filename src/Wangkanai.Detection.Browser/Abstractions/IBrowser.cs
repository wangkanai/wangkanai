// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface IBrowser : IManufacturer
    {
        string Name { get; }
        BrowserType Type { get; }
    }
}