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

namespace Adapdev.Scheduling.Timer
{
	/// <summary>
	/// This is an empty filter that does not filter any of the events.
	/// </summary>
	public class Filter : IResultFilter
	{
		public static IResultFilter Empty = new Filter();
		private Filter() {}

		public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
		{
			if (List == null)
				return;
			List.Sort();
		}
	}

	/// <summary>
	/// This causes only the first event of the interval to be counted.
	/// </summary>
	public class FirstEventFilter : IResultFilter
	{
		public static IResultFilter Filter = new FirstEventFilter();
		private FirstEventFilter() {}

		public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
		{
			if (List == null)
				return;
			if (List.Count < 2)
				return;
			List.Sort();
			List.RemoveRange(1, List.Count-1);
		}
	}

	/// <summary>
	/// This causes only the last event of the interval to be counted.
	/// </summary>
	public class LastEventFilter : IResultFilter
	{
		public static IResultFilter Filter = new LastEventFilter();
		private LastEventFilter() {}

		public void FilterResultsInInterval(DateTime Start, DateTime End, ArrayList List)
		{
			if (List == null)
				return;
			if (List.Count < 2)
				return;
			List.Sort();
			List.RemoveRange(0, List.Count-1);
		}
	}

}
