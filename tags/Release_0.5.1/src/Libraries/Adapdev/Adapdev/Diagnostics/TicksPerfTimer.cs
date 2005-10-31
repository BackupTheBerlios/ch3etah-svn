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
	using System;

	/// <summary>
	/// Summary description for TicksPerfTimer.
	/// </summary>
	public class TicksPerfTimer : IPerfTimer
	{
		private int start;
		private int stop;

		public TicksPerfTimer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region IPerfTimer Members

		public void Start()
		{
			start = Environment.TickCount;
		}

		public void Stop()
		{
			stop = Environment.TickCount - start;
		}

		public double Duration
		{
			get { return stop; }
		}

		#endregion
	}
}