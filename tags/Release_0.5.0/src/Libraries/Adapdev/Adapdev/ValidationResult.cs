using System;
using System.Text;

namespace Adapdev
{
	/// <summary>
	/// Summary description for ValidationResult.
	/// </summary>
	public class ValidationResult
	{
		private bool _isValid = true;
		private StringBuilder _sb = new StringBuilder();

		public string Message
		{
			get{return this._sb.ToString();}
		}

		public bool IsValid
		{
			get{return this._isValid;}
			set{this._isValid = value;}
		}

		public void AddMessage(string message)
		{
			if(message.Length > 0)
			{
				this._sb.Append(message);
				this._sb.Append(Environment.NewLine);
			}
		}
	}
}
