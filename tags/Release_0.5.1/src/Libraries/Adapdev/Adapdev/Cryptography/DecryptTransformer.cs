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

namespace Adapdev.Cryptography
{
	/// <summary>
	/// Summary description for DecryptTransformer.
	/// </summary>
	internal class DecryptTransformer
	{
		private EncryptionAlgorithm _algorithmID;
		private byte[] _initVec;

		internal DecryptTransformer(EncryptionAlgorithm deCryptId)
		{
			_algorithmID = deCryptId;
		}

		internal ICryptoTransform GetCryptoServiceProvider(byte[] bytesKey)
		{
			// Pick the provider.
			switch (_algorithmID)
			{
				case EncryptionAlgorithm.Des:
				{
					DES des = new DESCryptoServiceProvider();
					des.Mode = CipherMode.CBC;
					des.Key = bytesKey;
					des.IV = _initVec;
					return des.CreateDecryptor();
				}
				case EncryptionAlgorithm.TripleDes:
				{
					TripleDES des3 = new TripleDESCryptoServiceProvider();
					des3.Mode = CipherMode.CBC;
					return des3.CreateDecryptor(bytesKey, _initVec);
				}
				case EncryptionAlgorithm.Rc2:
				{
					RC2 rc2 = new RC2CryptoServiceProvider();
					rc2.Mode = CipherMode.CBC;
					return rc2.CreateDecryptor(bytesKey, _initVec);
				}
				case EncryptionAlgorithm.Rijndael:
				{
					Rijndael rijndael = new RijndaelManaged();
					rijndael.Mode = CipherMode.CBC;
					return rijndael.CreateDecryptor(bytesKey, _initVec);
				}
				default:
				{
					throw new CryptographicException("Algorithm ID '" + _algorithmID +
						"' not supported.");
				}
			}
		} //end GetCryptoServiceProvider


		internal byte[] IV
		{
			set{_initVec = value;}
		}
	}
}
