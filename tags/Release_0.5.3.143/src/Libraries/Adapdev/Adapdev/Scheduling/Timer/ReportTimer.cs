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

namespace Adapdev.Scheduling.Timer
{
	public class ReportEventArgs : EventArgs
	{
		public ReportEventArgs(DateTime Time, int reportNo) { EventTime = Time; ReportNo = reportNo; }
		public int ReportNo;
		public DateTime EventTime;
	}

	public delegate void ReportEventHandler(object sender, ReportEventArgs e);
	/// <summary>
	/// Summary description for ReportTimer.
	/// </summary>
	public class ReportTimer : ScheduleTimerBase
	{
		public void AddReportEvent(IScheduledItem Schedule, int reportNo)
		{
			if (Elapsed == null)
				throw new Exception("You must set elapsed before adding Events");
			AddJob(new TimerJob(Schedule, new DelegateMethodCall(Handler, Elapsed, reportNo)));
		}

		public void AddAsyncReportEvent(IScheduledItem Schedule, int reportNo)
		{
			if (Elapsed == null)
				throw new Exception("You must set elapsed before adding Events");
			TimerJob Event = new TimerJob(Schedule, new DelegateMethodCall(Handler, Elapsed, reportNo));
			Event.SyncronizedEvent = false;
			AddJob(Event);
		}

		public event ReportEventHandler Elapsed;

		delegate void ConvertHandler(ReportEventHandler Handler, int ReportNo, object sender, DateTime time);
		static ConvertHandler Handler = new ConvertHandler(Converter);
		static void Converter(ReportEventHandler Handler, int ReportNo, object sender, DateTime time)
		{
			if (Handler == null)
				throw new ArgumentNullException("Handler");
			if (sender == null)
				throw new ArgumentNullException("sender");
			Handler(sender, new ReportEventArgs(time, ReportNo));
		}
	}
}
