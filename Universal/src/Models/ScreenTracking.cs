// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Universal.Options;

namespace Wangkanai.Universal.Models;

public sealed class ScreenTracking : Send
{
	public ScreenTracking(string name)
	{
		option            = new ScreenTrackingOption();
		option.ScreenName = name;
	}

	private ScreenTrackingOption option { get; }

	public override string ToString()
	{
		return "ga('send','screenview'," + option + "});";
	}
}