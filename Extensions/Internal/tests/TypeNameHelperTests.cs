// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text;

namespace Wangkanai.Extensions.Internal;

public class TypeNameHelperTests
{
	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName();
		Assert.Equal("Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName(true);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName(true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName(true, true, true);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ReturnsFriendlyName_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter_WithNestedTypeDelimiter()
	{
		var type = typeof(Room);
		var result = type.GetTypeDisplayName(true, true, true, '+');
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsNull()
	{
		Room _null = null!;
		var result = _null.GetTypeDisplayName();
		Assert.Null(result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsNull_WithFullName()
	{
		Room _null = null!;
		var result = _null.GetTypeDisplayName(true);
		Assert.Null(result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance()
	{
		var instance = new Room();
		var result = instance.GetTypeDisplayName();
		Assert.Equal("Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance_WithFullName()
	{
		var instance = new Room();
		var result = instance.GetTypeDisplayName(true);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", result);
	}

	[Fact]
	public void GetTypeDisplayName_ObjectIsInstance_WithShortName()
	{
		var instance = new Room();
		var result = instance.GetTypeDisplayName(false);
		Assert.Equal("Room", result);
	}

	[Fact]
	public void Builder_ProcessType_IsGenericType()
	{
		var builder = new StringBuilder();
		var type = typeof(Room);
		var option = new TypeNameHelper.DisplayNameOptions();
		builder.ProcessType(type, option);
		Assert.Equal("Room", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessType_IsGenericType_WithFullName()
	{
		var builder = new StringBuilder();
		var type = typeof(Room);
		var option = new TypeNameHelper.DisplayNameOptions(true);
		builder.ProcessType(type, option);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessType_IsGenericType_WithFullName_WithGenericParameterNames()
	{
		var builder = new StringBuilder();
		var type = typeof(Room);
		var option = new TypeNameHelper.DisplayNameOptions(true, true);
		builder.ProcessType(type, option);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessType_IsGenericType_WithFullName_WithGenericParameterNames_WithGenericParameters()
	{
		var builder = new StringBuilder();
		var type = typeof(Room);
		var option = new TypeNameHelper.DisplayNameOptions(true, true, true);
		builder.ProcessType(type, option);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessType_IsGenericType_WithFullName_WithGenericParameterNames_WithGenericParameters_WithNestedTypeDelimiter()
	{
		var builder = new StringBuilder();
		var type = typeof(Room);
		var option = new TypeNameHelper.DisplayNameOptions(true, true, true, '+');
		builder.ProcessType(type, option);
		Assert.Equal("Wangkanai.Extensions.Internal.Room", builder.ToString());
	}

	[Fact]
	public void Builder_GenericStruct_IsGenericType()
	{
		var builder = new StringBuilder();
		var type = typeof(GenericStruct<>);
		var option = new TypeNameHelper.DisplayNameOptions();
		builder.ProcessType(type, option);
		Assert.Equal("GenericStruct", builder.ToString());
	}

	[Fact]
	public void Builder_GenericClass_IsGenericType()
	{
		var builder = new StringBuilder();
		var type = typeof(GenericClass<>);
		var option = new TypeNameHelper.DisplayNameOptions();
		builder.ProcessType(type, option);
		Assert.Equal("GenericClass", builder.ToString());
	}

	[Fact]
	public void Builder_ArrayType_IsArrayType()
	{
		var builder = new StringBuilder();
		var ints = new int[1];
		var type = ints.GetType();
		var option = new TypeNameHelper.DisplayNameOptions();
		builder.ProcessType(type, option);
		Assert.Equal("int[]", builder.ToString());
	}

	[Fact]
	public void Builder_GenericParameter_IsGenericParameter()
	{
		var builder = new StringBuilder();
		var type = typeof(GenericStruct<>);
		var t0 = type.GetGenericArguments()[0];
		var option = new TypeNameHelper.DisplayNameOptions(false, true);
		builder.ProcessType(t0, option);
		Assert.Equal("T", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessGenericType_withFullName()
	{
		var builder = new StringBuilder();
		var option = new TypeNameHelper.DisplayNameOptions(true, true);
		var type = typeof(GenericStruct<>);
		var types = type.GetGenericArguments();
		builder.ProcessGenericType(type, types, 2, option);
		Assert.Equal("Wangkanai.Extensions.Internal.GenericStruct", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessGenericType_IsNested()
	{
		var builder = new StringBuilder();
		var options = new TypeNameHelper.DisplayNameOptions(true, true);
		var type = typeof(Room.Owner<>);
		var types = type.GetGenericArguments();
		builder.ProcessGenericType(type, types, 2, options);
		Assert.Equal("Wangkanai.Extensions.Internal.Room.Owner", builder.ToString());
	}

	[Fact]
	public void Builder_ProcessGenericType_IsNested_IncludeGenericParameter()
	{
		var builder = new StringBuilder();
		var options = new TypeNameHelper.DisplayNameOptions(true, true, true);
		var type = typeof(Room.Owner<>);
		var types = type.GetGenericArguments();
		builder.ProcessGenericType(type, types, 1, options);
		Assert.Equal("Wangkanai.Extensions.Internal.Room.Owner<T>", builder.ToString());
	}
}

public class Room
{
	public class Owner<T> where T : struct;
}

public class GenericStruct<T> where T : struct;

public class GenericClass<T> where T : class;
