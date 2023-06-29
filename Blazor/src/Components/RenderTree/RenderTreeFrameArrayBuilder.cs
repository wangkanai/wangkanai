// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components.RenderTree;

internal sealed class RenderTreeFrameArrayBuilder : ArrayBuilder<RenderTreeFrame>
{
	public void AppendElement(int sequence, string elementName)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField    = sequence,
			FrameTypeField   = RenderTreeFrameType.Element,
			ElementNameField = elementName
		};
	}

	public void AppendText(int sequence, string textContent)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField    = sequence,
			FrameTypeField   = RenderTreeFrameType.Text,
			TextContentField = textContent
		};
	}

	public void AppendMarkup(int sequence, string markupContent)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField      = sequence,
			FrameTypeField     = RenderTreeFrameType.Markup,
			MarkupContentField = markupContent
		};
	}

	public void AppendAttribute(int sequence, string attributeName, object? attributeValue)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField       = sequence,
			FrameTypeField      = RenderTreeFrameType.Attribute,
			AttributeNameField  = attributeName,
			AttributeValueField = attributeValue
		};
	}

	public void AppendComponent(int sequence, [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField      = sequence,
			FrameTypeField     = RenderTreeFrameType.Component,
			ComponentTypeField = componentType
		};
	}

	public void AppendElementReferenceCapture(int sequence, Action<ElementReference> elementReferenceCaptureAction)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField                      = sequence,
			FrameTypeField                     = RenderTreeFrameType.ElementReferenceCapture,
			ElementReferenceCaptureActionField = elementReferenceCaptureAction
		};
	}

	public void AppendComponentReferenceCapture(int sequence, Action<object> componentReferenceCaptureAction, int parentFrameIndexValue)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField                                  = sequence,
			FrameTypeField                                 = RenderTreeFrameType.ComponentReferenceCapture,
			ComponentReferenceCaptureActionField           = componentReferenceCaptureAction,
			ComponentReferenceCaptureParentFrameIndexField = parentFrameIndexValue
		};
	}

	public void AppendRegion(int sequence)
	{
		if (_itemsInUse == _items.Length)
			GrowBuffer(_items.Length * 2);

		_items[_itemsInUse++] = new RenderTreeFrame
		{
			SequenceField  = sequence,
			FrameTypeField = RenderTreeFrameType.Region
		};
	}
}