// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Collections;

/// <summary>Represents a range of values, an <see cref="IComparer{T}"/> is used to compare specific values with a start and end point. A range may be included or exclude each end individually.</summary>
public class Range<T>
{
   /// <summary>Constructor a new inclusive range using the default comparer</summary>
   public Range(T start, T end)
      : this(start, end, Comparer<T>.Default) { }

   /// <summary>Constructor a new including or excluding each end as specified with the given comparer</summary>
   public Range(T start, T end, IComparer<T> comparer, bool includesStart = true, bool includesEnd = true)
   {
      if (comparer.Compare(start, end) > 0)
      {
         throw new ArgumentOutOfRangeException(nameof(end), "start must be lower than end according to comparer");
      }

      Start         = start;
      End           = end;
      Comparer      = comparer;
      IncludesStart = includesStart;
      IncludesEnd   = includesEnd;
   }

   /// <summary>The start of the range</summary>
   public T Start { get; }

   /// <summary>The end of the range</summary>
   public T End { get; }

   /// <summary>Comparer to use for comparisons</summary>
   public IComparer<T> Comparer { get; }

   /// <summary>Whether or no this range includes the start point</summary>
   public bool IncludesStart { get; }

   /// <summary>Whether or no this range includes the end point</summary>
   public bool IncludesEnd { get; }

   /// <summary>Returns a range with the same boundaries as this, but excluding the end point. When called on a range already excluding the end point, returns the original range.</summary>
   public Range<T> ExcludeEnd()
      => IncludesEnd
         ? new(Start, End, Comparer, IncludesStart, false)
         : this;

   /// <summary>Returns a range with the same boundaries as this, but excluding the start point. When called on a range already excluding the start point, returns the original range.</summary>
   public Range<T> ExcludeStart()
      => IncludesStart
         ? new(Start, End, Comparer, false, IncludesEnd)
         : this;

   /// <summary>Returns a range with the same boundaries as this, but including the end point. When called on a range already including the end point, returns the original range is returned.</summary>
   public Range<T> IncludeEnd()
      => !IncludesEnd
         ? new(Start, End, Comparer, IncludesStart)
         : this;

   /// <summary>Returns a range with the same boundaries as this, but including the start point. When called on a range already excluding the start point, returns the original range.</summary>
   public Range<T> IncludeStart()
      => !IncludesStart
         ? new(Start, End, Comparer, true, IncludesEnd)
         : this;

   /// <summary>Returns whether the range contains the given value</summary>
   public bool Contains(T value)
   {
      var lower = Comparer.Compare(value, Start);
      if (lower < 0 || lower == 0 && !IncludesStart)
      {
         return false;
      }

      var upper = Comparer.Compare(value, End);

      if (upper < 0 || upper == 0 && IncludesEnd)
      {
         return true;
      }

      return false;
   }

   /// <summary>Returns an iterator which begins at the start of this range, applying the given step on each iteration until the end is reached or passed. The start and end points are included or excluded according to this range.</summary>
   /// <param name="step">Delegate to apply to the "current value" on each iteration</param>
   public RangeIterator<T> FromStart(Func<T, T> step)
      => new(this, step);

   /// <summary>Return an iterator which begins at the end of this range, applying the given step delegate on each iteration until the start is reached or passed. The start and end points are included or excluded according to this range.</summary>
   /// <param name="step">Delegate to apply to the "current value" on each iteration</param>
   public RangeIterator<T> FromEnd(Func<T, T> step)
      => new(this, step, false);

   /// <summary>Returns an iterator which steps through the range, applying the specified step delegate on each iteration. The method determines whether to begin at the start or end of the range based on whether the step delegate appears to go "up" or "down". Tge step delegate is applied iterator begins at the start point; otherwise it begins at the end point.</summary>
   /// <param name="step">Delegate to apply to the "current value" on each iteration</param>
   public RangeIterator<T> Step(Func<T, T> step)
   {
      step.ThrowIfNull();

      var ascending = Comparer.Compare(Start, step(Start)) < 0;

      return ascending ? FromStart(step) : FromEnd(step);
   }
}