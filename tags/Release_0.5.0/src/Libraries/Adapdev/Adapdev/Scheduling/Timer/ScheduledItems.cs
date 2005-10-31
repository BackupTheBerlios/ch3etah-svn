// Original Copyright (c) 2004 Andy Brummer. All Rights Reserved. - http://www.codeproject.com/dotnet/ABTransClockArticle.asp
#region Modified Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;

namespace Adapdev.Scheduling.Timer
{
	public enum EventTimeBase
	{
		BySecond = 1,
		ByMinute = 2,
		Hourly = 3,
		Daily = 4,
		Weekly = 5,
		Monthly = 6,
	}

	/// <summary>
	/// This class represents a simple schedule.  It can represent a repeating event that occurs anywhere from every
	/// second to once a month.  It consists of an enumeration to mark the interval and an offset from that interval.
	/// For example new ScheduledTime(Hourly, new TimeSpan(0, 15, 0)) would represent an event that fired 15 minutes
	/// after the hour every hour.
	/// </summary>
	[Serializable]
	public class ScheduledTime : IScheduledItem
	{
		public ScheduledTime(EventTimeBase Base, TimeSpan Offset)
		{
			_Base = Base;
			_Offset = Offset;
		}

		/// <summary>
		/// intializes a simple scheduled time element from a pair of strings.  
		/// Here are the supported formats
		/// 
		/// BySecond - single integer representing the offset in ms
		/// ByMinute - A comma seperate list of integers representing the number of seconds and ms
		/// Hourly - A comma seperated list of integers representing the number of minutes, seconds and ms
		/// Daily - A time in hh:mm:ss AM/PM format
		/// Weekly - n, time where n represents an integer and time is a time in the Daily format
		/// Monthly - the same format as weekly.
		/// 
		/// </summary>
		/// <param name="StrBase">A string representing the base enumeration for the scheduled time</param>
		/// <param name="StrOffset">A string representing the offset for the time.</param>
		public ScheduledTime(string StrBase, string StrOffset)
		{
			//TODO:Create an IScheduled time factory method.
			_Base = (EventTimeBase)Enum.Parse(typeof(EventTimeBase), StrBase, true);
			Init(StrOffset);
		}

		public int ArrayAccess(string[] Arr, int i)
		{
			if (i >= Arr.Length)
				return 0;
			return int.Parse(Arr[i]);
		}

		public void AddEventsInInterval(DateTime Begin, DateTime End, ArrayList List)
		{
			DateTime Next = NextRunTime(Begin, true);
			while (Next < End)
			{
				List.Add(Next);
				Next = IncInterval(Next);
			}
		}

		public DateTime NextRunTime(DateTime time, bool AllowExact)
		{
			DateTime NextRun = LastSyncForTime(time) + _Offset;
			if (NextRun == time && AllowExact)
				return time;
			if (NextRun > time)
				return NextRun;
			return IncInterval(NextRun);
		}


		public DateTime LastSyncForTime(DateTime time)
		{
			switch (_Base)
			{
				case EventTimeBase.BySecond:
					return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, time.Second);
				case EventTimeBase.ByMinute:
					return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0);
				case EventTimeBase.Hourly:
					return new DateTime(time.Year, time.Month, time.Day, time.Hour, 0, 0);
				case EventTimeBase.Daily:
					return new DateTime(time.Year, time.Month, time.Day);
				case EventTimeBase.Weekly:
					return (new DateTime(time.Year, time.Month, time.Day)).AddDays(-(int)time.DayOfWeek);
				case EventTimeBase.Monthly:
					return new DateTime(time.Year, time.Month, 1);
			}
			throw new Exception("Invalid base specified for timer.");
		}
	 
		public DateTime IncInterval(DateTime Last)
		{
			switch (_Base)
			{
				case EventTimeBase.BySecond:
					return Last.AddSeconds(1);
				case EventTimeBase.ByMinute:
					return Last.AddMinutes(1);
				case EventTimeBase.Hourly:
					return Last.AddHours(1);
				case EventTimeBase.Daily:
					return Last.AddDays(1);
				case EventTimeBase.Weekly:
					return Last.AddDays(7);
				case EventTimeBase.Monthly:
					return Last.AddMonths(1);
			}
			throw new Exception("Invalid base specified for timer.");
		}

		private void Init(string StrOffset)
		{
			switch (_Base)
			{
				case EventTimeBase.BySecond:
					_Offset = new TimeSpan(0, 0, 0, 0, int.Parse(StrOffset));
					break;
				case EventTimeBase.ByMinute:
					string[] ArrMinute = StrOffset.Split(',');
					_Offset = new TimeSpan(0, 0, 0, ArrayAccess(ArrMinute, 0), ArrayAccess(ArrMinute, 1));
					break;
				case EventTimeBase.Hourly:
					string[] ArrHour = StrOffset.Split(',');
					_Offset = new TimeSpan(0, 0, ArrayAccess(ArrHour, 0), ArrayAccess(ArrHour, 1), ArrayAccess(ArrHour, 2));
					break;
				case EventTimeBase.Daily:
					DateTime Daytime = DateTime.Parse(StrOffset);
					_Offset = new TimeSpan(0, Daytime.Hour, Daytime.Minute, Daytime.Second, Daytime.Millisecond);
					break;
				case EventTimeBase.Weekly:
					string[] ArrWeek = StrOffset.Split(',');
					if (ArrWeek.Length != 2)
						throw new Exception("Weekly offset must be in the format n, time where n is the day of the week starting with 0 for sunday");
					DateTime WeekTime = DateTime.Parse(ArrWeek[1]);
					_Offset = new TimeSpan(int.Parse(ArrWeek[0]), WeekTime.Hour, WeekTime.Minute, WeekTime.Second, WeekTime.Millisecond);
					break;
				case EventTimeBase.Monthly:
					string[] ArrMonth = StrOffset.Split(',');
					if (ArrMonth.Length != 2)
						throw new Exception("Weekly offset must be in the format n, time where n is the day of the week starting with 0 for sunday");
					DateTime MonthTime = DateTime.Parse(ArrMonth[1]);
					_Offset = new TimeSpan(int.Parse(ArrMonth[0]), MonthTime.Hour, MonthTime.Minute, MonthTime.Second, MonthTime.Millisecond);
					break;
				default:
					throw new Exception("Invalid base specified for timer.");
			}
		}
	 
		private EventTimeBase _Base;
		private TimeSpan _Offset;
	}

	/// <summary>
	/// The simple interval represents the simple scheduling that .net supports natively.  It consists of a start
	/// absolute time and an interval that is counted off from the start time.
	/// </summary>
	[Serializable]
	public class SimpleInterval : IScheduledItem
	{
		public SimpleInterval(DateTime StartTime, TimeSpan Interval)
		{
			_Interval = Interval;
			_StartTime = StartTime;
			_EndTime = DateTime.MaxValue;
		}
		public SimpleInterval(DateTime StartTime, TimeSpan Interval, int count)
		{
			_Interval = Interval;
			_StartTime = StartTime;
			_EndTime = StartTime + TimeSpan.FromTicks(Interval.Ticks*count);
		}
		public SimpleInterval(DateTime StartTime, TimeSpan Interval, DateTime EndTime)
		{
			_Interval = Interval;
			_StartTime = StartTime;
			_EndTime = EndTime;
		}
		public void AddEventsInInterval(DateTime Begin, DateTime End, ArrayList List)
		{
			if (End <= _StartTime)
				return;
			DateTime Next = NextRunTime(Begin, true);
			while (Next < End)
			{
				List.Add(Next);
				Next = NextRunTime(Next, false);
			}
		}

		public DateTime NextRunTime(DateTime time, bool AllowExact)
		{
			DateTime returnTime = NextRunTimeInt(time, AllowExact);
			Debug.WriteLine(time);
			Debug.WriteLine(returnTime);
			Debug.WriteLine(_EndTime);
			return (returnTime >= _EndTime) ? DateTime.MaxValue : returnTime;
		}

		private DateTime NextRunTimeInt(DateTime time, bool AllowExact)
		{
			TimeSpan Span = time-_StartTime;
			if (Span < TimeSpan.Zero)
				return _StartTime;
			if (ExactMatch(time))
				return AllowExact ? time : time + _Interval;
			uint msRemaining = (uint)(_Interval.TotalMilliseconds - ((uint)Span.TotalMilliseconds % (uint)_Interval.TotalMilliseconds));
			return time.AddMilliseconds(msRemaining);
		}

		private bool ExactMatch(DateTime time)
		{
			TimeSpan Span = time-_StartTime;
			if (Span < TimeSpan.Zero)
				return false;
			return (Span.TotalMilliseconds % _Interval.TotalMilliseconds) == 0;
		}

		private TimeSpan _Interval;
		private DateTime _StartTime;
		private DateTime _EndTime;
	}

	/// <summary>
	/// This class will be used to implement a filter that enables a window of activity.  For cases where you want to 
	/// run every 15 minutes between 6:00 AM and 5:00 PM.  Or just on weekdays or weekends.
	/// </summary>
	public class BlockWrapper : IScheduledItem
	{
		public BlockWrapper(IScheduledItem item, string StrBase, string BeginOffset, string EndOffset)
		{
			_Item = item;
			_Begin = new ScheduledTime(StrBase, BeginOffset);
			_End = new ScheduledTime(StrBase, EndOffset);
		}
		public void AddEventsInInterval(DateTime Begin, DateTime End, ArrayList List)
		{
			DateTime Next = NextRunTime(Begin, true);
			while (Next < End)
			{
				List.Add(Next);
				Next = NextRunTime(Next, false);
			}
		}

		public DateTime NextRunTime(DateTime time, bool AllowExact)
		{
			return NextRunTime(time, 100, AllowExact);
		}

		DateTime NextRunTime(DateTime time, int count, bool AllowExact)
		{
			if (count == 0)
				throw new Exception("Invalid block wrapper combination.");

			DateTime
				temp = _Item.NextRunTime(time, AllowExact),
				begin = _Begin.NextRunTime(time, true),
				end = _End.NextRunTime(time, true);
			System.Diagnostics.Debug.WriteLine(string.Format("{0} {1} {2} {3}", time, begin, end, temp));
			bool A = temp > end, B = temp < begin, C = end < begin;
			System.Diagnostics.Debug.WriteLine(string.Format("{0} {1} {2}", A, B, C));
			if (C)
			{
				if (A && B)
					return NextRunTime(begin, --count, false);
				else
					return temp;
			} 
			else
			{
				if (!A && !B)
					return temp;
				if (!A)
					return NextRunTime(begin, --count, false);
				else
					return NextRunTime(end, --count, false);
			}
		}
		private IScheduledItem _Item;
		private ScheduledTime _Begin, _End;
	}
}