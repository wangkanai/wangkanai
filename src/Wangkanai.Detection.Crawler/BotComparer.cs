using System.Collections.Generic;

namespace Wangkanai.Detection
{
    public class BotComparer : IEqualityComparer<string>
    {
        public bool Equals(string x, string y)
        {
            return x.Contains(y);
        }

        public int GetHashCode(string obj)
        {
            if (obj == null || obj == string.Empty) return 0;

            return obj.GetHashCode();
        }
    }
}
