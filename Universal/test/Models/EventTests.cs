// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;

using Xunit;

namespace Wangkanai.Universal.Models
{
    public class EventTests
    {
        [Fact]
        public void TestEventCategoryOnly()
        {
            Event categoryevent = new Event("button", "click", "submit", "1");
            Assert.Equal("'event','button','click','submit','1'", categoryevent.ToString());
        }
    }
}