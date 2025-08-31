// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Xml.Serialization;

namespace Wangkanai.Extensions;

/// <summary>Provides extension methods for object serialization and deserialization.</summary>
[DebuggerStepThrough]
public static class SerializationExtensions
{
   /// <summary>Extension method that takes objects and serialized them.</summary>
   /// <typeparam name="T">The type of the object to be serialized.</typeparam>
   /// <param name="source">The object to be serialized.</param>
   /// <returns>A string that represents the serialized XML.</returns>
   public static string SerializeXml<T>(this T source)
      where T : class, new()
   {
      source.ThrowIfNull();

      var       serializer = new XmlSerializer(typeof(T));
      using var writer     = new StringWriter();
      serializer.Serialize(writer, source);
      return writer.ToString();
   }

   /// <summary>Extension method to string which attempts to deserialize XML with the same name as the source string.</summary>
   /// <typeparam name="T">The type which to be deserialized to.</typeparam>
   /// <param name="xml">The source string</param>
   /// <returns>The deserialized object, or null if unsuccessful.</returns>
   public static T DeserializeXml<T>(this string xml)
      where T : class, new()
   {
      xml.ThrowIfNull();

      var       serializer = new XmlSerializer(typeof(T));
      using var reader     = new StringReader(xml);
      return ((T)serializer.Deserialize(reader)!)!;
   }
}