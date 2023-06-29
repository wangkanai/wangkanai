// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Blazor.Components.RenderTree;

public enum RenderTreeFrameType : short
{
	None                      = 0,
	Element                   = 1,
	Text                      = 2,
	Attribute                 = 3,
	Component                 = 4,
	Region                    = 5,
	ElementReferenceCapture   = 6,
	ComponentReferenceCapture = 7,
	Markup                    = 8
}