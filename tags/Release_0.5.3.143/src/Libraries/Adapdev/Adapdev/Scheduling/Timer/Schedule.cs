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

namespace Adapdev.Scheduling.Timer
{

	/// <summary>
	/// There have been quite a few requests to allow scheduling of multiple delegates and method parameter data
	/// from the same timer.  This class allows you to match the event with the time that it fired.  I want to keep
	/// the same simple implementation of the EventQueue and interval classes since they can be reused elsewhere.
	/// The timer should be responsible for matching this data up.
	/// </summary>
	public class EventInstance : IComparable
	{
		public EventInstance(DateTime time, IScheduledItem scheduleItem, object data)
		{
			Time = time;
			ScheduleItem = scheduleItem;
			Data = data;
		}
		public DateTime Time;
		public IScheduledItem ScheduleItem;
		public object Data;

		public int CompareTo(object obj)
		{
			if (obj is EventInstance)
				return Time.CompareTo(((EventInstance)obj).Time);
			if (obj is DateTime)
				return Time.CompareTo((DateTime)obj);
			return 0;
		}
	}
	/// <summary>
	/// IScheduledItem represents a scheduled event.  You can query it for the number of events that occur
	/// in a time interval and for the remaining interval before the next event.
	/// </summary>
	public interface IScheduledItem
	{
		/// <summary>
		/// Returns the times of the events that occur in the given time interval.  The interval is closed
		/// at the start and open at the end so that intervals can be stacked without overlapping.
		/// </summary>
		/// <param name="Begin">The beginning of the interval</param>
		/// <param name="End">The end of the interval</param>
		/// <returns>All events >= Begin and &lt; End </returns>
		void AddEventsInInterval(DateTime Begin, DateTime End, ArrayList List);

		/// <summary>
		/// Returns the next run time of the scheduled item.  Optionally excludes the starting time.
		/// </summary>
		/// <param name="time">The starting time of the interval</param>
		/// <param name="IncludeStartTime">if true then the starting time is included in the query false, it is excluded.</param>
		/// <returns>The next execution time either on or after the starting time.</returns>
		DateTime NextRunTime(DateTime time, bool IncludeStartTime);
	}

	/// <summary>
	/// The event queue is a collection of scheduled items that represents the union of all child scheduled items.
	/// This is useful for events that occur every 10 minutes or at multiple intervals not covered by the simple
	/// scheduled items.
	/// </summary>
	public class EventQueue : IScheduledItem
	{
		public EventQueue()
		{
			_List = new ArrayList();
		}
		/// <summary>
		/// Adds a ScheduledTime to the queue.
		/// </summary>
		/// <param name="time">The scheduled time to add</param>
		public void Add(IScheduledItem time)
		{
			_List.Add(time);
		}

		/// <summary>
		/// Clears the list of scheduled times.
		/// </summary>
		public void Clear()
		{
			_List.Clear();
		}

		/// <summary>
		/// Adds the running time for all events in the list.
		/// </summary>
		/// <param name="Begin"></param>
		/// <param name="End"></param>
		/// <param name="List"></param>
		public void AddEventsInInterval(DateTime Begin, DateTime End, ArrayList List)
		{
			foreach(IScheduledItem st in _List)
				st.AddEventsInInterval(Begin, End, List);
			List.Sort();
		}

		/// <summary>
		/// Returns the first time after the starting time for all events in the list.
		/// </summary>
		/// <param name="time"></param>
		/// <param name="AllowExact"></param>
		/// <returns></returns>
		public DateTime NextRunTime(DateTime time, bool AllowExact)
		{
			DateTime next = DateTime.MaxValue;
			//Get minimum datetime from the list.
			foreach(IScheduledItem st in _List)
			{
				DateTime Proposed = st.NextRunTime(time, AllowExact);
				next = (Proposed < next) ? Proposed : next;
			}
			return next;
		}
		private ArrayList _List;
	}	 
}
 
 
