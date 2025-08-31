// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Models;

namespace Wangkanai.Responsive.Services;

public interface IResponsiveService
{
   Device View { get; }
   void   PreferSet(Device desktop);
   void   PreferClear();
   bool   HasPreferred();
}