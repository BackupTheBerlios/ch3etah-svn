using System;

namespace Adapdev
{
	/// <summary>
	/// Summary description for IRule.
	/// </summary>
	public interface IValidationRule
	{
		ValidationResult Validate();
	}
}
