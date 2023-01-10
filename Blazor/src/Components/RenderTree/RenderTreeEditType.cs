// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

public enum RenderTreeEditType
{
	PrependFrame         = 1,
	RemoveFrame          = 2,
	SetAttribute         = 3,
	RemoveAttribute      = 4,
	UpdateText           = 5,
	StepIn               = 6,
	StepOut              = 7,
	UpdateMarkup         = 8,
	PermutationListEntry = 9,
	PermutationListEnd   = 10
}