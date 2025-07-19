// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Linq;

namespace Wangkanai.Blazor;

public class StyleMapper : BaseMapper
{
	public override string ToString()
		=> string.Join(
			"; ", Items.Select(i => i())
					   .Where(i => i != null));
}
