using System;
using System.Collections;
using System.Reflection;
using System.Text;

namespace Adapdev.Reflection
{
	/// <summary>
	/// Summary description for ClassAccessor.
	/// </summary>
	public class ClassAccessor
	{
		private Type _type = null;
		private Hashtable _properties = new Hashtable();
		private DateTime _created;

		public ClassAccessor(Type t)
		{
			this._type = t;
			this._created = DateTime.Now;
		}

		public ClassAccessor(object o) : this(o.GetType()){}

		public void AddProperty(string name)
		{
			PropertyAccessor accessor = new PropertyAccessor(this._type, name);
			this._properties[name] = accessor;
		}

		public object GetPropertyValue(object o, string propertyName)
		{
			this.CheckForAccessor(propertyName);
			PropertyAccessor accessor = this._properties[propertyName] as PropertyAccessor;
			return accessor.Get(o);
		}

		public PropertyAccessor GetPropertyAccessor(string propertyName)
		{
			return this._properties[propertyName] as PropertyAccessor;
		}

		public Hashtable GetPropertyAccessors()
		{
			return this._properties;
		}

		public Type PropertyType(string propertyName)
		{
			this.CheckForAccessor(propertyName);
			PropertyAccessor accessor = this._properties[propertyName] as PropertyAccessor;
			return accessor.PropertyType;
		}

		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		public void SetPropertyValue(object o, string propertyName, object val)
		{
			this.CheckForAccessor(propertyName);
			PropertyAccessor accessor = this._properties[propertyName] as PropertyAccessor;
			accessor.Set(o, val);
		}

		private void CheckForAccessor(string propertyName)
		{
			if(!this.IsPropertyDefined(propertyName))
				this.AddProperty(propertyName);
		}

		private bool IsPropertyDefined(string propertyName)
		{
			return this._properties.ContainsKey(propertyName);
		}

		public void LoadAllProperties()
		{
			PropertyInfo[] infoArray = this._type.GetProperties();
			foreach(PropertyInfo p in infoArray)
			{
				this.AddProperty(p.Name);
			}
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("[" + this._type.FullName + "] Properties loaded: " + Environment.NewLine);
			foreach(string key in this._properties.Keys)
			{
				sb.Append(key + Environment.NewLine);
			}
			return sb.ToString();
		}

		public int PropertyCount
		{
			get{return this._properties.Count;}
		}

	}
}
