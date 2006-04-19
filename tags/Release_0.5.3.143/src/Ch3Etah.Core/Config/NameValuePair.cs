/*   Copyright 2004 Jacob Eggleston
 *
 *   Licensed under the Apache License, Version 2.0 (the "License");
 *   you may not use this file except in compliance with the License.
 *   You may obtain a copy of the License at
 *
 *       http://www.apache.org/licenses/LICENSE-2.0
 *
 *   Unless required by applicable law or agreed to in writing, software
 *   distributed under the License is distributed on an "AS IS" BASIS,
 *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *   See the License for the specific language governing permissions and
 *   limitations under the License.
 *
 *   ========================================================================
 */

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;


namespace Ch3Etah.Core.Config
{

	internal delegate void RemoveNameValuePairEventHandler(object sender, RemoveNameValuePairEventArgs e);

	#region RemoveNameValuePairEventArgs
	internal class RemoveNameValuePairEventArgs 
	{
		public readonly NameValuePair NameValuePair;
		public RemoveNameValuePairEventArgs (NameValuePair pair)
		{
			NameValuePair = pair;
		}
	}
	#endregion RemoveNameValuePairEventArgs

	[TypeConverter(typeof(NameValuePairTypeConverter))]
	public class NameValuePair : IEditableObject, IDataErrorInfo
	{
		internal event RemoveNameValuePairEventHandler RemoveMe;

		#region Member variables

		bool _isNew = false;
		bool _isEditing = false;
		string _name = "";
		string _value = "";
		string _oldName = "";
		string _oldValue = "";

		#endregion

		#region Constructors

		public NameValuePair() {}


		public NameValuePair(string name, string value) 
		{
			_name = name;
			_value = value;
		}

		#endregion

		#region Properties

		[XmlAttribute]
		public string Name 
		{
			get 
			{
				return _name;
			}
			set 
			{
				_name = value;
			}
		}
		
		[XmlAttribute]
		public string Value 
		{
			get 
			{
				return _value;
			}
			set 
			{
				_value = value;
			}
		}

		#endregion
		
		#region IEditableObject implementation
		internal bool IsNew 
		{
			get 
			{
				return _isNew;
			}
			set 
			{
				_isNew = value;
			}
		}
		
		void IEditableObject.BeginEdit() 
		{
			if ( !_isEditing ) 
			{
				_isEditing = true;
				_oldName = _name;
				_oldValue = _value;
			}
		}

		void IEditableObject.CancelEdit() 
		{
			_isEditing = false;
			_name = _oldName;
			_value = _oldValue;
			if (_isNew) 
			{
				_isNew = false;
				RemoveMe(this, new RemoveNameValuePairEventArgs(this));
			}
		}

		void IEditableObject.EndEdit() 
		{
			_isEditing = false;
			_isNew = false;
		}
		#endregion IEditableObject implementation
		
		#region IDataErrorInfo
		string IDataErrorInfo.Error 
		{
			get 
			{
				if (Name == "") 
				{
					return "Name cannot be blank.";
				}
				return "";
			}
		}
		
		string IDataErrorInfo.this[string itemName] 
		{
			get 
			{
				if (itemName == "Name" && Name == "") 
				{
					//return "Name cannot be blank.";
				}
				return "";
			}
		}
		#endregion IDataErrorInfo

		public override string ToString()
		{
			return _name + "=\"" + _value + "\"";
		}
	}

	#region TypeConverter

	public class NameValuePairTypeConverter: StringConverter 
	{

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) 
		{
			if (sourceType == typeof(string)) 
			{
				return true;
			}
			else 
			{
				return base.CanConvertFrom(context, sourceType);
			}
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) 
		{
			object instance = context.PropertyDescriptor.GetValue(context.Instance);
			if (value is string && instance is NameValuePair && !value.Equals("")) 
			{
				return new NameValuePair(((NameValuePair) instance).Name, value as string);
			}
			return base.ConvertFrom(context, culture, value);
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
		{
			if (destinationType == typeof (string)) 
			{
				return true;
			}
			else 
			{
				return base.CanConvertTo(context, destinationType);
			}
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
		{
			if (destinationType == typeof(string) && value is NameValuePair) 
			{
				return ((NameValuePair) value).Value;
			}
			else 
			{
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
	}

	#endregion
}
