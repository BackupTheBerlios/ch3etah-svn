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

using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Design;
using System.Xml.Serialization;
using Ch3Etah.Core.Config;
using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Metadata.OREntities
{
	
	#region IndexNameChangedEvent
	public delegate void IndexNameChangedEventHandler(object sender, IndexNameChangedEventArgs e);

	public class IndexNameChangedEventArgs
	{
		public readonly Index Index;
		public readonly string OldName;
		public readonly string NewName;
		public IndexNameChangedEventArgs(Index index, string oldName, string newName)
		{
			this.Index = index;
			this.NewName = newName;
			this.OldName = oldName;
		}
	}
	#endregion IndexNameChangedEvent

	/// <summary>
	/// Represents an Index on an Entity.
	/// An Index defines a set of fields and rules used to filter
	/// records when retrieving or deleting objects from the database.
	/// </summary>
	public class Index : MetadataNodeBase
	{
		
		#region IndexNameChangedEvent
		public static event IndexNameChangedEventHandler NameChanged;

		private void DoNameChanged(string oldName, string newName)
		{
			if (NameChanged != null && oldName != "" && newName != "")
			{
				NameChanged(this, new IndexNameChangedEventArgs(
					this,
					oldName,
					newName));
			}
		}

		#endregion IndexNameChangedEvent

		public Index()
		{
			EntityField.NameChanged += new EntityFieldNameChangedEventHandler(EntityField_NameChanged);
		}
		
		#region Member Variables
		private string _dbName = string.Empty;
		private string _name = string.Empty;
		private bool _primaryKey = false;
		private bool _isForeignKey = false;
		private bool _unique = false;
		private bool _selectBy = true;
		private bool _deleteBy = false;
		private string _selectText = string.Empty;
		private string _deleteText = string.Empty;
		IndexFieldCollection _fields = new IndexFieldCollection();
		IndexOrderFieldCollection _orderByFields = new IndexOrderFieldCollection();
		#endregion Member Variables
		
		#region Entity
		private Entity _entity;
		private string _category;

		[XmlIgnore()]
		public Entity Entity {
			get { return _entity; }
		}
		internal void SetEntity(Entity entity) {
			Debug.Assert(entity != null, "Entity parameter should not be null.");
			_entity = entity;
		}
		#endregion Entity
		
		#region Properties
		
		[XmlAttribute("dbname"),
		Category("Database field")]
		public string DBName {
			get { return _dbName; }
			set { _dbName = value; }
		}
		
		[XmlAttribute("name")]
		[Category("(General)")]
		public override string Name {
			get { return _name; }
			set
			{
				DoNameChanged(_name, value);
				_name = value;
			}
		}
		
		[Category("(General)")]
		[XmlAttribute("category")]
		public string Category 
		{
			get { return _category; }
			set { _category = value; }
		}

		[XmlAttribute("primarykey"),
		Category("Database field")]
		public bool PrimaryKey {
			get { return _primaryKey; }
			set { _primaryKey = value; }
		}
		
		[XmlAttribute("foreignkey"),
		Category("Database field")]
		public bool ForeignKey
		{
			get { return _isForeignKey; }
			set { _isForeignKey = value; }
		}

		[XmlAttribute("unique"),
		Category("Index setup")]
		public bool Unique {
			get { return _unique; }
			set { _unique = value; }
		}
		
		[XmlAttribute("selectby"),
		Category("Index setup")]
		public bool SelectBy {
			get { return _selectBy; }
			set { _selectBy = value; }
		}
		
		[XmlAttribute("deleteby"),
		Category("Index setup")]
		public bool DeleteBy {
			get { return _deleteBy; }
			set { _deleteBy = value; }
		}

		[XmlElement("SelectText")]
		[Category("Query field")]
		[Editor("Ch3Etah.Design.Editors.SQLEditor,Ch3Etah.Design", typeof(UITypeEditor))]
		public string SelectText {
			get { return _selectText; }
			set { _selectText = value; }
		}

		[XmlElement("DeleteText")]
		[Category("Query field")]
		[Editor("Ch3Etah.Design.Editors.SQLEditor,Ch3Etah.Design", typeof(UITypeEditor))]
		public string DeleteText {
			get { return _deleteText; }
			set { _deleteText = value; }
		}
		#endregion Properties
		
		#region Collection Properties
		[Browsable(true), 
			MetadataNodeCollectionAttribute("Fields", "Field", typeof(IndexField)),
			Category("Index setup")]
		public IndexFieldCollection Fields {
			get {
				_fields.Index = this;
				return _fields;
			}
			set { _fields = value; }
		}
		
		[Browsable(true), 
			MetadataNodeCollectionAttribute("OrderBy", "Field", typeof(IndexOrderField)),
			Category("Index setup")]
		public IndexOrderFieldCollection OrderByFields {
			get {
				_orderByFields.Index = this;
				return _orderByFields;
			}
			set { _orderByFields = value; }
		}
		#endregion Collection Properties

//		public void RefreshDBInfo(OrmConfiguration config, DataRowView schema, EntityField entityField) {
//			IndexField indexField = null;
//			foreach (IndexField f in this.Fields) {
//				if (f.Name == entityField.Name) {
//					indexField = f;
//					break;
//				}
//			}
//			if (indexField == null) {
//				indexField = new IndexField();
//				indexField.Name = entityField.Name;
//				this.Fields.Add(indexField);
//			}
//			this.DBName = (string)schema["INDEX_NAME"];
//			this.PrimaryKey = (bool)schema["PRIMARY_KEY"];
//			//if (this.PrimaryKey) this.Name = "ID";
//			//else this.IsExcluded = true;
//			this.Unique = (bool)schema["UNIQUE"];
//			if (this.Name == string.Empty) {
//				if (this.PrimaryKey) {
//					this.IsExcluded = !config.AutoEnableMappedIndexes && !config.AutoEnablePrimaryIndex;
//					if (config.RenamePrimaryIndex && config.PrimaryIndexName != "") {
//						this.Name = config.PrimaryIndexName;
//					}
//					else {
//						this.Name = this.DBName;
//					}
//					this.DeleteBy = true;
//				}
//				else {
//					this.IsExcluded = !config.AutoEnableMappedIndexes;
//					this.Name = this.DBName;
//				}
//			}
//		}
//		
//		public void RemoveUnusedFields(DataView schema) {
//			for (int i=this.Fields.Count-1; i>=0; i--) {
//				IndexField field = this.Fields[i];
//				if (!SchemaContainsField(schema, field)) {
//					this.Fields.Remove(field);
//				}
//			}
//		}
//		
//		private bool SchemaContainsField(DataView schema, IndexField field) {
//			string dbColumn = string.Empty;
//			foreach (EntityField f in this.Entity.Fields) {
//				if (f.Name == field.Name) {
//					dbColumn = f.DBColumn;
//					break;
//				}
//			}
//			if (dbColumn == string.Empty) {
//				dbColumn = field.Name;
//			}
//			foreach (DataRowView row in schema) {
//				if ((string)row["TABLE_NAME"] == this.Entity.DBEntityName 
//					&& (string)row["INDEX_NAME"] == this.DBName 
//					&& (string)row["COLUMN_NAME"] == dbColumn) {
//					return true;
//				}
//			}
//			return false;
//		}
//
		public override string ToString() {
			return this.Name;
		}

		private void EntityField_NameChanged(object sender, EntityFieldNameChangedEventArgs e)
		{
			if (e.EntityField.Entity != this.Entity)
				return;

			foreach (IndexField field in this.Fields)
			{
				if (field.Name == e.OldName)
				{
					if (field.ParameterName == field.Name)
						field.ParameterName = e.NewName;

					field.Name = e.NewName;
				}
			}
		}
	}
}
