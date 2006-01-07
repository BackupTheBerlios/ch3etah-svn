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
 *
 *   File Created using SharpDevelop.
 *   User: Jacob Eggleston
 *   Date: 23/12/2004
 */


using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using Adapdev.Data.Schema;
using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Metadata.OREntities {
	
	#region EntityFieldNameChangedEvent
	public delegate void EntityFieldNameChangedEventHandler(object sender, EntityFieldNameChangedEventArgs e);

	public class EntityFieldNameChangedEventArgs
	{
		public readonly EntityField EntityField;
		public readonly string OldName;
		public readonly string NewName;
		public EntityFieldNameChangedEventArgs(EntityField field, string oldName, string newName)
		{
			this.EntityField = field;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion EntityFieldNameChangedEvent

	/// <summary>
	/// Represents a field in an Object-Relational Entity.
	/// </summary>
	public class EntityField : MetadataNodeBase 
	{
		
		#region EntityFieldNameChangedEvent
		public static event EntityFieldNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new EntityFieldNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}
		#endregion EntityFieldNameChangedEvent

		#region Member Variables

		private string _name = "";
		private bool _autoGenerated = false;
		private bool _readOnly = false;
		private bool _hidden = false;
		private string _defaultValue = "";
		private string _type = "";
		private string _vbType = "";
		private bool _serialize = true;
		private bool _allowNull = true;
		private string _nullValue = "";

		private string _dbColumn = "";
		private string _dbType = "";
		private bool _keyField = false;
		private int _dbSize = 0;
		private int _dbPrecision = 0;
		private bool _dbIdentity = false;
		private bool _dbReadOnly = false;

		private Entity _entity;
		private string _expression;
		private bool _browsable= true;
		private string _category;

		#endregion Member Variables

		#region Entity

		[XmlIgnore()]
		public Entity Entity {
			get { return _entity; }
		}

		internal void SetEntity(Entity entity) {
			Debug.Assert(entity != null, "Entity parameter should not be null.");
			_entity = entity;
		}

		#endregion Entity

		#region Field Properties

		[Category("(General)")]
		[XmlAttribute("name")]
		public override string Name {
			get {
				if (_name == "" && _dbColumn != "") return _dbColumn;
				else return _name;
			}
			set
			{
				DoNameChanged(_name, value);
				_name = value;
			}
		}

		[Category("Field")]
		[XmlAttribute("autogenerated")]
		public bool AutoGenerated {
			get { return _autoGenerated; }
			set { _autoGenerated = value; }
		}

		[Category("Field")]
		[XmlAttribute("defaultvalue")]
		public string DefaultValue {
			get { return _defaultValue; }
			set { _defaultValue = value; }
		}

		[Category("Field")]
		[XmlAttribute("readonly")]
		public bool ReadOnly {
			get { return _readOnly; }
			set { _readOnly = value; }
		}

		[Category("Field")]
		[XmlAttribute("browsable")]
		public bool Browsable {
			get { return _browsable; }
			set { _browsable  = value; }
		}

		[Category("Field")]
		[XmlAttribute("hidden")]
		public bool Hidden {
			get { return _hidden; }
			set { _hidden = value; }
		}

		[Category("Field")]
		[XmlAttribute("exclude")]
		public bool Exclude {
			get { return this.IsExcluded; }
			set { this.IsExcluded = value; }
		}

		[Category("Field")]
		[XmlAttribute("expression")]
		public string Expression {
			get { return _expression; }
			set { _expression = value; }
		}

		[Category("(General)")]
		[XmlAttribute("category")]
		public string Category {
			get { return _category; }
			set { _category = value; }
		}

		[Category("Field")]
		[XmlAttribute("type")]
		public string Type {
			get { return _type; }
			set { _type = value; }
		}

		[Category("Field")]
		[XmlAttribute("vbtype")]
		public string VBType {
			get { return _vbType; }
			set { _vbType = value; }
		}

		[Category("Field")]
		[XmlAttribute("serialize")]
		public bool Serialize {
			get { return _serialize; }
			set { _serialize = value; }
		}

		[Category("Field")]
		[XmlAttribute("allownull")]
		public bool AllowNull {
			get { return _allowNull; }
			set { _allowNull = value; }
		}

		[Category("Field")]
		[XmlAttribute("nullvalue")]
		public string NullValue {
			get { return _nullValue; }
			set { _nullValue = value; }
		}

		#endregion Field Properties

		#region Database Field Properties

		[Category("Database Field")]
		[XmlAttribute("dbcolumn")]
		public string DBColumn {
			get { return _dbColumn; }
			set { _dbColumn = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("keyfield")]
		public bool KeyField {
			get { return _keyField; }
			set { _keyField = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("dbidentity")]
		public bool DBIdentity {
			get { return _dbIdentity; }
			set { _dbIdentity = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("dbsize")]
		public int DBSize {
			get { return _dbSize; }
			set { _dbSize = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("dbprecision")]
		public int DBPrecision {
			get { return _dbPrecision; }
			set { _dbPrecision = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("dbtype")]
		public string DBType {
			get { return _dbType; }
			set { _dbType = value; }
		}

		[Category("Database Field")]
		[XmlAttribute("dbreadonly")]
		public bool DBReadOnly {
			get { return _dbReadOnly; }
			set { _dbReadOnly = value; }
		}

		#endregion Database Field Properties

		#region RefreshDBInfo

		public void RefreshDBInfo(ColumnSchema column) {
			if (Name == string.Empty) {
				Name = column.Name.Replace(" ", "");
				ReadOnly = column.IsReadOnly;
				AllowNull = column.AllowNulls;
			}
			Debug.WriteLine(
				column.Name + " " + column.DataTypeId + " " + column.DataType + " " + column.NetType + " " + column.Length + " " +
				column.DefaultValue + " " + column.DefaultTestValue);
			DBColumn = column.Name;
			KeyField = column.IsPrimaryKey;
			DBIdentity = column.IsAutoIncrement;
			DBReadOnly = column.IsReadOnly;
			DBType = GetDbType(column.DataTypeId);
			Type = column.NetType.Replace("System.", "");
			//this.VBType
			DBSize = column.Length;
			DBPrecision = 8;
		}

		// HACK
		private string GetDbType(int typeID) {
			switch (typeID) {
				case 2:
					return "smallint";
				case 3:
					return "int";
				case 4:
					return "real";
				case 5:
					return "float";
				case 6:
					return "money"; //smallmoney
				case 11:
					return "bit";
				case 17:
					return "tinyint";
				case 20:
					return "bigint";
				case 72:
					return "uniqueidentifier";
				case 128:
					return "binary";
				case 129:
					return "char";
				case 130:
					return "nchar";
				case 131:
					return "decimal"; //numeric
				case 135:
					return "datetime"; //smalldatetime
				case 200:
					return "varchar";
				case 201:
					return "text";
				case 202:
					return "nvarchar";
				case 203:
					return "ntext";
				case 204:
					return "varbinary";
				case 205:
					return "image";
				default:
					throw new InvalidOperationException("Type ID '" + typeID.ToString() + "' is not a recognized type code. (" + this.GetFullyQualifiedName() + ")");
			}
		}
		
		private string GetFullyQualifiedName() {
			string nm = "";
			if (_entity != null) {
				nm = "Entity=" + _entity.ToString() + ":";
			}
			nm += "Field=" + this.ToString();
			return nm;
		}

		#endregion RefreshDBInfo

		public override string ToString() {
			return Name;
		}
	}

}