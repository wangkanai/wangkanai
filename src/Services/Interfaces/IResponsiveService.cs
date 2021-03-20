// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Models;

namespace Wangkanai.Detection.Services.Interfaces
{
    public interface IResponsiveService
    {
        public Device View { get; }
        void          PreferSet(Device desktop);
        void          PreferClear();
        bool          HasPreferred();
    }
}