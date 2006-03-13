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
using System.Collections;

namespace Ch3Etah.Core.ProjectLib
{
	//[XmlInclude(typeof(OleDataSource))]
	public interface IDataSource
	{
		Guid Guid { get; set; }
		string Name { get; set; }
		//Project Project { get; }

		//internal void SetProject(Project project);

		void TestConnection();
		bool IsConnectionValid();
		DataSourceEntityGroup[] ListEntities();
		void SyncProjectEntities();
		void SyncProjectEntities(DataSourceEntity[] entities);
		//void SyncProjectEntity(DataSourceEntity dsEntity, Entity entity);
	}
	
	public class DataSourceEntity
	{
		internal DataSourceEntity(string nameSpace, string name)
		{
			if (nameSpace != null) Namespace = nameSpace.Trim('.');
			if (name != null) Name = name.Trim('.');
		}
		
		
		public readonly string Namespace = string.Empty;
		public readonly string Name = string.Empty;
		
		public override string ToString()
		{
			if (Namespace != "")
				return Namespace + "." + Name;
			else
				return Name;
		}

	}

	public class DataSourceEntityGroup
	{
		private string _name;
		private ArrayList _entities = new ArrayList();
		private ArrayList _subGroups = new ArrayList();

		internal DataSourceEntityGroup(string name)
		{
			_name = name;
		}
		
		
		internal void AddEntity(string name)
		{
			_entities.Add(new DataSourceEntity("", name));
		}

		internal void AddEntity(string name, string nameSpace)
		{
			_entities.Add(new DataSourceEntity(nameSpace, name));
		}

		internal void AddEntity(DataSourceEntity entity)
		{
			_entities.Add(entity);
		}

		internal void AddSubGroup(string name)
		{
			_entities.Add(new DataSourceEntityGroup(name));
		}

		internal void AddSubGroup(DataSourceEntityGroup group)
		{
			_entities.Add(group);
		}
		
		
		public string Name
		{
			get { return _name; }
		}
		public DataSourceEntity[] Entities
		{
			get
			{
				return (DataSourceEntity[])_entities.ToArray(typeof(DataSourceEntity));
			}
		}

		public DataSourceEntityGroup[] SubGroups
		{
			get
			{
				return (DataSourceEntityGroup[])_entities.ToArray(typeof(DataSourceEntityGroup));
			}
		}

		
		public DataSourceEntity FindEntity(string name)
		{
			return FindEntity(name, "");
		}
		
		public DataSourceEntity FindEntity(string name, string nameSpace)
		{
			foreach (DataSourceEntity e in _entities)
			{
				if (e.Name == name 
					&& (e.Namespace == nameSpace || nameSpace == ""))
				{
					return e;
				}
			}
			return null;
		}

		public DataSourceEntityGroup FindSubGroup(string name)
		{
			foreach (DataSourceEntityGroup g in _subGroups)
			{
				if (g.Name == name)
				{
					return g;
				}
			}
			return null;
		}
	}
}

