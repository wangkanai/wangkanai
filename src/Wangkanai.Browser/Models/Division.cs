// Copyright (c) 2016 Sarin Na Wangkanai, All Rights Reserved.
// The GNU GPLv3. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Wangkanai.Browser.Models
{
    public class Division
    {
        private readonly string _name = null;
        private readonly int _sortIndex = 0;
        private readonly bool _lite = false;
        private readonly bool _standand = false;
        private readonly List<string> _versions = new List<string>();
        private readonly List<string> _userAgents = new List<string>();

        public Division(string name, int sortIndex, List<string> userAgents, bool lite, bool standand, List<string> versions)
        {
            _name = name;
            _sortIndex = sortIndex;
            _userAgents = userAgents;
            _lite = lite;
            _standand = standand;
            _versions = versions;
        }

        public string Name => _name;
        public int SortIndex => _sortIndex;
        public bool IsLite => _lite;
        public bool IsStandand => _standand;
        public List<string> Versions => _versions;
        public List<string> UserAgents => _userAgents;
    }
}