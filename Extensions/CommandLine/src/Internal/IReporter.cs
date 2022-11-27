// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions.CommandLine.Internal;

public interface IReporter
{
    void Verbose(string message);
    void Output(string  message);
    void Warn(string    message);
    void Error(string   message);
}