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
using System.Xml.Serialization;
using Ch3Etah.Metadata.OREntities;

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
			Namespace = nameSpace;
			Name = name;
		}
		
		public readonly string Namespace;
		public readonly string Name;
	}

	public class DataSourceEntityGroup
	{
		private ArrayList _entities = new ArrayList();

		internal DataSourceEntityGroup(string name)
		{
			Name = name;
		}
		
		public readonly string Name;
		public DataSourceEntity[] Entities
		{
			get
			{
				return (DataSourceEntity[])_entities.ToArray(typeof(DataSourceEntity));
			}
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
	}
}

