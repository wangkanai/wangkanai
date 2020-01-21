// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System.Collections.Generic;
using Wangkanai.Detection.Models;
using Xunit;

namespace Wangkanai.Detection.Extensions
{
    public class UserAgentExtensionsTest
    {
        [Fact]
        public void IsNull()
        {
            var agent = new UserAgent();
            Assert.True(agent.IsNullOrEmpty());
        }

        [Fact]
        public void IsEmpty()
        {
            var agent = new UserAgent("");
            Assert.True(agent.IsNullOrEmpty());
        }

        [Fact]
        public void IsNotEmpty()
        {
            var agent = new UserAgent("abc");
            Assert.False(agent.IsNullOrEmpty());
        }

        [Fact]
        public void IsLower()
        {
            var agent = new UserAgent("ABC");
            Assert.Equal("abc", agent.ToLower());
        }

        [Fact]
        public void Length()
        {
            var agentNull = new UserAgent(null);
            Assert.Equal(0, agentNull.Length());
            var agentEmpty = new UserAgent("");
            Assert.Equal(0, agentEmpty.Length());
            var agentAbc = new UserAgent("abc");
            Assert.Equal(3, agentAbc.Length());
        }

        [Fact]
        public void ContainsString()
        {
            var agent = new UserAgent("abc");
            Assert.False(agent.Contains((string) null));
            Assert.False(agent.Contains(""));
            Assert.True(agent.Contains("abc"));
            Assert.True(agent.Contains("ABC"));
        }

        [Fact]
        public void ContainsArray()
        {
            var agent = new UserAgent("abc");
            Assert.False(agent.Contains(new[] {(string) null}));
            Assert.False(agent.Contains(new[] {""}));
            Assert.False(agent.Contains(new[] {(string) null, ""}));
            Assert.True(agent.Contains(new[] {"ABC"}));
            Assert.True(agent.Contains(new[] {"ABC", "abc"}));
            Assert.False(agent.Contains(new[] {"ABCD"}));
        }

        [Fact]
        public void ContainsGeneric()
        {
            var abc = new UserAgent("abc");
            var google = new UserAgent("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
            Assert.False(abc.Contains(Crawler.Google));
            Assert.True(google.Contains(Crawler.Google));
            Assert.False(google.Contains(Crawler.Yahoo));
            Assert.False(google.Contains(Crawler.Yahoo | Crawler.Bing));
            Assert.True(google.Contains(Crawler.Google | Crawler.Bing));
        }

        [Fact]
        public void ContainsListString()
        {
            var agent = new UserAgent("Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)");
            Assert.False(agent.Contains(new List<string> {null}));
            Assert.False(agent.Contains(new List<string> {""}));
            Assert.False(agent.Contains(new List<string> {null, ""}));
            Assert.False(agent.Contains(new List<string> {"abc"}));
            Assert.True(agent.Contains(new List<string> {"google"}));
            Assert.True(agent.Contains(new List<string> {"google", "abc"}));
            Assert.True(agent.Contains(new List<string> {"abc", "google"}));
        }

        [Fact]
        public void StartsWithString()
        {
            var agent = new UserAgent("abc");
            Assert.False(agent.StartsWith((string) null));
            Assert.False(agent.StartsWith(""));
            Assert.True(agent.StartsWith("ab"));
            Assert.True(agent.StartsWith("AB"));
            Assert.False(agent.StartsWith("BC"));
        }

        [Fact]
        public void StartsWithArray()
        {
            var agent = new UserAgent("abc");
            Assert.False(agent.StartsWith(new[] {(string) null}));
            Assert.False(agent.StartsWith(new[] {""}));
            Assert.False(agent.StartsWith(new[] {(string) null, ""}));
            Assert.True(agent.StartsWith(new[] {"AB"}));
            Assert.True(agent.StartsWith(new[] {"AB", "ab"}));
            Assert.False(agent.StartsWith(new[] {"ABCD"}));
        }

        [Fact]
        public void StartsWithArrayMinumum()
        {
            var agent = new UserAgent("abcdefg");
            Assert.False(agent.StartsWith(new[] {(string) null}, 4));
            Assert.False(agent.StartsWith(new[] {""}, 4));
            Assert.False(agent.StartsWith(new[] {(string) null, ""}, 4));
            Assert.True(agent.StartsWith(new[] {"ABCD"}, 4));
            Assert.True(agent.StartsWith(new[] {"ABCD", "abcd"}, 4));
            Assert.True(agent.StartsWith(new[] {"ABCDEF"}, 4));
        }
    }
}
