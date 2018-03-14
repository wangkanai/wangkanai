// Copyright (c) 2018 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.


namespace Wangkanai.Detection
{
    public interface IManufacturer
    {
        string Maker { get; }
        IVersion Version { get; }
    }
}
