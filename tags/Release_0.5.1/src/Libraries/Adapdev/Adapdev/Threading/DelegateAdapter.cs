#region Original Copyright 2003 Richard Lowe

/**
Taken from the following blog: http://blogs.geekdojo.net/richard/archive/2003/12/19/492.aspx
Usage:

using System;
using System.Collections;
using System.Threading;
using Timing;
using DelegateAdapter;
public class Program
{
    // Create any method and a corresponding delegate:
    public delegate double WorkMethodHandler(double factor, string name);
    public static double WorkMethod(double factor, string name)
    {           
        Console.WriteLine(name);
        return 3.14159 * factor;
    }
    public static void Main()
    {
        // Create the DelegateAdapter with the appropriate method and arguments:
        DelegateAdapter adapter = new DelegateAdapter(new WorkMethodHandler(WorkMethod), 3.123456789, "Richard");
        // Automatically creates new ThreadStart and passes to the Thread constructor.
        // The adapter is implicitly convertible to a ThreadStart, which is why this works.
        Thread worker = new Thread(adapter);
        // change the arguments:
        adapter.Args = new object[] {9.14159d, "Roberto"};
        // run it:
        worker.Start();
        // wait to exit:
        worker.Join();
        // get result:
        Console.WriteLine(adapter.ReturnValue);          
    }
}
**/

#endregion
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

namespace DelegateAdapter
{
	using System;
	using System.Threading;

	/// <summary>
	/// Uses DynamicInvoke to allow any method to be easily mapped to the ThreadStart, WaitCallback or TimerCallback.
	/// </summary>
	public class DelegateAdapter
	{
		private object[] _args;

		public object[] Args
		{
			get { return _args; }
			set { _args = value; }
		}

		private Delegate _target;

		private object _returnValue;

		/// <summary>
		/// The return value, if any of the last execution of this DelegateAdapter's target method.
		/// </summary>
		public object ReturnValue
		{
			get { return _returnValue; }
		}

		/// <summary>
		/// Creates an instance of DelegateAdapter given any delegate.
		/// </summary>
		/// <param name="target">The delegate that will be invoked when one of the output delegates is invoked.</param>
		public DelegateAdapter(Delegate target) : this(target, null)
		{
		}

		/// <summary>
		/// Creates an instance of DelegateAdapter given any delegate and it's parameters to pass.
		/// </summary>
		/// <param name="target">The delegate that will be invoked when one of the output delegates is invoked.</param>
		/// <param name="args">The arguments that will be passed to the target delegate.</param>
		public DelegateAdapter(Delegate target, params object[] args)
		{
			_target = target;
			_args = args;
		}

		/// <summary>
		/// Dynamically invokes the target delegate with the provided arguments.
		/// </summary>
		public void Execute()
		{
			_returnValue = _target.DynamicInvoke(_args);
		}

		/// <summary>
		/// Dynamically invokes the target delegate with the state object provided by the caller.  *Note* ignores any Args passed to the DelegateAdapter.
		/// </summary>
		/// <param name="state"></param>
		public void Execute(object state)
		{
			if (state is object[])
				_returnValue = _target.DynamicInvoke(state as object[]);
			else
				_returnValue = _target.DynamicInvoke(new object[] {state});
		}

		/// <summary>
		/// Creates a new, unique ThreadStart delegate for use with the Thread class.
		/// </summary>
		/// <returns>The new ThreadStart delegate</returns>
		public ThreadStart CreateThreadStart()
		{
			return new ThreadStart(Execute);
		}

		/// <summary>
		/// Creates a new, unique WaitCallback delegate for use with the ThreadPool class.
		/// </summary>
		/// <returns>The new WaitCallback delegate</returns>
		public WaitCallback CreateWaitCallback()
		{
			return new WaitCallback(Execute);
		}

		/// <summary>
		/// Creates a new, unique TimerCallback delegate for use with the Timer class.
		/// </summary>
		/// <returns>The new TimerCallback delegate</returns>
		public TimerCallback CreateTimerCallback()
		{
			return new TimerCallback(Execute);
		}

		public static implicit operator ThreadStart(DelegateAdapter adapter)
		{
			return adapter.CreateThreadStart();
		}

		public static implicit operator WaitCallback(DelegateAdapter adapter)
		{
			return adapter.CreateWaitCallback();
		}

		public static implicit operator TimerCallback(DelegateAdapter adapter)
		{
			return adapter.CreateTimerCallback();
		}
	}
}