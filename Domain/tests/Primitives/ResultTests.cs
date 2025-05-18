// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Primitives;

namespace Wangkanai.Domain.Tests.Primitives;

public class ResultTests
{
	// Helper class to test protected constructor
	private class TestResult(bool isSuccess, Error error) : Result(isSuccess, error);

	#region Result Base Class Tests

	[Fact]
	public void Success_ReturnsSuccessResult()
	{
		// Act
		var result = Result.Success();

		// Assert
		Assert.True(result.IsSuccess);
		Assert.False(result.IsFailure);
		Assert.Equal(Error.None, result.Error);
	}

	[Fact]
	public void Failure_WithError_ReturnsFailureResult()
	{
		// Arrange
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Failure(error);

		// Assert
		Assert.False(result.IsSuccess);
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
	}

	[Fact]
	public void Constructor_SuccessWithNonNoneError_ThrowsInvalidOperationException()
	{
		// Arrange
		var error = new Error("Test.Error", "Test error message");

		// Act & Assert
		var exception = Assert.Throws<InvalidOperationException>(() =>
			new TestResult(true, error));

		Assert.Equal("Invalid operation for success result.", exception.Message);
	}

	[Fact]
	public void Constructor_FailureWithNoneError_ThrowsInvalidOperationException()
	{
		// Act & Assert
		var exception = Assert.Throws<InvalidOperationException>(() =>
			new TestResult(false, Error.None));

		Assert.Equal("Invalid operation for failure result.", exception.Message);
	}

	[Fact]
	public void FirstFailureOrSuccess_AllSuccess_ReturnsSuccess()
	{
		// Arrange
		var success1 = Result.Success();
		var success2 = Result.Success();
		var success3 = Result.Success();

		// Act
		var result = Result.FirstFailureOrSuccess(success1, success2, success3);

		// Assert
		Assert.True(result.IsSuccess);
	}

	[Fact]
	public void FirstFailureOrSuccess_WithFailures_ReturnsFirstFailure()
	{
		// Arrange
		var success  = Result.Success();
		var failure1 = Result.Failure(new Error("Test.Error1", "Test error 1"));
		var failure2 = Result.Failure(new Error("Test.Error2", "Test error 2"));

		// Act
		var result = Result.FirstFailureOrSuccess(success, failure1, failure2);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(failure1.Error, result.Error);
	}

	[Fact]
	public void FirstFailureOrSuccess_EmptyArray_ReturnsSuccess()
	{
		// Act
		var result = Result.FirstFailureOrSuccess();

		// Assert
		Assert.True(result.IsSuccess);
	}

	[Fact]
	public void FirstFailureOrSuccess_FailureInMiddle_ReturnsFirstFailure()
	{
		// Arrange
		var success1 = Result.Success();
		var failure  = Result.Failure(new Error("Test.Error", "Test error"));
		var success2 = Result.Success();

		// Act
		var result = Result.FirstFailureOrSuccess(success1, failure, success2);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(failure.Error, result.Error);
	}

	[Fact]
	public void FirstFailureOrSuccess_MultipleFailures_ReturnsFirstFailure()
	{
		// Arrange
		var failure1 = Result.Failure(new Error("Test.Error1", "Test error 1"));
		var failure2 = Result.Failure(new Error("Test.Error2", "Test error 2"));
		var failure3 = Result.Failure(new Error("Test.Error3", "Test error 3"));

		// Act
		var result = Result.FirstFailureOrSuccess(failure1, failure2, failure3);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(failure1.Error, result.Error);
	}

	#endregion

	#region Result<T> Generic Class Tests

	[Fact]
	public void GenericSuccess_WithValue_ReturnsSuccessResultWithValue()
	{
		// Arrange
		var value = "test value";

		// Act
		var result = Result.Success(value);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.False(result.IsFailure);
		Assert.Equal(Error.None, result.Error);
		Assert.Equal(value, result.Value);
	}

	[Fact]
	public void GenericFailure_WithError_ReturnsFailureResult()
	{
		// Arrange
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Failure<string>(error);

		// Assert
		Assert.False(result.IsSuccess);
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
	}

	[Fact]
	public void GenericValue_OnFailure_ThrowsInvalidOperationException()
	{
		// Arrange
		var error  = new Error("Test.Error", "Test error message");
		var result = Result.Failure<string>(error);

		// Act & Assert
		var exception = Assert.Throws<InvalidOperationException>(() => result.Value);
		Assert.Equal("The value of a failure result cannot be accessed.", exception.Message);
	}

	[Fact]
	public void GenericSuccess_WithNullValue_AllowsNullValue()
	{
		// Arrange
		string? value = null;

		// Act
		var result = Result.Success<string?>(value);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Null(result.Value);
	}

	[Fact]
	public void Create_WithNonNullValue_ReturnsSuccess()
	{
		// Arrange
		var value = "test value";
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Create(value, error);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(value, result.Value);
	}

	[Fact]
	public void Create_WithNullValue_ReturnsFailure()
	{
		// Arrange
		string? value = null;
		var     error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Create(value, error);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
	}

	[Fact]
	public void ImplicitConversion_FromValue_CreatesSuccessResult()
	{
		// Arrange
		string value = "test value";

		// Act
		Result<string> result = value;

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(value, result.Value);
	}

	[Fact]
	public void Create_WithCustomClass_ReturnsSuccess()
	{
		// Arrange
		var value = new TestClass { Name = "test" };
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Create(value, error);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(value, result.Value);
	}

	[Fact]
	public void Create_WithNullCustomClass_ReturnsFailure()
	{
		// Arrange
		TestClass? value = null;
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Create(value, error);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
	}

	[Fact]
	public void ImplicitConversion_WithComplexObject_CreatesSuccessResult()
	{
		// Arrange
		var complexObject = new TestClass { Name = "test" };

		// Act
		Result<TestClass> result = complexObject;

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(complexObject, result.Value);
	}

	[Fact]
	public void Success_WithValueType_ReturnsSuccessResult()
	{
		// Arrange
		int value = 42;

		// Act
		var result = Result.Success(value);

		// Assert
		Assert.True(result.IsSuccess);
		Assert.Equal(value, result.Value);
	}

	[Fact]
	public void Failure_WithGenericValueType_ReturnsFailureResult()
	{
		// Arrange
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Failure<int>(error);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
		Assert.Throws<InvalidOperationException>(() => result.Value);
	}

	[Theory]
	[InlineData(null)]
	//[InlineData("")]
	//[InlineData(" ")]
	public void Create_WithNullOrEmptyString_ReturnsFailure(string? value)
	{
		// Arrange
		var error = new Error("Test.Error", "Test error message");

		// Act
		var result = Result.Create(value, error);

		// Assert
		Assert.True(result.IsFailure);
		Assert.Equal(error, result.Error);
	}

	private class TestClass
	{
		public string Name { get; set; } = string.Empty;
	}

	#endregion
}
