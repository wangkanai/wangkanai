// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

using Wangkanai.Blazor.Components.Rendering;
using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components.RenderTree;

[StructLayout(LayoutKind.Explicit, Pack = 4)]
public struct RenderTreeFrame
{
	// Note that the struct layout has to be valid in both 32-bit and 64-bit runtime platforms,
	// which means that all reference-type fields need to take up 8 bytes (except for the last
	// one, which will be sized as either 4 or 8 bytes depending on the runtime platform).

	// Although each frame type uses the slots for different purposes, the runtime does not
	// allow reference type slots to overlap with each other or with value-type slots.
	// Here's the current layout:
	//
	// Offset   Type
	// ------   ----
	// 0-3      Int32 (sequence number)
	// 4-5      Int16 (frame type)
	// 6-15     Value types (usage varies by frame type)
	// 16-23    Reference type (usage varies by frame type)
	// 24-31    Reference type (usage varies by frame type)
	// 32-39    Reference type (usage varies by frame type)
	//
	// On Mono WebAssembly, because it's 32-bit, the final slot occupies bytes 32-35,
	// so the struct length is only 36.

	// --------------------------------------------------------------------------------
	// Common
	// --------------------------------------------------------------------------------

	[FieldOffset(0)] internal int                 SequenceField;
	[FieldOffset(4)] internal RenderTreeFrameType FrameTypeField;

	/// <summary>
	///     Gets the sequence number of the frame. Sequence numbers indicate the relative source
	///     positions of the instructions that inserted the frames. Sequence numbers are only
	///     comparable within the same sequence (typically, the same source method).
	/// </summary>
	public int Sequence => SequenceField;

	/// <summary>
	///     Describes the type of this frame.
	/// </summary>
	public RenderTreeFrameType FrameType => FrameTypeField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Element
	// --------------------------------------------------------------------------------

	[FieldOffset(8)]  internal int    ElementSubtreeLengthField;
	[FieldOffset(16)] internal string ElementNameField;
	[FieldOffset(24)] internal object ElementKeyField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Element" />
	///     gets the number of frames in the subtree for which this frame is the root.
	///     The value is zero if the frame has not yet been closed.
	/// </summary>
	public int ElementSubtreeLength => ElementSubtreeLengthField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Element" />,
	///     gets a name representing the type of the element. Otherwise, the value is undefined.
	/// </summary>
	public string ElementName => ElementNameField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Element" />,
	///     gets the element's diffing key, or null if none was specified.
	/// </summary>
	public object ElementKey => ElementKeyField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Text
	// --------------------------------------------------------------------------------

	[FieldOffset(16)] internal string TextContentField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Text" />,
	///     gets the content of the text frame. Otherwise, the value is undefined.
	/// </summary>
	public string TextContent => TextContentField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Attribute
	// --------------------------------------------------------------------------------

	[FieldOffset(8)]  internal ulong  AttributeEventHandlerIdField;
	[FieldOffset(16)] internal string AttributeNameField;
	[FieldOffset(24)] internal object AttributeValueField;
	[FieldOffset(32)] internal string AttributeEventUpdatesAttributeNameField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Attribute" />
	///     gets the ID of the corresponding event handler, if any.
	/// </summary>
	public ulong AttributeEventHandlerId => AttributeEventHandlerIdField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Attribute" />,
	///     gets the attribute name. Otherwise, the value is undefined.
	/// </summary>
	public string AttributeName => AttributeNameField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Attribute" />,
	///     gets the attribute value. Otherwise, the value is undefined.
	/// </summary>
	public object AttributeValue => AttributeValueField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Attribute" />,
	///     and the attribute represents an event handler, gets the name of another attribute whose value
	///     can be updated to represent the UI state prior to executing the event handler. This is
	///     primarily used in two-way bindings.
	/// </summary>
	public string AttributeEventUpdatesAttributeName => AttributeEventUpdatesAttributeNameField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Component
	// --------------------------------------------------------------------------------

	[FieldOffset(8)]  internal int ComponentSubtreeLengthField;
	[FieldOffset(12)] internal int ComponentIdField;

	[FieldOffset(16)]
	[DynamicallyAccessedMembers(LinkerFlags.Component)]
	internal Type ComponentTypeField;

	[FieldOffset(24)] internal ComponentState ComponentStateField;
	[FieldOffset(32)] internal object         ComponentKeyField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />
	///     gets the number of frames in the subtree for which this frame is the root.
	///     The value is zero if the frame has not yet been closed.
	/// </summary>
	public int ComponentSubtreeLength => ComponentSubtreeLengthField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />,
	///     gets the child component instance identifier.
	/// </summary>
	public int ComponentId => ComponentIdField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />,
	///     gets the type of the child component.
	/// </summary>
	public Type ComponentType => ComponentTypeField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />,
	///     gets the child component state object. Otherwise, the value is undefined.
	/// </summary>
	internal ComponentState ComponentState => ComponentStateField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />,
	///     gets the component's diffing key, or null if none was specified.
	/// </summary>
	public object ComponentKey => ComponentKeyField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Component" />,
	///     gets the child component instance. Otherwise, the value is undefined.
	/// </summary>
	public IComponent Component => ComponentStateField?.Component;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Region
	// --------------------------------------------------------------------------------

	[FieldOffset(8)] internal int RegionSubtreeLengthField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Region" />
	///     gets the number of frames in the subtree for which this frame is the root.
	///     The value is zero if the frame has not yet been closed.
	/// </summary>
	public int RegionSubtreeLength => RegionSubtreeLengthField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.ElementReferenceCapture
	// --------------------------------------------------------------------------------

	[FieldOffset(16)] internal string                   ElementReferenceCaptureIdField;
	[FieldOffset(24)] internal Action<ElementReference> ElementReferenceCaptureActionField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.ElementReferenceCapture" />,
	///     gets the ID of the reference capture. Otherwise, the value is undefined.
	/// </summary>
	public string ElementReferenceCaptureId => ElementReferenceCaptureIdField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.ElementReferenceCapture" />,
	///     gets the action that writes the reference to its target. Otherwise, the value is undefined.
	/// </summary>
	public Action<ElementReference> ElementReferenceCaptureAction => ElementReferenceCaptureActionField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.ComponentReferenceCapture
	// --------------------------------------------------------------------------------

	[FieldOffset(8)]  internal int            ComponentReferenceCaptureParentFrameIndexField;
	[FieldOffset(16)] internal Action<object> ComponentReferenceCaptureActionField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.ComponentReferenceCapture" />,
	///     gets the index of the parent frame representing the component being captured. Otherwise, the value is undefined.
	///     WARNING: This index can only be used in the context of the frame's original render tree. If the frame is
	///     copied elsewhere, such as to the ReferenceFrames buffer of a RenderTreeDiff, then the index will
	///     not relate to entries in that other buffer.
	///     Currently there's no scenario where this matters, but if there was, we could change all of the subtree
	///     initialization logic in RenderTreeDiffBuilder to walk the frames hierarchically, then it would know
	///     the parent index at the point where it wants to initialize the ComponentReferenceCapture frame.
	/// </summary>
	public int ComponentReferenceCaptureParentFrameIndex => ComponentReferenceCaptureParentFrameIndexField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.ComponentReferenceCapture" />,
	///     gets the action that writes the reference to its target. Otherwise, the value is undefined.
	/// </summary>
	public Action<object> ComponentReferenceCaptureAction => ComponentReferenceCaptureActionField;

	// --------------------------------------------------------------------------------
	// RenderTreeFrameType.Markup
	// --------------------------------------------------------------------------------

	[FieldOffset(16)] internal string MarkupContentField;

	/// <summary>
	///     If the <see cref="FrameType" /> property equals <see cref="RenderTreeFrameType.Markup" />,
	///     gets the content of the markup frame. Otherwise, the value is undefined.
	/// </summary>
	public string MarkupContent => MarkupContentField;

	// Element constructor
	private RenderTreeFrame(int sequence, int elementSubtreeLength, string elementName, object elementKey)
		: this()
	{
		SequenceField             = sequence;
		FrameTypeField            = RenderTreeFrameType.Element;
		ElementSubtreeLengthField = elementSubtreeLength;
		ElementNameField          = elementName;
		ElementKeyField           = elementKey;
	}

	// Component constructor
	private RenderTreeFrame(int sequence, int componentSubtreeLength, [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType, ComponentState componentState, object componentKey)
		: this()
	{
		SequenceField               = sequence;
		FrameTypeField              = RenderTreeFrameType.Component;
		ComponentSubtreeLengthField = componentSubtreeLength;
		ComponentTypeField          = componentType;
		ComponentKeyField           = componentKey;

		if (componentState != null)
		{
			ComponentStateField = componentState;
			ComponentIdField    = componentState.ComponentId;
		}
	}

	// Region constructor
	private RenderTreeFrame(int sequence, int regionSubtreeLength)
		: this()
	{
		SequenceField            = sequence;
		FrameTypeField           = RenderTreeFrameType.Region;
		RegionSubtreeLengthField = regionSubtreeLength;
	}

	// Text/markup constructor
	private RenderTreeFrame(int sequence, bool isMarkup, string textOrMarkup)
		: this()
	{
		SequenceField = sequence;
		if (isMarkup)
		{
			FrameTypeField     = RenderTreeFrameType.Markup;
			MarkupContentField = textOrMarkup;
		}
		else
		{
			FrameTypeField   = RenderTreeFrameType.Text;
			TextContentField = textOrMarkup;
		}
	}

	// Attribute constructor
	private RenderTreeFrame(int sequence, string attributeName, object attributeValue, ulong attributeEventHandlerId, string attributeEventUpdatesAttributeName)
		: this()
	{
		FrameTypeField                          = RenderTreeFrameType.Attribute;
		SequenceField                           = sequence;
		AttributeNameField                      = attributeName;
		AttributeValueField                     = attributeValue;
		AttributeEventHandlerIdField            = attributeEventHandlerId;
		AttributeEventUpdatesAttributeNameField = attributeEventUpdatesAttributeName;
	}

	// Element reference capture constructor
	private RenderTreeFrame(int sequence, Action<ElementReference> elementReferenceCaptureAction, string elementReferenceCaptureId)
		: this()
	{
		FrameTypeField                     = RenderTreeFrameType.ElementReferenceCapture;
		SequenceField                      = sequence;
		ElementReferenceCaptureActionField = elementReferenceCaptureAction;
		ElementReferenceCaptureIdField     = elementReferenceCaptureId;
	}

	// Component reference capture constructor
	private RenderTreeFrame(int sequence, Action<object> componentReferenceCaptureAction, int parentFrameIndex)
		: this()
	{
		FrameTypeField                                 = RenderTreeFrameType.ComponentReferenceCapture;
		SequenceField                                  = sequence;
		ComponentReferenceCaptureActionField           = componentReferenceCaptureAction;
		ComponentReferenceCaptureParentFrameIndexField = parentFrameIndex;
	}

	internal static RenderTreeFrame Element(int sequence, string elementName)
	{
		return new(sequence, 0, elementName, null);
	}

	internal static RenderTreeFrame Text(int sequence, string textContent)
	{
		return new(sequence, false, textContent);
	}

	internal static RenderTreeFrame Markup(int sequence, string markupContent)
	{
		return new(sequence, true, markupContent);
	}

	internal static RenderTreeFrame Attribute(int sequence, string name, object value)
	{
		return new(sequence, name, value, 0, null);
	}

	internal static RenderTreeFrame ChildComponent(int sequence, [DynamicallyAccessedMembers(LinkerFlags.Component)] Type componentType)
	{
		return new(sequence, 0, componentType, null, null);
	}

	internal static RenderTreeFrame PlaceholderChildComponentWithSubtreeLength(int subtreeLength)
	{
		return new(0, subtreeLength, typeof(IComponent), null, null);
	}

	internal static RenderTreeFrame Region(int sequence)
	{
		return new(sequence, 0);
	}

	internal static RenderTreeFrame ElementReferenceCapture(int sequence, Action<ElementReference> elementReferenceCaptureAction)
	{
		return new(sequence, elementReferenceCaptureAction, null);
	}

	internal static RenderTreeFrame ComponentReferenceCapture(int sequence, Action<object> componentReferenceCaptureAction, int parentFrameIndex)
	{
		return new(sequence, componentReferenceCaptureAction, parentFrameIndex);
	}

	internal RenderTreeFrame WithElementSubtreeLength(int elementSubtreeLength)
	{
		return new(SequenceField, elementSubtreeLength, ElementNameField, ElementKeyField);
	}

	internal RenderTreeFrame WithComponentSubtreeLength(int componentSubtreeLength)
	{
		return new(SequenceField, componentSubtreeLength, ComponentTypeField, ComponentStateField, ComponentKeyField);
	}

	internal RenderTreeFrame WithAttributeSequence(int sequence)
	{
		return new(sequence, AttributeNameField, AttributeValueField, AttributeEventHandlerIdField, AttributeEventUpdatesAttributeNameField);
	}

	internal RenderTreeFrame WithComponent(ComponentState componentState)
	{
		return new(SequenceField, ComponentSubtreeLengthField, ComponentTypeField, componentState, ComponentKeyField);
	}

	internal RenderTreeFrame WithAttributeEventHandlerId(ulong eventHandlerId)
	{
		return new(SequenceField, AttributeNameField, AttributeValueField, eventHandlerId, AttributeEventUpdatesAttributeNameField);
	}

	internal RenderTreeFrame WithAttributeValue(object attributeValue)
	{
		return new(SequenceField, AttributeNameField, attributeValue, AttributeEventHandlerIdField, AttributeEventUpdatesAttributeNameField);
	}

	internal RenderTreeFrame WithAttributeEventUpdatesAttributeName(string attributeUpdatesAttributeName)
	{
		return new(SequenceField, AttributeNameField, AttributeValueField, AttributeEventHandlerIdField, attributeUpdatesAttributeName);
	}

	internal RenderTreeFrame WithRegionSubtreeLength(int regionSubtreeLength)
	{
		return new(SequenceField, regionSubtreeLength);
	}

	internal RenderTreeFrame WithElementReferenceCaptureId(string elementReferenceCaptureId)
	{
		return new(SequenceField, ElementReferenceCaptureActionField, elementReferenceCaptureId);
	}

	internal RenderTreeFrame WithElementKey(object elementKey)
	{
		return new(SequenceField, ElementSubtreeLengthField, ElementNameField, elementKey);
	}

	internal RenderTreeFrame WithComponentKey(object componentKey)
	{
		return new(SequenceField, ComponentSubtreeLengthField, ComponentTypeField, ComponentStateField, componentKey);
	}

	/// <inheritdoc />
	// Just to be nice for debugging and unit tests.
	public override string ToString()
	{
		return FrameTypeField switch
		{
			RenderTreeFrameType.Attribute               => $"Attribute: (seq={Sequence}, id={AttributeEventHandlerId}) '{AttributeName}'='{AttributeValue}'",
			RenderTreeFrameType.Component               => $"Component: (seq={Sequence}, key={ComponentKeyField ?? "(none)"}, len={ComponentSubtreeLength}) {ComponentType}",
			RenderTreeFrameType.Element                 => $"Element: (seq={Sequence}, key={ElementKeyField ?? "(none)"}, len={ElementSubtreeLength}) {ElementName}",
			RenderTreeFrameType.Region                  => $"Region: (seq={Sequence}, len={RegionSubtreeLength})",
			RenderTreeFrameType.Text                    => $"Text: (seq={Sequence}, len=n/a) {EscapeNewlines(TextContentField)}",
			RenderTreeFrameType.Markup                  => $"Markup: (seq={Sequence}, len=n/a) {EscapeNewlines(TextContentField)}",
			RenderTreeFrameType.ElementReferenceCapture => $"ElementReferenceCapture: (seq={Sequence}, len=n/a) {ElementReferenceCaptureAction}",
			_                                           => base.ToString()
		};
	}

	private static string EscapeNewlines(string text)
	{
		return text.Replace("\n", "\\n").Replace("\r\n", "\\r\\n");
	}
}