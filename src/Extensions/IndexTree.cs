using System;
using System.Linq;

namespace Wangkanai.Detection.Extensions
{
    public readonly struct IndexTree
    {
        public IndexTree(string[]? keywords, int seed = 0)
        {
            if (seed > 0)
            {
                keywords = keywords?.Where(k => k.Length > seed).ToArray();
            }

            if (keywords == null || keywords.Length == 0)
            {
                _lookup = Array.Empty<IndexTree>();
                _offset = 0;
                return;
            }

            var lowerBound = keywords.Min(k => k[seed]);
            var upperBound = keywords.Max(k => k[seed]);

            _offset = lowerBound;
            _lookup = new IndexTree[upperBound - lowerBound + 1];

            foreach (var (key, list) in keywords.GroupBy(k => k[seed])
                .Select(x => (x.Key, x)))
            {
                var newKeys = list.ToArray();
                if (newKeys.Any(k => seed + 1 >= k.Length))
                {
                    _lookup[key - lowerBound] = new IndexTree(null, seed + 1);
                }
                else
                {
                    _lookup[key - lowerBound] = new IndexTree(newKeys, seed + 1);
                }
            }
        }

        private readonly IndexTree[]? _lookup;
        private readonly int          _offset;

        public bool ContainsWithAnyIn(ReadOnlySpan<char> searchSource)
        {
            while (searchSource.Length > 0)
            {
                var source = searchSource;
                
                var current = this;
                var found = true;

                while (!current.IsEnd)
                {
                    var lookup = current._lookup;

                    if (source.Length == 0 || lookup == null)
                    {
                        found = false;
                        break;
                    }

                    var c = source[0];
                    var offset = current._offset;

                    if (c - offset < 0 || c - offset >= lookup.Length)
                    {
                        found = false;
                        break;
                    }

                    current = lookup[c - offset];

                    if (current._lookup == null)
                    {
                        found = false;
                        break;
                    }

                    source = source.Slice(1);
                }

                if (found)
                {
                    return true;
                }

                searchSource = searchSource.Slice(1);
            }

            return false;
        }

        public bool StartsWithAnyIn(ReadOnlySpan<char> source)
        {
            var current = this;

            while (!current.IsEnd)
            {
                var lookup = current._lookup;
                
                if (source.Length == 0 || lookup == null)
                {
                    return false;
                }

                var c = source[0];
                var offset = current._offset;

                if (c - offset < 0 || c - offset >= lookup.Length)
                {
                    return false;
                }

                current = lookup[c - offset];
                
                if (current._lookup == null)
                {
                    return false;
                }

                source = source.Slice(1);
            }

            return true;
        }

        private bool IsEnd => _lookup?.Length == 0;
    }
}