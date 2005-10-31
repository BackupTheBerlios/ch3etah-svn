// Original Copyright (c) 2004 Dennis Austin. http://www.codeproject.com/csharp/tsnewlib.asp
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
using System.Runtime.InteropServices;

namespace Adapdev.Scheduling.Task
{
	/// <summary>
	/// Deprecated.  For V1 compatibility only. 
	/// </summary>
	/// <remarks>
	/// <p>Scheduler is just a wrapper around the TaskList class.</p>
	/// <p><i>Provided for compatibility with version one of the library.  Use of Scheduler
	/// and TaskList will normally result in COM memory leaks.</i></p>
	/// </remarks>
	public class Scheduler
	{
		/// <summary>
		/// Internal field which holds TaskList instance
		/// </summary>
		private readonly TaskList tasks = null;

		/// <summary>
		/// Creates instance of task scheduler on local machine
		/// </summary>
		public Scheduler()
		{
			tasks = new TaskList();
		}

		/// <summary>
		/// Creates instance of task scheduler on remote machine
		/// </summary>
		/// <param name="computer">Name of remote machine</param>
		public Scheduler(string computer)
		{
			tasks = new TaskList();
			TargetComputer = computer;
		}

		/// <summary>
		/// Gets/sets name of target computer. Null or emptry string specifies local computer.
		/// </summary>
		public string TargetComputer
		{
			get
			{
				return tasks.TargetComputer;
			}
			set
			{
				tasks.TargetComputer = value;
			}
		}

		/// <summary>
		/// Gets collection of system tasks
		/// </summary>
		public TaskList Tasks
		{
			get
			{
				return tasks;
			}
		}

	}
}
