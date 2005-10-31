using System;

namespace Adapdev
{
	/// <summary>
	/// Summary description for IRuleValidatable.
	/// </summary>
	public interface ICompositeValidator : IValidator
	{
		void AddRule(IValidationRule rule);
	}
}
