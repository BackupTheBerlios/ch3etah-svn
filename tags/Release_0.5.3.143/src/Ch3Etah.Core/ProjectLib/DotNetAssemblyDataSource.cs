/*   Copyright 2004-2006 Jacob Eggleston
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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

using Ch3Etah.Core.Config;
using Ch3Etah.Metadata.OREntities;

namespace Ch3Etah.Core.ProjectLib
{
	//Provides a data source which pulls it's metadata from a .NET assembly
	public class DotNetAssemblyDataSource : DataSource
	{
		
		#region Fields
		private string _assemblyPath = string.Empty;
		private string _subNamespace = string.Empty;
		private OrmConfiguration _ormConfiguration = Ch3EtahConfig.OrmConfiguration.Clone();
		#endregion Fields

		#region Properties
		[Browsable(true)]
		public string AssemblyPath 
		{
			get { return _assemblyPath; }
			set { _assemblyPath = value; }
		}

		[Browsable(true)]
		public string SubNamespace 
		{
			get { return _subNamespace; }
			set { _subNamespace = value; }
		}

		[Browsable(true), XmlIgnore()]
		public string ConnectionString 
		{
			get 
			{
				if (SubNamespace != "")
					return string.Format("AssemblyPath={0};SubNamespace={1}", AssemblyPath, SubNamespace);
				else
					return "AssemblyPath=" + AssemblyPath;
			}
		}

		[TypeConverter(typeof (ExpandableObjectConverter))]
		public OrmConfiguration OrmConfiguration
		{
			get { return _ormConfiguration; }
			set { _ormConfiguration = value; }
		}
		#endregion Properties

		public override void TestConnection() 
		{
			LoadAssembly();
		}

		public override DataSourceEntityGroup[] ListEntities()
		{
			DataSourceEntityGroup classes = new DataSourceEntityGroup("Classes");
			foreach (Type t in GetClasses())
			{
				classes.AddEntity(t.Name, t.Namespace);
			}
			return new DataSourceEntityGroup[] {classes};
		}
		[Obsolete("", true)]
		public override void SyncProjectEntities()
		{
			throw new NotImplementedException();
		}
		
		public override void SyncProjectEntities(DataSourceEntity[] entities)
		{
			if (this.Project == null) throw new ApplicationException("Project is null.");
			ArrayList updatedEntities = new ArrayList();
			foreach (DataSourceEntity entity in entities) 
			{
				Type schema = GetEntitySchema(entity);
				Entity orEntity = GetMetadataEntity(entity, true);
				this.RefreshSchemaInfo(schema, orEntity);
				updatedEntities.Add(orEntity);
				if (this.OrmConfiguration.CustomEntityAttributes.Count > 0)
				{
					foreach (NameValuePair att in this.OrmConfiguration.CustomEntityAttributes)
					{
						orEntity.SetAttributeValue(att.Name, att.Value);
					}
				}
				orEntity.OwningMetadataFile.Save();
			}
		}
		
		private Type GetEntitySchema(DataSourceEntity entity)
		{
			Assembly asm = LoadAssembly();
			foreach (Type t in asm.GetTypes())
			{
				if (t.Name == entity.Name && t.Namespace == entity.Namespace)
				{
					return t;
				}
			}
			return null;
		}


		private Type[] GetClasses()
		{
			ArrayList classes = new ArrayList();
			Assembly asm = LoadAssembly();
			foreach (Type t in asm.GetTypes())
			{
				if (t.IsClass)//t.HasElementType //t.IsAbstract
				{
					classes.Add(t);
				}
			}
			return (Type[])classes.ToArray(typeof(Type));
		}

		private Assembly LoadAssembly()
		{
			if (this.Project == null) throw new ApplicationException("Project is null.");
			
			string dir = Path.GetDirectoryName(this.Project.FileName);
			Debug.WriteLine("Loading assembly info from: " + Path.Combine(dir, AssemblyPath));
			return Assembly.LoadFile(Path.Combine(dir, AssemblyPath));
		}

		#region RefreshSchemaInfo
		private void RefreshSchemaInfo(Type schema, Entity entity) 
		{
			Debug.WriteLine("Parsing data source information for the entity [" + entity.ToString() + "].");
			Debug.Indent();
			try
			{
				entity.DataSourceName = this.Name;
				entity.Namespace = schema.Namespace;
				entity.DBEntityName = schema.Name;
				if (entity.Name == string.Empty) 
				{
					entity.Name = schema.Name;
				}
				this.FillFields(schema, entity);
			}
			finally
			{
				Debug.Unindent();
			}
		}

		#endregion RefreshSchemaInfo

		#region FillFields
		private ArrayList _schemafields;
		private void FillFields(Type schema, Entity entity) 
		{
			_schemafields = new ArrayList();
			foreach (PropertyInfo prop in schema.GetProperties()) 
			{
				_schemafields.Add(prop.Name);
				EntityField field = GetEntityField(prop, entity);
				field.SetEntity(entity);
				this.RefreshFieldSchemaInfo(prop, field);
				if (!entity.Fields.Contains(field)) 
				{
					entity.Fields.Add(field);
				}
			}
			RemoveStaleSchemaFields(entity);
		}

		private EntityField GetEntityField(PropertyInfo prop, Entity entity) 
		{
			foreach (EntityField field in entity.Fields) 
			{
				if (field.Name == prop.Name) 
				{
					return field;
				}
			}
			return new EntityField();
		}

		private void RefreshFieldSchemaInfo(PropertyInfo prop, EntityField field) 
		{
			if (field.Name == string.Empty) 
			{
				field.Name = prop.Name.Replace(" ", "");
			}
			Debug.WriteLine(prop.Name + " " + prop.PropertyType.ToString());
			field.ReadOnly = ( prop.GetSetMethod() == null );
			field.DBColumn = prop.Name;
//			field.KeyField = column.IsPrimaryKey;
//			if (field.KeyField 
//				&& this.OrmConfiguration.RenameSurrogateKeys 
//				&& this.OrmConfiguration.SurrogateKeyName != "")
//			{
//				field.Name = this.OrmConfiguration.SurrogateKeyName;
//			}
//			field.DBIdentity = column.IsAutoIncrement;
//			field.DBReadOnly = column.IsReadOnly;
//			field.DBType = GetFieldDbType(column.DataTypeId, field);
			
			field.Type = prop.PropertyType.ToString().Replace("System.", "");
			if (field.Type.IndexOf(".") > 0)
			{
				field.Type = prop.PropertyType.ToString();
			}
			foreach (object att in prop.GetCustomAttributes(true))
			{
				if (att is CategoryAttribute)
				{
					field.Category = ((CategoryAttribute)att).Category;
				}
				if (att is DescriptionAttribute)
				{
					field.Description = ((DescriptionAttribute)att).Description;
				}
				if (att is BrowsableAttribute)
				{
					field.Browsable = ((BrowsableAttribute)att).Browsable;
				}
			}
//			field.DBSize = column.Length;
//			field.DBPrecision = 8;
		}

		private void RemoveStaleSchemaFields(Entity entity)
		{
			foreach (EntityField field in entity.Fields)
			{
				if (field.DBColumn != "" && !_schemafields.Contains(field.DBColumn))
				{
					field.IsExcluded = true;
				}
			}
		}
		#endregion FillFields

	}
}
