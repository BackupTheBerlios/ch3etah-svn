// Original Copyright (c) 2004 Brad Vincent.  http://www.codeproject.com/csharp/CyptoHashing.asp
#region Modified Copyright / License Information
/*

   Copyright 2004 - 2005 Adapdev Technologies, LLC

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.

============================
Author Log
============================
III	Full Name
SMM	Sean McCormack (Adapdev)


============================
Change Log
============================
III	MMDDYY	Change

*/
#endregion
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Adapdev.Cryptography
{
	/// <summary>
	/// Hashing class. Only static members so no need to create an instance
	/// </summary>
	public class Hasher
	{
		#region enum, constants and fields
		//types of hashing available
		public enum HashType
		{
			SHA, SHA256, SHA384, SHA512, MD5
		}
		#endregion

		#region static members
		public static string Hash(String inputText)
		{
			return ComputeHash(inputText,HashType.MD5);
		}
		
		public static string Hash(String inputText, HashType hashingType)
		{
			return ComputeHash(inputText,hashingType);
		}

		/// <summary>
		///		returns true if the input text is equal to hashed text
		/// </summary>
		/// <param name="inputText">unhashed text to test</param>
		/// <param name="hashText">already hashed text</param>
		/// <returns>boolean true or false</returns>
		public static bool IsHashEqual(string inputText, string hashText)
		{
			return (Hash(inputText) == hashText);
		}

		public static bool IsHashEqual(string inputText, string hashText, HashType hashingType)
		{
			return (Hash(inputText,hashingType) == hashText);
		}
		#endregion

		#region Hashing Engine

		/// <summary>
		///		computes the hash code and converts it to string
		/// </summary>
		/// <param name="inputText">input text to be hashed</param>
		/// <param name="hashingType">type of hashing to use</param>
		/// <returns>hashed string</returns>
		private static string ComputeHash(string inputText, HashType hashingType)
		{
			HashAlgorithm HA = getHashAlgorithm(hashingType);

			//declare a new encoder
			UTF8Encoding UTF8Encoder = new UTF8Encoding();
			//get byte representation of input text
			byte[] inputBytes = UTF8Encoder.GetBytes(inputText);
			
			
			//hash the input byte array
			byte[] output = HA.ComputeHash(inputBytes);

			//convert output byte array to a string
			return Convert.ToBase64String(output);
		}

		/// <summary>
		///		returns the specific hashing alorithm
		/// </summary>
		/// <param name="hashingType">type of hashing to use</param>
		/// <returns>HashAlgorithm</returns>
		private static HashAlgorithm getHashAlgorithm(HashType hashingType)
		{
			switch (hashingType)
			{
				case HashType.MD5 :
					return new MD5CryptoServiceProvider();
				case HashType.SHA :
					return new SHA1CryptoServiceProvider();
				case HashType.SHA256 :
					return new SHA256Managed();
				case HashType.SHA384 :
					return new SHA384Managed();
				case HashType.SHA512 :
					return new SHA512Managed();
				default :
					return new MD5CryptoServiceProvider();
			}
		}
		#endregion

	}

}
