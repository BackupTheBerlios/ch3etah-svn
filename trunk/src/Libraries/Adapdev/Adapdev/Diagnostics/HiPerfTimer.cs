// Original Copyright 2002 Daniel Strigl. http://www.codeproject.com/csharp/highperformancetimercshar.asp?target=timer
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

namespace Adapdev.Diagnostics
{
	using System.ComponentModel;
	using System.Runtime.InteropServices;
	using System.Threading;

	internal class HiPerfTimer : IPerfTimer
	{
		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceCounter(
			out long lpPerformanceCount);

		[DllImport("Kernel32.dll")]
		private static extern bool QueryPerformanceFrequency(
			out long lpFrequency);

		private long startTime, stopTime;
		private long freq;

		// Constructor
		public HiPerfTimer()
		{
			startTime = 0;
			stopTime = 0;

			if (QueryPerformanceFrequency(out freq) == false)
			{
				// high-performance counter not supported
				throw new Win32Exception();
			}
		}

		// Start the timer
		public void Start()
		{
			// lets do the waiting threads there work
			Thread.Sleep(0);

			QueryPerformanceCounter(out startTime);
		}

		// Stop the timer
		public void Stop()
		{
			QueryPerformanceCounter(out stopTime);
		}

		// Returns the duration of the timer (in seconds)
		public double Duration
		{
			get
			{
				double d = (stopTime - startTime);
				return d/freq;
			}
		}
	}
}