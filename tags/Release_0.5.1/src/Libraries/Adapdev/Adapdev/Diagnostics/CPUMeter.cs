// Original Copyright (c) 2004, Ingo Rammer http://www.thinktecture.com/staff/ingo/weblog/archives/001403.html
/**
 * Usage:
 * 
 * static void Main(string[] args)
 * {
 *	CPUMeter mtr = new CPUMeter();
 *	// do some heavy stuff
 *	double result = 0;
 *	for (int i = 0;i<100000000; i++)
 *	{
 *		result = result+Math.Sin(i);
 *	}
 *	double usage = mtr.GetCpuUtilization();
 *	Console.WriteLine("Done. CPU Usage {0:#00.00} %", usage);
 *	Console.ReadLine();
 * }
 * */
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
	using System;
	using System.Diagnostics;

	public class CPUMeter : IDisposable
	{
		private CounterSample _startSample;
		private PerformanceCounter _cnt;

		/// Creates a per-process CPU meter instance tied to the current process.
		public CPUMeter()
		{
			String instancename = GetCurrentProcessInstanceName();
			_cnt = new PerformanceCounter("Process", "% Processor Time", instancename, true);
			ResetCounter();
		}

		/// Creates a per-process CPU meter instance tied to a specific process.
		public CPUMeter(int pid)
		{
			String instancename = GetProcessInstanceName(pid);
			_cnt = new PerformanceCounter("Process", "% Processor Time", instancename, true);
			ResetCounter();
		}

		/// Resets the internal counter. All subsequent calls to GetCpuUtilization() will 
		/// be relative to the point in time when you called ResetCounter(). This 
		/// method can be call as often as necessary to get a new baseline for 
		/// CPU utilization measurements.
		public void ResetCounter()
		{
			_startSample = _cnt.NextSample();
		}

		/// Returns this process's CPU utilization since the last call to ResetCounter().
		public double GetCpuUtilization()
		{
			CounterSample curr = _cnt.NextSample();

			double diffValue = curr.RawValue - _startSample.RawValue;
			double diffTimestamp = curr.TimeStamp100nSec - _startSample.TimeStamp100nSec;

			double usage = (diffValue/diffTimestamp)*100;
			return usage;
		}

		private static string GetCurrentProcessInstanceName()
		{
			Process proc = Process.GetCurrentProcess();
			int pid = proc.Id;
			return GetProcessInstanceName(pid);
		}

		private static string GetProcessInstanceName(int pid)
		{
			PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

			string[] instances = cat.GetInstanceNames();
			foreach (string instance in instances)
			{
				using (PerformanceCounter cnt = new PerformanceCounter("Process",
				                                                       "ID Process", instance, true))
				{
					int val = (int) cnt.RawValue;
					if (val == pid)
					{
						return instance;
					}
				}
			}
			throw new Exception("Could not find performance counter " +
				"instance name for current process. This is truly strange ...");
		}

		public void Dispose()
		{
			if (_cnt != null) _cnt.Dispose();
		}
	}
}