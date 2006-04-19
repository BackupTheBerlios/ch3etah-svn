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
 *   Date: 19/11/2004
 */

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

using Ch3Etah.Core.CodeGen;
using Ch3Etah.Core.CodeGen.NVelocityEngine;
using Ch3Etah.Core.CodeGen.Xslt;
using Ch3Etah.Core.Config;
using Ch3Etah.Core.Exceptions;

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Description of TransformationEngineFactory.
	/// </summary>
	public class TransformationEngineFactory
	{
		private const string DEFAULT_ENGINE = "NVelocity";
		public static string DefaultEngineName
		{
			get { return DEFAULT_ENGINE; }
		}

		public static string[] GetConfiguredEngineNames()
		{
			ArrayList list = new ArrayList();
			foreach (TransformationEngine engine in Ch3EtahConfig.TransformationEngines)
			{
				list.Add(engine.Name);
			}
			return (string[])list.ToArray(typeof(string));
		}

		private TransformationEngineFactory()
		{
		}
		
		public static ITransformationEngine CreateEngine() 
		{
			return CreateEngine(DEFAULT_ENGINE);
		}
		
		public static ITransformationEngine CreateEngine(string name) 
		{
			ITransformationEngine engine = InternalCreateEngine(name);
			if (engine != null)
			{
				Debug.WriteLine("[TransformationEngineFactory()] Created code generation engine of type " + engine.GetType().ToString());
				return engine;
			}
			Trace.WriteLine(string.Format(
				"WARNING: [TransformationEngineFactory()] The template transformation engine named '{0}' could not be loaded. Default engine will be used instead.", name));
			return InternalCreateEngine(DEFAULT_ENGINE);
		}
		
		public static ITransformationEngine InternalCreateEngine(string name) 
		{
			if (Ch3EtahConfig.TransformationEngines.Contains(name))
			{
				Ch3Etah.Core.Config.TransformationEngine engInfo = 
					Ch3EtahConfig.TransformationEngines[name];
				Type engType = null;
			
				string oldDir = Directory.GetCurrentDirectory();
				Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
				try 
				{
					engType = Type.GetType(engInfo.EngineType);
					if (engType == null) 
						throw new UnknownTypeException("EngineType '" + engInfo.EngineType + "' for the TemplateEngine '" + name + "' could not be created. Please make sure the specified assembly is in the application directory.");
				}
				finally 
				{
					Directory.SetCurrentDirectory(oldDir);
				}
				ITransformationEngine eng = (ITransformationEngine)Activator.CreateInstance(engType);
				return eng;
			}
			else
			{
				return null;
			}
		}
	}
}
