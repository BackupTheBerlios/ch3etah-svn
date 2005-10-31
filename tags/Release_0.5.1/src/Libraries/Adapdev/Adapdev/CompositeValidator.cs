using System;
using System.Collections;

namespace Adapdev
{
	/// <summary>
	/// Summary description for AbstractCompositeValidator.
	/// </summary>
	public class CompositeValidator : ICompositeValidator
	{
		private ArrayList _rules = new ArrayList();

		#region ICompositeValidator Members

		public void AddRule(IValidationRule rule)
		{
			this._rules.Add(rule);
		}

		#endregion

		#region IValidator Members

		public virtual ValidationResult Validate()
		{
			ValidationResult vr = new ValidationResult();
			ValidationResult temp = new ValidationResult();
			foreach(IValidationRule rule in this._rules)
			{
				temp = rule.Validate();
				if(!temp.IsValid)
				{
					vr.IsValid = false;
					vr.AddMessage(temp.Message);
				}
			}

			return vr;
		}

		#endregion
	}
}
