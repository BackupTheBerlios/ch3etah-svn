// Original Copyright 2002 Microsoft Corporation. http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dnnetsec/html/SecNetHT10.asp
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
using System.Security.Cryptography;
using System.IO;

namespace Adapdev.Cryptography
{
	/// <summary>
	/// Summary description for Encryptor.
	/// </summary>
	internal class Encryptor
	{
		private EncryptTransformer _transformer;
		private byte[] _initVec;
		private byte[] _encKey;		
		
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="algId">The encryption algorithm to use</param>
		internal Encryptor(EncryptionAlgorithm algId)
		{
			_transformer = new EncryptTransformer(algId);
		}

		/// <summary>
		/// Encrypts data
		/// </summary>
		/// <param name="bytesData">The data to encrypt</param>
		/// <param name="bytesKey">The key to use</param>
		/// <returns></returns>
		internal byte[] Encrypt(byte[] bytesData, byte[] bytesKey)
		{
			//Set up the stream that will hold the encrypted data.
			MemoryStream memStreamEncryptedData = new MemoryStream();
			_transformer.IV = _initVec;
			ICryptoTransform transform = _transformer.GetCryptoServiceProvider(bytesKey);
			CryptoStream encStream = new CryptoStream(memStreamEncryptedData,
				transform,
				CryptoStreamMode.Write);
			try
			{
				//Encrypt the data, write it to the memory stream.
				encStream.Write(bytesData, 0, bytesData.Length);
			}
			catch(Exception ex)
			{
				throw new Exception("Error while writing encrypted data to the stream: \n"
					+ ex.Message);
			}
			//Set the IV and key for the client to retrieve
			_encKey = _transformer.Key;
			_initVec = _transformer.IV;
			encStream.FlushFinalBlock();
			encStream.Close();
			//Send the data back.
			return memStreamEncryptedData.ToArray();
		}//end Encrypt

		/// <summary>
		/// Gets / sets the initial vector
		/// </summary>
		internal byte[] IV
		{
			get{return _initVec;}
			set{_initVec = value;}
		}

		/// <summary>
		/// Gets / sets the key
		/// </summary>
		internal byte[] Key
		{
			get{return _encKey;}
		}
	}
}
