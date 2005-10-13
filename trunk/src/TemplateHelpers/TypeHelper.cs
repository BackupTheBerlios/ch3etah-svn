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
 *   Date: 1/12/2004
 */

using System;
using System.ComponentModel;

namespace Ch3Etah.TemplateHelpers {
	/// <summary>
	/// Description of TypeHelper.
	/// </summary>
	public class TypeHelper {
		public TypeHelper() {
		}
		
		public string DBType2VBType(string type) {
			return DBType2DotNetType(type);
		}
		
		public string DBType2DotNetType(string type) {
			
			switch (type.ToUpper()) {
				case "BIGINT":
					return "Int64";
				case "BIT":
					return "Boolean";
				case "MONEY":
					return "Decimal";
				case "DATETIME":
					return "DateTime";
				case "SMALLDATETIME":
					return "DateTime";
				case "DECIMAL":
					return "Decimal";
				case "FLOAT":
					return "Double";
				case "GUID":
					return "Guid";
				case "UNIQUEIDENTIFIER":
					return "Guid";
				case "INT":
					return "Int32";
				case "INTEGER":
					return "Int32";
				case "NUMERIC":
					return "Decimal";
				case "REAL":
					return "Single";
				case "SMALLINT":
					return "Int16";
				case "TINYINT":
					return "Byte";
				case "VARCHAR":
					return "String";
				case "NVARCHAR":
					return "String";
				case "CHAR":
					return "String";
				case "NCHAR":
					return "String";
				case "TEXT":
					return "String";
				case "NTEXT":
					return "String";
				case "BINARY":
					return "Object";
				case "VARBINARY":
					return "Object";
				case "IMAGE":
					return "Object";
				default:
					throw new ArgumentException("The DBType '" + type + "' is not recognized.");
			}
		}
	
		public string DBType2XsdType(string type) {
			
			switch (type.ToUpper()) {
				case "BIGINT":
					return "xs:long";

				case "BIT":
					return "xs:boolean";

				case "DATETIME": case "SMALLDATETIME":
					return "xs:dateTime";

				case "MONEY": case "SMALLMONEY": case "DECIMAL": case "NUMERIC":
					return "xs:decimal";

				case "FLOAT":
					return "xs:double";

				case "GUID":
					return "Guid";

				case "UNIQUEIDENTIFIER":
					return "xs:ID";

				case "INT": case "INTEGER":
					return "xs:int";

				case "REAL":
					return "Single";

				case "SMALLINT":
					return "xs:short";

				case "TINYINT":
					return "xs:byte";

				case "VARCHAR": case "NVARCHAR": case "CHAR": case "NCHAR": case "TEXT": case "NTEXT":
					return "xs:string";

				case "BINARY": case "VARBINARY": case "IMAGE":
					return "xs:base64Binary";

				default:
					throw new ArgumentException("The DDType '" + type + "' is not recognized.");
			}
		}
	
		public string NetType2XsdType(string type) {
			
			switch (type.ToUpper()) {
				case "INT64": case "SYSTEM.INT64": case "LONG":
					return "xs:long";

				case "BOOLEAN": case "SYSTEM.BOOLEAN": case "BOOL":
					return "xs:boolean";

				case "DATETIME": case "SYSTEM.DATETIME": case "DATE":
					return "xs:dateTime";

				case "DECIMAL": case "SYSTEM.DECIMAL": 
					return "xs:decimal";

				case "SINGLE": case "SYSTEM.SINGLE": case "FLOAT":
					return "xs:float";

				case "DOUBLE": case "SYSTEM.DOUBLE": 
					return "xs:double";

				case "INT32": case "SYSTEM.INT32": case "INT": case "INTEGER":
					return "xs:int";

				case "INT16": case "SYSTEM.INT16": case "SHORT":
					return "xs:short";

				case "BYTE": case "SYSTEM.BYTE": 
					return "xs:byte";

				default:
					return "xs:string";
			}
		}
	
		public string CSDefaultValue(string type, string defaultValue) {
			switch (type.ToUpper()) {
				case "STRING": case "SYSTEM.STRING": 
					if (defaultValue == "") {
						return "\"\"";
					}
					else {
						return "\"" + defaultValue + "\"";
					}
				case "DECIMAL": case "SYSTEM.DECIMAL": 
				case "DOUBLE": case "SYSTEM.DOUBLE": 
				case "SINGLE": case "SYSTEM.SINGLE": 
				case "INT64": case "SYSTEM.INT64": 
				case "INT32": case "SYSTEM.INT32": 
				case "INT16": case "SYSTEM.INT16": 
				case "BYTE": case "SYSTEM.BYTE": 
				case "SBYTE": case "SYSTEM.SBYTE": 
					if (defaultValue == "") {
						return "0";
					}
					else {
						return defaultValue;
					}
				case "DATE": case "SYSTEM.DATE": 
				case "DATETIME":
					if (defaultValue == "" || defaultValue == "Now") {
						return "DateTime.Now";
					}
					else if (defaultValue == "Today" || defaultValue == "Date") {
						return "DateTime.Today";
					}
					else {
						return defaultValue;
					}
				case "BOOL":
				case "BOOLEAN": case "SYSTEM.BOOLEAN": 
					if (defaultValue == "") {
						return "false";
					}
					else {
						return defaultValue;
					}
				case "GUID": case "SYSTEM.GUID": 
					if (defaultValue == "") {
						return "Guid.NewGuid()";
					}
					else {
						return defaultValue;
					}
				default:
					if (defaultValue == "") {
						return "null";
					}
					else {
						return defaultValue;
					}
			}
		}
		
		public string CSNullValue(string type, string nullValue) {
			switch (type.ToUpper()) {
				case "STRING":
					if (nullValue == "") {
						return "\"<NULL>\"";
					}
					else {
						return "\"" + nullValue + "\"";
					}
				case "DECIMAL":
					if (nullValue == "") {
						return "decimal.MinValue";
					}
					else {
						return nullValue;
					}
				case "DOUBLE":
					if (nullValue == "") {
						return "double.NaN";
					}
					else {
						return nullValue;
					}
				case "SINGLE":
					if (nullValue == "") {
						return "Single.NaN";
					}
					else {
						return nullValue;
					}
				case "INT64":
					if (nullValue == "") {
						return "Int64.MinValue";
					}
					else {
						return nullValue;
					}
				case "INT32":
					if (nullValue == "") {
						return "Int32.MinValue";
					}
					else {
						return nullValue;
					}
				case "INT16":
					if (nullValue == "") {
						return "Int16.MinValue";
					}
					else {
						return nullValue;
					}
				case "BYTE":
					if (nullValue == "") {
						return "byte.MinValue";
					}
					else {
						return nullValue;
					}
				case "DATETIME":
					if (nullValue == "") {
						return "DateTime.MinValue";
					}
					else {
						return nullValue;
					}
				case "BOOLEAN":
					return "TemplateHelper Error: null values are not supported for the Boolean .NET data type.";
				case "GUID":
					if (nullValue == "") {
						return "Guid.Empty";
					}
					else {
						return nullValue;
					}
				default:
					throw new ArgumentException("TemplateHelper Error: The .NET Type '" + type + "' is not recognized for null handling.");
			}
		}
		
		public string Guid() {
			return new Guid().ToString();
		}
		
		public string Guid(string seed) {
			uint intVal = (uint) seed.GetHashCode();
			ushort shortVal1 = (ushort) (intVal >> 16);
			ushort shortVal2 = (ushort) (intVal << 2);
			byte byte1 = (byte) (shortVal1 >> 8);
			byte byte2 = (byte) (shortVal1 >> 12);
			byte byte3 = (byte) (shortVal2 >> 3);
			byte byte4 = (byte) (shortVal2 >> 9);
			return new Guid(intVal, shortVal1, shortVal2, byte1, byte2, byte3, byte4, byte1, byte2, byte3, byte4).ToString();
		}
		
	}
}
