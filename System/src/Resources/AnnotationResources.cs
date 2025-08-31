// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Resources;

/// <summary>Contains constant string values representing different null-handling behaviors. This class is intended for internal use within the library to standardize messages associated with scenarios where null values are encountered.</summary>
internal static class AnnotationResources
{
   internal const string ValueNullThenHalt    = "null => halt";
   internal const string ValueNullThenStop    = "null => stop";
   internal const string ValueNullThenVoid    = "null => void";
   internal const string ValueNullThenNothing = "null => nothing";

   internal const string ValueNullThenTrue  = "null => true";
   internal const string ValueNullThenFalse = "null => false";
}