using System;
using System.Collections;
using System.ComponentModel;

namespace Ch3Etah.Core.ComponentModel {
	
	public class ComponentCollection : CollectionBase, ICustomTypeDescriptor, IComponent {
		
		private ISite site;

		protected Attribute[] GetCustomAttributes() {
			return new Attribute[]{};
		}

		public override string ToString() {
			return string.Format("[{0} item(s)]", Count);
		}

		#region ICustomTypeDescriptor members

		public AttributeCollection GetAttributes() {
			return TypeDescriptor.GetAttributes(this, true);
		}

		public string GetClassName() {
			return GetType().Name;
		}

		public string GetComponentName() {
			return GetType().Name;
		}

		public TypeConverter GetConverter() {
			return TypeDescriptor.GetConverter(this, true);
		}

		public EventDescriptor GetDefaultEvent() {
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		public PropertyDescriptor GetDefaultProperty() {
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		public object GetEditor(Type editorBaseType) {
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		public EventDescriptorCollection GetEvents() {
			return TypeDescriptor.GetEvents(this, true);
		}

		public EventDescriptorCollection GetEvents(Attribute[] attributes) {
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		public PropertyDescriptorCollection GetProperties() {
			PropertyDescriptor[] retProps = CreatePropertyList();
			return new PropertyDescriptorCollection(retProps);
		}

		protected virtual PropertyDescriptor[] CreatePropertyList() {
			ArrayList props = new ArrayList();

			foreach (object item in this) {
				PropertyDescriptor name = TypeDescriptor.GetProperties(item).Find("Name", true);
				CollectionItemPropertyDescriptor pd = new CollectionItemPropertyDescriptor(name.GetValue(item).ToString(), item, this, GetCustomAttributes());
				props.Add(pd);
			}
			return (PropertyDescriptor[]) props.ToArray(typeof (CollectionItemPropertyDescriptor));
		}

		public PropertyDescriptorCollection GetProperties(Attribute[] attributes) {
			return GetProperties();
		}

		public object GetPropertyOwner(PropertyDescriptor pd) {
			if (pd is CollectionItemPropertyDescriptor) {
				return ((CollectionItemPropertyDescriptor)pd).PropertyOwner;
			}
			else {
				return this;
			}
		}

		#endregion

		#region IComponent Members

		public event EventHandler Disposed;

		public ISite Site {
			get {
				return site;
			}
			set {
				site = value;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose() {
			foreach (object item in this) {
				if (item is IDisposable) {
					((IDisposable)item).Dispose();
				}
				if (Disposed != null)
				{
					Disposed(this, EventArgs.Empty);
				}
			}
		}

		#endregion
	}

	public class CollectionItemPropertyDescriptor: PropertyDescriptor {
		private object _item;
		public IList _owner;
 
		public CollectionItemPropertyDescriptor(string itemName, object item, IList owner, Attribute[] attrs) : base(itemName, attrs) {
			this._item = item;
			this._owner = owner;
		}

		public override bool CanResetValue(object component) {
			return false;
		}

		public override object GetValue(object component) {
			return _item;
		}

		public override void ResetValue(object component) {
		}

		protected override void FillAttributes(IList attributeList) {
			base.FillAttributes(attributeList);
			attributeList.Add(new TypeConverterAttribute(typeof (ExpandableObjectConverter)));
		}

		public override void SetValue(object component, object value) {
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

		public override bool ShouldSerializeValue(object component) {
			return false;
		}

		public override Type ComponentType {
			get { return typeof(ICollection); }
		}

		public override bool IsReadOnly {
			get { return false; }
		}

		public override Type PropertyType {
			get { return _item.GetType(); }
		}

		public object PropertyOwner {
			get { return _owner; }
		}
	}

}