// Copyright 2004 Jacob Eggleston

//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at

//       http://www.apache.org/licenses/LICENSE-2.0

//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.

using System;
using System.Diagnostics;

namespace Ch3Etah.Gui
{

	public delegate void TraceEvent(object sender, TraceEventArgs e);

	#region TraceEventArgs
	public class TraceEventArgs {
		public readonly string TraceText;
		public TraceEventArgs (string traceText){
			TraceText = traceText;
		}
		public override string ToString() {
			return TraceText;
		}

	}
	#endregion

	/// <summary>
	/// <c>TraceListener</c> implementation which notifies 
	/// listeners by firing evens.
	/// </summary>
	public class EventTraceListener : TraceListener {
		
		public event TraceEvent TextWritten;
		
		public EventTraceListener() {}

		public override void Write(string message) {
			TextWritten(this, new TraceEventArgs(message));
		}

		public override void WriteLine(string message) {
			TextWritten(this, new TraceEventArgs(message + "\r\n"));
		}

		public override void Close() {
			Flush();
		}
		
		public override void Flush() {
			base.Flush();
		}

	}


}
