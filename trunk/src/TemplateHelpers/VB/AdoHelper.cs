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
 *   Date: 21/9/2004
 */

using System;

namespace Ch3Etah.TemplateHelpers
{
	/// <summary>
	/// Helper functions that are usefull when creating templates that 
	/// output ADO code.
	/// </summary>
	public class AdoHelper
	{
		public AdoHelper()
		{
		}
		
		public string VbSqlString(string sql, string variableName) {
			string val = sql.Trim();
			val = val.Replace("\t", " ");
			val = val.Replace("\r\n", " \" & vbCrLf \r\n    " + variableName + " = " + variableName + " & " + "\"");
			val = val.Replace("\n\r", " \" & vbCrLf \r\n    " + variableName + " = " + variableName + " & " + "\"");
			return val;
		}
		
		public string DbType2AdoType (string DbType) {
			switch (DbType.ToUpper()) {
				case "BIGINT":
					return "adBigInt";
				case "BIT":
					return "adBoolean";
				case "CHAR":
					return "adChar";
				case "MONEY":
					return "adCurrency";
				case "DATETIME":
					return "adDate";
				case "SMALLDATETIME":
					return "adDate";
				case "DECIMAL":
					return "adDecimal";
				case "FLOAT":
					return "adDouble";
				case "GUID":
					return "adGUID";
				case "UNIQUEIDENTIFIER":
					return "adGUID";
				case "INT":
					return "adInteger";
				case "INTEGER":
					return "adInteger";
				case "NUMERIC":
					return "adNumeric";
				case "REAL":
					return "adSingle";
				case "SMALLINT":
					return "adSmallInt";
				case "TINYINT":
					return "adTinyInt";
				case "VARCHAR":
					return "adVarChar";
				case "NVARCHAR":
					return "adVarWChar";
				case "NCHAR":
					return "adWChar";
				default:
					throw new ArgumentException("The ADO parameter datatype for DbType '" + DbType + "' is not known.");
			}
		}
		
		
	}
}
