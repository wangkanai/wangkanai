// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

namespace Wangkanai.Detection
{
    public interface IVersion
    {
        string Major { get; }
        string Minor { get; }
        string Patch { get; }
        string Build { get; }
    }
}