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

using Ch3Etah.Core.Metadata;

namespace Ch3Etah.Metadata.OREntities
{
	/// <summary>
	/// Represents a Link, or association, from one Entity to Another.
	/// </summary>
	public class Link : MetadataNodeBase
	{
		
		public Link()
		{
			Entity.NameChanged += new EntityNameChangedEventHandler(Entity_NameChanged);
			EntityField.NameChanged += new EntityFieldNameChangedEventHandler(EntityField_NameChanged);
			Index.NameChanged += new IndexNameChangedEventHandler(Index_NameChanged);
			IndexField.NameChanged += new IndexFieldNameChangedEventHandler(IndexField_NameChanged);
		}
		
		#region Member Variables
		private string _name = "";
		private string _dbName = "";
		private string _targetEntityName = "";
		private string _targetIndexName = "";
		private bool _isProperty = true;
		private bool _isCollection = true;
		private bool _isConstrained = false;
		private bool _cascadeDelete = false;
		private bool _cascadeUpdate = false;
		private bool _hidden = false;
		private bool _readonly = false;
		private LinkFieldCollection _fields = new LinkFieldCollection();
		#endregion Member Variables
		
		#region Entity
		private Entity _entity;
		private string _category;
		private bool _browsable = false;

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
		[Category("(General)")]
		[XmlAttribute("name")]
		public override string Name {
			get {
				if (_name == "" && _targetEntityName != "") {
					return _targetEntityName;
				}
				else {
					return _name;
				}
			}
			set { _name = value; }
		}
		
		[Category("Database")]
		[XmlAttribute("dbname")]
		public string DBName 
		{
			get { return _dbName; }
			set { _dbName = value; }
		}
		
		[Category("(General)")]
		[XmlAttribute("category")]
		public string Category 
		{
			get { return _category; }
			set { _category = value; }
		}

		[XmlAttribute("targetentity")]
		[Category("Target setup")]
		[TypeConverter("Ch3Etah.Design.Converters.EntitiesNameConverter,Ch3Etah.Design")]
		public string TargetEntityName {
			get {
				if (_targetEntityName == "" && _name != "") {
					return _name;
				}
				else {
					return _targetEntityName;
				}
			}
			set 
			{ 
				_targetEntityName = value; 

				// Clear TargetIndex
				this.TargetIndexName = String.Empty;
			}
		}
		
		[XmlAttribute("targetindex")]
		[Category("Target setup")]
		[TypeConverter("Ch3Etah.Design.Converters.EntitiesIndexesNameConverter,Ch3Etah.Design")]
		public string TargetIndexName {
			get { return _targetIndexName; }
			set { _targetIndexName = value; }
		}
		
		[XmlAttribute("isproperty")]
		[Category("Link setup")]
		public bool IsProperty 
		{
			get { return _isProperty; }
			set { _isProperty = value; }
		}
		
		[XmlAttribute("iscollection")]
		[Category("Link setup")]
		public bool IsCollection {
			get { return _isCollection; }
			set {
				_isCollection = value;
//				if (_isCollection) {
//					_inverse = false;
//				}
			}
		}
		
		[XmlAttribute("isconstrained")]
		[Category("Link setup")]
		public bool IsConstrained 
		{
			get { return _isConstrained; }
			set { _isConstrained = value; }
		}
		
		[Category("Field")]
		[XmlAttribute("browsable")]
		public bool Browsable 
		{
			get { return _browsable; }
			set { _browsable  = value; }
		}

		[XmlAttribute("cascadedelete")]
		[Category("Link setup")]
		public bool CascadeDelete {
			get { return _cascadeDelete; }
			set { _cascadeDelete = value; }
		}
		
		[XmlAttribute("cascadeupdate"),
		Category("Link setup")]
		public bool CascadeUpdate {
			get { return _cascadeUpdate; }
			set { _cascadeUpdate = value; }
		}
		
		[XmlAttribute("hidden"),
		Category("Link setup")]
		public bool Hidden {
			get { return _hidden; }
			set { _hidden = value; }
		}

		[XmlAttribute("readonly"),
		Category("Link setup")]
		public bool ReadOnly {
			get { return _readonly; }
			set { _readonly = value; }
		}
		#endregion Properties
		
		#region Collection Properties
		[Browsable(true), 
			MetadataNodeCollectionAttribute("Fields", "Field", typeof(LinkField)),
			Category("Target setup")]
		public LinkFieldCollection Fields {
			get {
				_fields.Link = this;
				return _fields;
			}
			set { _fields = value; }
		}
		#endregion Collection Properties
		
		/// <summary>
		/// Returns whether or not the local (from) side of an association/link is
		/// unique. In other words, returns true if the link is on the "one" side 
		/// of a one-to-many or one-to-one association.
		/// </summary>
		public bool IsFromUniqueSide
		{
			get
			{
				bool isMatch = false;
				
				// check any fields marked as key fields for this entity
				foreach (EntityField field in this.Entity.Fields)
				{
					if (field.KeyField)
					{
						isMatch = this.ContainsSourceField(field.Name);
						if (!isMatch)
							break;
					}
				}
				if (isMatch) return true;
				
				// check primary keys and unique indexes
				foreach (Index index in this.Entity.Indexes)
				{
					if (index.PrimaryKey || index.Unique)
					{
						foreach (IndexField field in index.Fields)
						{
							isMatch = this.ContainsSourceField(field.Name);
							if (!isMatch)
								break;
						}
						if (isMatch)
							break;
					}
				}
				if (isMatch) return true;
				
				// no unique index or PK on this side of the link
				return false;
			}
		}
		
		
		private bool ContainsSourceField(string sourceFieldName)
		{
			foreach (LinkField field in this.Fields)
			{
				if (field.SourceFieldName == sourceFieldName)
					return true;
			}
			return false;
		}

		public override string ToString() {
			return this.Name;
		}

		private void Entity_NameChanged(object sender, EntityNameChangedEventArgs e)
		{
			if (this.TargetEntityName == e.OldName)
			{
				bool save = this.Entity != null 
					&& this.Entity.OwningMetadataFile != null 
					&& !this.Entity.OwningMetadataFile.IsDirty;
				this.TargetEntityName = e.NewName;
				if (save) this.Entity.OwningMetadataFile.Save();
			}
		}

		private void EntityField_NameChanged(object sender, EntityFieldNameChangedEventArgs e)
		{
			if (e.EntityField.Entity != this.Entity)
				return;

			foreach (LinkField field in this.Fields)
			{
				if (field.SourceFieldName == e.OldName)
				{
					field.SourceFieldName = e.NewName;
				}
			}
		}

		private void Index_NameChanged(object sender, IndexNameChangedEventArgs e)
		{
			if (this.TargetIndexName == e.OldName)
			{
				if (e.Index.Entity == null || e.Index.Entity.Name != this.TargetEntityName)
					return;
				
				bool save = this.Entity != null 
					&& this.Entity.OwningMetadataFile != null 
					&& !this.Entity.OwningMetadataFile.IsDirty;
				
				this.TargetIndexName = e.NewName;
				
				if (save) this.Entity.OwningMetadataFile.Save();
			}
		}

		private void IndexField_NameChanged(object sender, IndexFieldNameChangedEventArgs e)
		{
			if (e.IndexField.Index == null
				|| e.IndexField.Index.Entity == null
				|| e.IndexField.Index.Entity.Name != this.TargetEntityName
				|| e.IndexField.Index.Name != this.TargetIndexName)
				return;
			
			bool save = this.Entity != null 
				&& e.IndexField.Index.Entity != this.Entity
				&& this.Entity.OwningMetadataFile != null 
				&& !this.Entity.OwningMetadataFile.IsDirty;
			bool altered = false;

			foreach (LinkField field in this.Fields)
			{
				if (field.TargetFieldName == e.OldName)
				{
					altered = true;
					field.TargetFieldName = e.NewName;
				}
			}
			
			if (save && altered) this.Entity.OwningMetadataFile.Save();
		}
	}
}
