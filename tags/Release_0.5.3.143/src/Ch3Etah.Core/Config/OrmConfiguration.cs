/*   Copyright 2006 Jacob Eggleston
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
using System.Xml.Serialization;

namespace Ch3Etah.Core.Config
{
	public class OrmConfiguration : ICloneable
	{
		public OrmConfiguration() {}
		
		private bool _autoFillLinks = false;
		private bool _enableMappedLinks = true;
		private bool _excludeFKSourceFields = false;
		private bool _renameSurrogateKeys = false;
		private string _surrogateKeyName = "ID";
		private bool _renamePrimaryIndex = true;
		private string _primaryIndexName = "PK";
		private bool _autoFillIndexes = true;
		private bool _enableMappedIndexes = true;
		private bool _enablePrimaryIndex = true;
		private NameValuePairCollection _entityAttributes = new NameValuePairCollection();
		
		[Category("Links")]
		[Description("Allows you to indicate if you want associations to be automatically created in an entity for database relationships involving its mapped table.")]
		[XmlAttribute()]
		public bool AutoMapLinks
		{
			get { return _autoFillLinks; }
			set { _autoFillLinks = value; }
		}
		
		[Category("Links")]
		[Description("Allows you to specify whether associations that are automatically created will be enabled or disabled by default. If false, links may still be created for you, but you will need to manually include the ones you want for each entity.")]
		[XmlAttribute()]
		public bool AutoEnableMappedLinks
		{
			get { return _enableMappedLinks; }
			set { _enableMappedLinks = value; }
		}

		[Category("Links")]
		[Description("If true, fields on the source side of foreign key links will be excluded from the mapped entity. For example, if Person contains a FK link to PersonType, then PersonTypeID would not be mapped in person. This is usefull for O/R mappers like NHibernate which would automatically insert the correct value in PersonTypeID on the Person table based on the PersonType link.")]
		[XmlAttribute()]
		public bool ExcludeFKSourceFields
		{
			get { return _excludeFKSourceFields; }
			set { _excludeFKSourceFields = value; }
		}

		[Category("Fields")]
		[Description("Allows you to indicate if surrogate keys (primary key fields that are GUIDs or Identity/AutoNumber) should be renamed. For example you might want all surrogate key fields to be renamed 'ID' as a standard.")]
		[XmlAttribute()]
		public bool RenameSurrogateKeys
		{
			get { return _renameSurrogateKeys; }
			set { _renameSurrogateKeys = value; }
		}
		
		[Category("Fields")]
		[Description("If surrogate keys are automatically renamed, this property lets you define the name that will be given to them.")]
		[XmlAttribute()]
		public string SurrogateKeyName
		{
			get { return _surrogateKeyName; }
			set { _surrogateKeyName = value; }
		}
		
		[Category("Indexes")]
		[Description("Allows you to indicate if the primary key index on a table/entity should be renamed. For example you might want the primary key index on all your entities to be named 'PK' as a standard.")]
		[XmlAttribute()]
		public bool RenamePrimaryIndex
		{
			get { return _renamePrimaryIndex; }
			set { _renamePrimaryIndex = value; }
		}
		
		[Category("Indexes")]
		[Description("If primary key indexes are automatically renamed, this property lets you define the name that will be given to them.")]
		[XmlAttribute()]
		public string PrimaryIndexName
		{
			get { return _primaryIndexName; }
			set { _primaryIndexName = value; }
		}
		
		[Category("Indexes")]
		[Description("Allows you to indicate if you want indexes to be automatically created in entities for the database indexes on tables to which they are mapped.")]
		[XmlAttribute()]
		public bool AutoMapIndexes
		{
			get { return _autoFillIndexes; }
			set { _autoFillIndexes = value; }
		}
		
		[Category("Indexes")]
		[Description("Allows you to specify whether non-primary key indexes that are mapped automatically will be enabled or disabled by default. If false, indexes may still be created for you, but you will need to manually include the ones you want for each entity.")]
		[XmlAttribute()]
		public bool AutoEnableMappedIndexes
		{
			get { return _enableMappedIndexes; }
			set { _enableMappedIndexes = value; }
		}

		[Category("Indexes")]
		[Description("Allows you to specify whether primary key indexes that are mapped automatically will be enabled or disabled by default. If false, indexes may still be created for you, but you will need to manually include the ones you want for each entity.")]
		[XmlAttribute()]
		public bool AutoEnablePrimaryIndex
		{
			get { return _enablePrimaryIndex; }
			set { _enablePrimaryIndex = value; }
		}

		[Category("(Entity)")]
		[Description("Allows you to define custom XML attributes that will be added to entities when they are mapped from a data source.")]
		[TypeConverter(typeof (ExpandableObjectConverter))]
		[XmlArrayItem("Attribute")]
		public NameValuePairCollection CustomEntityAttributes 
		{
			get { return _entityAttributes; }
			set { _entityAttributes = value; }
		}

		
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		public OrmConfiguration Clone()
		{
			OrmConfiguration clone = (OrmConfiguration)this.MemberwiseClone();
			clone.CustomEntityAttributes = this.CustomEntityAttributes.Clone();
			return clone;
		}
		
	}
}
