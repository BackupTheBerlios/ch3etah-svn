#region Copyright / License Information
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

namespace Adapdev.Diagnostics
{
	/// <summary>
	/// Summary description for PerfTimerFactory.
	/// </summary>
	public class PerfTimerFactory
	{
		public PerfTimerFactory()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static IPerfTimer GetPerfTimer(PerfTimerType type)
		{
			switch (type)
			{
				case PerfTimerType.MILLISECONDS:
					return new MillisecondsPerfTimer();
				case PerfTimerType.HIRESSECONDS:
					return new HiPerfTimer();
				case PerfTimerType.SECONDS:
					return new SecondsPerfTimer();
				case PerfTimerType.MINUTES:
					return new MinutesPerfTimer();
				case PerfTimerType.TICKS:
					return new TicksPerfTimer();
				default:
					return new MillisecondsPerfTimer();
			}
		}
	}

	public enum PerfTimerType
	{
		TICKS,
		MILLISECONDS,
		HIRESSECONDS,
		SECONDS,
		MINUTES
	}
}