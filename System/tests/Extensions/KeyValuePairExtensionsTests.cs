// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class KeyValuePairExtensionsTests
{
	  [Fact]
	  public void WithKey_ShouldReturnKeyValuePairWithNewKey()
	  {
		  // Arrange
		  var keyvalue = new KeyValuePair<int, string>(1, "One");
		  var newKey = 2;

		  // Act
		  var result = keyvalue.WithKey(newKey);

		  // Assert
		  Assert.Equal(newKey, result.Key);
		  Assert.Equal(keyvalue.Value, result.Value);
	  }

	  [Fact]
	  public void WithValue_ShouldReturnKeyValuePairWithNewValue()
	  {
		  // Arrange
		  var keyvalue = new KeyValuePair<int, string>(1, "One");
		  var newValue = "Two";

		  // Act
		  var result = keyvalue.WithValue(newValue);

		  // Assert
		  Assert.Equal(keyvalue.Key, result.Key);
		  Assert.Equal(newValue, result.Value);
	  }

	  [Fact]
	  public void WithKey_ShouldReturnKeyValuePairWithNewKey_WhenKeyValuePairIsNull()
	  {
		  // Arrange
		  KeyValuePair<int, string> keyvalue = default;
		  var newKey = 2;

		  // Act
		  var result = keyvalue.WithKey(newKey);

		  // Assert
		  Assert.Equal(newKey, result.Key);
		  Assert.Equal(default, result.Value);
	  }

	  [Fact]
	  public void WithValue_ShouldReturnKeyValuePairWithNewValue_WhenKeyValuePairIsNull()
	  {
		  // Arrange
		  KeyValuePair<int, string> keyvalue = default;
		  var newValue = "Two";

		  // Act
		  var result = keyvalue.WithValue(newValue);

		  // Assert
		  Assert.Equal(default, result.Key);
		  Assert.Equal(newValue, result.Value);
	  }

	  [Fact]
	  public void WithKey_ShouldReturnKeyValuePairWithNewKey_WhenKeyValuePairIsNullAndNewKeyIsNull()
	  {
		  // Arrange
		  KeyValuePair<int, string> keyvalue = default;
		  int newKey = default;

		  // Act
		  var result = keyvalue.WithKey(newKey);

		  // Assert
		  Assert.Equal(newKey, result.Key);
		  Assert.Equal(default, result.Value);
	  }
}
