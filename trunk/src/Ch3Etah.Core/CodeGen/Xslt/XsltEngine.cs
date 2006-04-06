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

using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Ch3Etah.Core.CodeGen;
using Ch3Etah.Core.CodeGen.PackageLib;

namespace Ch3Etah.Core.CodeGen.Xslt {
    public class XsltTransformationEngine: TransformationEngineBase {
    	
    	public override void Transform(XmlNode input, TextWriter writer) {
        	XslTransform transformer = new XslTransform();
            transformer.Load(Template.GetFullPath());
            transformer.Transform(input.CreateNavigator(), CreateArgs(), writer, new XmlUrlResolver());
        }


        private XsltArgumentList CreateArgs() {
            XsltArgumentList args = new XsltArgumentList();

        	InputParameterCollection parms = Context.Parameters;
            foreach (InputParameter parameter in parms) {
                args.AddParam(parameter.Name, "", parameter.Value);
            }

            return args;
        }
		
		public override void ClearCache()
		{
			// No caching implemented.
		}
    }
}