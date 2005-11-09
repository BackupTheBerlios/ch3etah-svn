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
using System.Diagnostics;
using Ch3Etah.Core.CodeGen;
using Ch3Etah.Core.CodeGen.NVelocityEngine;
using Ch3Etah.Core.CodeGen.Xslt;
using Ch3Etah.Core.Exceptions;

namespace Ch3Etah.Core.CodeGen
{
	/// <summary>
	/// Description of TransformationEngineFactory.
	/// </summary>
	public class TransformationEngineFactory
	{
		// TODO: REFACTOR - put in config file
		private const string DEFAULT_ENGINE = "NVelocity";
		
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
			if (engine != null) {
				Debug.WriteLine("TransformationEngineFactory: Created code generation engine of type " + engine.GetType().ToString());
				return engine;
			}
			Debug.WriteLine(string.Format(
				"TransformationEngineFactory: Code generation engine of type '{0}' could not be loaded. Returning default engine.", name));
			return InternalCreateEngine(DEFAULT_ENGINE);
		}
		
		public static ITransformationEngine InternalCreateEngine(string name) {
			// TODO: REFACTOR - put in config file
			switch (name.ToUpper()) {
				case "NVELOCITY":
					return new NVelocityTransformationEngine();
				case "XSLT":
					return new XsltTransformationEngine();
				case "CODESMITH":
					return null;//new CodeSmith.CodeSmithTransformationEngine();
				default:
					return null;
			}
		}
	}
}
