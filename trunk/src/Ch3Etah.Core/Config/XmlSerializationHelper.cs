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
 *   Date: 22/9/2004
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Ch3Etah.Core.Config
{
	/// <summary>
	/// Provides static methods for serializing and deserializing
	/// objects to XML.
	/// </summary>
	public class XmlSerializationHelper
	{
//		private XmlSerializationHelper()
//		{
//		}
		
		public static object LoadObject(string fileName, Type objectType){
            FileInfo file = new FileInfo(fileName);
            if (!file.Exists)
            {
                throw new FileNotFoundException(fileName + " does not exist.");
            }
			using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read)) {
				object o = LoadObject(stream, objectType);
				stream.Close();
				return o;
			}
 		}
		
		public static object LoadObject(Stream stream, Type objectType){
			object o = null;
			TextReader reader = null;
			try {
				reader = new StreamReader(stream);
				XmlSerializer ser = new XmlSerializer(objectType);
				o = ser.Deserialize(reader);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				throw ex;
			}
			return o;
		}
		
		public static object LoadObject(XmlNode node, Type objectType){
			XmlNodeReader reader = new XmlNodeReader(node);
			object o = Activator.CreateInstance(objectType);
			try {
				XmlSerializer ser = new XmlSerializer(objectType);
				o = ser.Deserialize(reader);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				throw ex;
			}
			return o;
		}

        public static void SaveObject(object obj, string fileName, Type objectType)
        {
            FileInfo file = new FileInfo(fileName);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }
			using (FileStream stream = new FileStream(fileName, FileMode.Create)) {
				SaveObject(obj, stream, objectType);
				stream.Close();
			}
		}
		
		public static void SaveObject(object obj, Stream stream, Type objectType) {
			TextWriter writer = null;
			try {
				writer = new StreamWriter(stream);
				XmlSerializer ser = new XmlSerializer(objectType);
				ser.Serialize(writer, obj);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.ToString());
				throw ex;
			}
		}
		
	}
}
