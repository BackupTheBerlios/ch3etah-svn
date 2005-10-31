using System;
using System.Collections;
using System.ComponentModel;

namespace Ch3Etah.Design
{
	public class CollectionItemPropertyDescriptor: PropertyDescriptor
	{
		private object _item;
		public IList _owner;
 
		public CollectionItemPropertyDescriptor(string itemName, object item, IList owner, Attribute[] attrs) : base(itemName, attrs)
		{
			this._item = item;
			this._owner = owner;
		}

		public override bool CanResetValue(object component)
		{
			return false;
		}

		public override object GetValue(object component)
		{
			return _item;
		}

		public override void ResetValue(object component)
		{
		}

		protected override void FillAttributes(IList attributeList)
		{
			base.FillAttributes(attributeList);
			attributeList.Add(new TypeConverterAttribute(typeof (ExpandableObjectConverter)));
		}

		public override void SetValue(object component, object value)
		{
			if (value != null && !value.Equals("")) {
				if (_owner.Contains(_item)) {
					_owner[_owner.IndexOf(_item)] = value;
				}
				else {
					_owner.Add(value);
				}
			}
			else {
				if (_owner.Contains(_item)) {
					_owner.Remove(_item);
				}
			}
			_item = value;
		}

		public override bool ShouldSerializeValue(object component)
		{
			return false;
		}

		public override Type ComponentType
		{
			get { return typeof(ICollection); }
		}

		public override bool IsReadOnly
		{
			get { return false; }
		}

		public override Type PropertyType
		{
			get { return _item.GetType(); }
		}

		public object PropertyOwner {
			get { return _owner; }
		}
	}
}
