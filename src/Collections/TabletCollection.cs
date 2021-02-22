// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using Wangkanai.Detection.Extensions;

namespace Wangkanai.Detection.Collections
{
    internal static class TabletCollection
    {
        private static readonly string[] Keywords = 
        {
            "tablet",
            "ipad",
            "playbook",
            "hp-tablet",
            "kindle",
            "sm-t",
            "kfauwi"
        };
        
        public static readonly IndexTree KeywordsSearchTree = Keywords.BuildIndexTree();
    }
}
