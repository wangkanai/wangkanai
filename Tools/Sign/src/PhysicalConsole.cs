// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Sign.Internal;

namespace Wangkanai.Sign;

public class PhysicalConsole : IConsole
{
    private PhysicalConsole()
    {
        Console.CancelKeyPress += (o, e) => { CancelKeyPress?.Invoke(o, e); };
    }
    
    public static IConsole Singleton { get; } = new PhysicalConsole();

    public event ConsoleCancelEventHandler CancelKeyPress;
}