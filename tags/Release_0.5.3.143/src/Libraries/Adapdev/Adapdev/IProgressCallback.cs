//
// This code based on functions from http://www.codeproject.com/cs/miscctrl/progressdialog.asp 
// Original Author: About Matthew Adams
//

using System;

namespace Adapdev
{
	public enum ProgressMessageTypes { Info, Warning, Critical };
	public enum ProgressAutoCloseTypes {AutoClose, WaitOnEnd, WaitOnError}

	/// <summary>
	/// This defines an interface which can be implemented by UI elements
	/// which indicate the progress of a long operation.
	/// (See ProgressWindow for a typical implementation)
	/// </summary>
	public interface IProgressCallback
	{
		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		void Begin( int minimum, int maximum );

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback.
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <param name="autoClose">Specify to automatically close on End or Pause on End</param>
		void Begin(ProgressAutoCloseTypes autoClose, int minimum, int maximum );

		/// <summary>
		/// Call this method from the worker thread to initialize
		/// the progress callback, without setting the range
		/// </summary>
		void Begin();

		/// <summary>
		/// Call this method to set the AutoClose flag. True mean to autoclose on completion
		/// (default) and false means to wait when closing. 
		/// </summary>
		/// <param name="autoClose">Sets the flag</param>
		void SetAutoClose( ProgressAutoCloseTypes autoClose );

		/// <summary>
		/// Call this method from the worker thread to reset the range in the progress callback
		/// </summary>
		/// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
		/// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		void SetRange( int minimum, int maximum );

		/// <summary>
		/// Call this method from the worker thread to update the progress text.
		/// </summary>
		/// <param name="text1">The progress text to display</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		void SetText( String text1, String text2 );
		void SetText( String text );

		/// <summary>
		/// Call this method to add a message to the list box of messages
		/// </summary>
		/// <param name="message"></param>
		void AddMessage( string message );
		void AddMessage( ProgressMessageTypes type, string message );

		/// <summary>
		/// Call this method from the worker thread to increase the progress counter by a specified value.
		/// </summary>
		/// <param name="val">The amount by which to increment the progress indicator</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		void StepTo( int val );

		/// <summary>
		/// Call this method from the worker thread to step the progress meter to a particular value.
		/// </summary>
		/// <param name="val">The value to which to step the meter</param>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		void Increment( int val );

		/// <summary>
		/// If this property is true, then you should abort work
		/// </summary>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		bool IsAborting
		{
			get;
		}

		/// <summary>
		/// Call this method from the worker thread to finalize the progress meter
		/// </summary>
		/// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
		void End();
	}
}
