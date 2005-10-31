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
	/// Summary description for Decryptor.
	/// </summary>
	internal class Decryptor
	{
		private DecryptTransformer _transformer;
		private byte[] _initVec;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="algId">The algorithm to use for decryption</param>
		internal Decryptor(EncryptionAlgorithm algId)
		{
			_transformer = new DecryptTransformer(algId);
		}

		/// <summary>
		/// Decrypts the data
		/// </summary>
		/// <param name="bytesData">The data to decrypt</param>
		/// <param name="bytesKey">The key to use</param>
		/// <returns></returns>
		internal byte[] Decrypt(byte[] bytesData, byte[] bytesKey)
		{
			//Set up the memory stream for the decrypted data.
			MemoryStream memStreamDecryptedData = new MemoryStream();
			//Pass in the initialization vector.
			_transformer.IV = _initVec;
			ICryptoTransform transform = _transformer.GetCryptoServiceProvider(bytesKey);
			CryptoStream decStream = new CryptoStream(memStreamDecryptedData,
				transform,
				CryptoStreamMode.Write);
			try
			{
				decStream.Write(bytesData, 0, bytesData.Length);
			}
			catch(Exception ex)
			{
				throw new Exception("Error while writing encrypted data to the stream: \n"
					+ ex.Message);
			}
			decStream.FlushFinalBlock();
			decStream.Close();
			// Send the data back.
			return memStreamDecryptedData.ToArray();
		} //end Decrypt

		/// <summary>
		/// Sets the initial vector (IV)
		/// </summary>
		internal byte[] IV
		{
			set{_initVec = value;}
		}
	}
}
