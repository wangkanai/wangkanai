// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;
using System.Reflection;

using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Wangkanai.Markdown.ApplicationModels;

[DebuggerDisplay("MarkdownParameterModel: Name={ParameterName}")]
public class MarkdownParameterModel : ParameterModelBase, ICommonModel, IBindingModel
{
   public MarkdownParameterModel(
      ParameterInfo         parameterInfo,
      IReadOnlyList<object> attributes)
      : base(parameterInfo.ParameterType, attributes)
   {
      parameterInfo.ThrowIfNull();
      attributes.ThrowIfNull();

      ParameterInfo = parameterInfo;
   }

   public MarkdownParameterModel(MarkdownParameterModel other)
      : base(other)
   {
      other.ThrowIfNull();

      Handler       = other.Handler;
      ParameterInfo = other.ParameterInfo;
   }

   public MarkdownHandlerModel Handler { get; set; } = default!;

   public ParameterInfo ParameterInfo { get; }

   public string ParameterName
   {
      get => Name;
      set => Name = value;
   }

   MemberInfo ICommonModel.MemberInfo => ParameterInfo.Member;
}