// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Primitives;

/// <summary>Represents a result of some operation, with status information and possibly an error.</summary>
public class Result
{
	/// <summary>Initializes a new instance of the <see cref="Result"/> class with the specified parameters.</summary>
	/// <param name="isSuccess"></param>
	/// <param name="error"></param>
	/// <exception cref="InvalidOperationException"></exception>
	protected Result(bool isSuccess, Error error)
	{
		if (isSuccess && error != Error.None)
			throw new InvalidOperationException("Invalid operation for success result.");
		if (!isSuccess && error == Error.None)
			throw new InvalidOperationException("Invalid operation for failure result.");

		IsSuccess = isSuccess;
		Error = error;
	}

	/// <summary>Gets the error.</summary>
	public Error Error { get; }

	/// <summary>Gets a value indicating whether the result is a success result.</summary>
	public bool IsSuccess { get; }

	/// <summary>Gets a value indicating whether the result is a failure result.</summary>
	public bool IsFailure => !IsSuccess;

	/// <summary>Returns a success <see cref="Result"/>.</summary>
	/// <returns>A new instance of <see cref="Result"/> with the success flag set.</returns>
	public static Result Success()
		=> new(true, Error.None);

	/// <summary>Returns a success <see cref="Result{TValue}"/> with the specified value.</summary>
	/// <param name="value">The result value.</param>
	/// <typeparam name="TValue">The result type.</typeparam>
	/// <returns>A new instance of <see cref="Result{TValue}"/> with the success flag set.</returns>
	public static Result<TValue> Success<TValue>(TValue value)
		=> new(value, true, Error.None);

	/// <summary>Returns a failure <see cref="Result"/> with the specified error.</summary>
	/// <param name="error">The error.</param>
	/// <returns>A new instance of <see cref="Result"/> with the specified error and failure flag set.</returns>
	public static Result Failure(Error error)
		=> new(false, error);

	/// <summary>Returns a failure <see cref="Result{TValue}"/> with the specified error.</summary>
	/// <param name="error">The error.</param>
	/// <typeparam name="TValue">The result type.</typeparam>
	/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified error and failure set.</returns>
	public static Result<TValue> Failure<TValue>(Error error)
		=> new(default!, false, error);

	/// <summary>Create a new <see cref="Result{TValue}"/> with the specified nullable value and the specified error.</summary>
	/// <typeparam name="TValue">The result type.</typeparam>
	/// <param name="value">The result value.</param>
	/// <param name="error">The error in case the value is null.</param>
	/// <returns>A new instance of <see cref="Result{TValue}"/> with the specified value or an error.</returns>
	public static Result<TValue> Create<TValue>(TValue? value, Error error)
		where TValue : class
		=> value is null ? Failure<TValue>(error) : Success(value);

	/// <summary>Returns the first failure result from the specified <paramref name="results"/>. If there is no failure, a success is returned.</summary>
	/// <param name="results">the result array.</param>
	/// <returns>The first failure from the specified <paramref name="results"/> array, or a success it does not exist.</returns>
	public static Result FirstFailureOrSuccess(params Result[] results)
	{
		foreach (var result in results)
			if (result.IsFailure)
				return result;

		return Success();
	}
}

/// <summary>
/// Represents the result of some operation, with status information and possibly a value and an error.
/// </summary>
/// <typeparam name="T">The result value type.</typeparam>
/// <param name="value">The result value.</param>
/// <param name="isSuccess">The flag indicating if the result is successful.</param>
/// <param name="error"></param>
public class Result<T>(T value, bool isSuccess, Error error) : Result(isSuccess, error)
{
	/// <summary>Gets the result value if the result is successful, otherwise throws an exception.</summary>
	/// <returns>The result value if the result is successful.</returns>
	/// <exception cref="InvalidOperationException"> when <see cref="Result.IsFailure"/> is true.</exception>
	public T Value
		=> IsSuccess ? value : throw new InvalidOperationException("The value of a failure result cannot be accessed.");

	/// <summary>Defines a custom operator for this class or struct.</summary>
	/// <param name="value">The value comparing the result</param>
	/// <returns>The result of applying the operator to the specified operand.</returns>
	public static implicit operator Result<T>(T value)
		=> Success(value);
}
