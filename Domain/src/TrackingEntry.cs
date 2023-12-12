// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Domain;

public class TrackingEntry
{
	public required object     Entity       { get; set; }
	public          EntryState EntryState   { get; set; }
	internal        bool       IsSubscribed { get; set; }

	public override string ToString() => $"{Entity ?? "null"} {EntryState}";
}
