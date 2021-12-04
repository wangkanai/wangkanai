// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace ResponsiveView.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}