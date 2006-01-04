using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Xml;

using CodeSmith.Engine;

using Ch3Etah.Core.CodeGen;
using Ch3Etah.Core.CodeGen.PackageLib;

namespace Ch3Etah.CodeSmithSupport.Engine
{
	/// <summary>
	/// Implements an <see cref="ITransformationEngine">ITransformationEngine</see> 
	/// which executes CodeSmith templates.
	/// </summary>
	public class CodeSmithTransformationEngine : TransformationEngineBase
	{
		public CodeSmithTransformationEngine()
		{
		}
		
		public override void Transform(XmlNode input, TextWriter output) 
		{
			CodeTemplateCompiler compiler = new CodeTemplateCompiler(this.Template.GetFullPath());
			compiler.Compile();
			
			if (compiler.Errors.Count == 0)
			{
				CodeTemplate template = compiler.CreateInstance();
				
				template.SetProperty("CurrentMetadataFile", base.Context.CurrentMetadataFile);
				template.SetProperty("SelectedMetadataFiles", base.Context.SelectedMetadataFiles);
				foreach (InputParameter param in base.Context.Parameters)
				{
					template.SetProperty(param.Name, param.Value);
				}
				foreach (Helper helper in base.Context.Helpers)
				{
					template.SetProperty(helper.Name, helper.CreateInstance());
				}

				template.Render(output);
			}
			else
			{
				string errs = "";
				foreach (CompilerError error in compiler.Errors)
				{
					errs += error.ToString() + "\r\n";
				}
				throw new CodeGenerationException("The following compiler error(s) occured while trying to compile the CodeSmith template '" + this.Template.FileName + "': " + errs);
			}
		}
		
	}
}
