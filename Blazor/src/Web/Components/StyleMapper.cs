// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq;

namespace Wangkanai.Blazor;

public class StyleMapper : BaseMapper
{
	public override string ToString()
	{
		return string.Join(
			"; ", Items.Select(i => i())
			           .Where(i => i != null));
	}
}