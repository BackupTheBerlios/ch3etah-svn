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
	/// Summary description for EncryptTransformer.
	/// </summary>
	internal class EncryptTransformer
	{
		private EncryptionAlgorithm _algorithmID;
		private byte[] _initVec;
		private byte[] _encKey;

		
		internal EncryptTransformer(EncryptionAlgorithm algId)
		{
			//Save the algorithm being used.
			_algorithmID = algId;
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

					// See if a key was provided
					if (null == bytesKey)
					{
						_encKey = des.Key;
					}
					else
					{
						des.Key = bytesKey;
						_encKey = des.Key;
					}

					// See if the client provided an initialization vector
					if (null == _initVec)
					{ // Have the algorithm create one
						_initVec = des.IV;
					}
					else
					{ //No, give it to the algorithm
						des.IV = _initVec;
					}
					return des.CreateEncryptor();
				}
				case EncryptionAlgorithm.TripleDes:
				{
					TripleDES des3 = new TripleDESCryptoServiceProvider();
					des3.Mode = CipherMode.CBC;
					// See if a key was provided
					if (null == bytesKey)
					{
						_encKey = des3.Key;
					}
					else
					{
						des3.Key = bytesKey;
						_encKey = des3.Key;
					}
					// See if the client provided an IV
					if (null == _initVec)
					{ //Yes, have the alg create one
						_initVec = des3.IV;
					}
					else
					{ //No, give it to the alg.
						des3.IV = _initVec;
					}
					return des3.CreateEncryptor();
				}
				case EncryptionAlgorithm.Rc2:
				{
					RC2 rc2 = new RC2CryptoServiceProvider();
					rc2.Mode = CipherMode.CBC;
					// Test to see if a key was provided
					if (null == bytesKey)
					{
						 _encKey = rc2.Key;
					}
					else
					{
						rc2.Key = bytesKey;
						_encKey = rc2.Key;
					}
					// See if the client provided an IV
					if (null == _initVec)
					{ //Yes, have the alg create one
						_initVec = rc2.IV;
					}
					else
					{ //No, give it to the alg.
						rc2.IV = _initVec;
					}
					return rc2.CreateEncryptor();
				}
				case EncryptionAlgorithm.Rijndael:
				{
					Rijndael rijndael = new RijndaelManaged();
					rijndael.Mode = CipherMode.CBC;
					// Test to see if a key was provided
					if(null == bytesKey)
					{
						_encKey = rijndael.Key;
					}
					else
					{
						rijndael.Key = bytesKey;
						_encKey = rijndael.Key;
					}
					// See if the client provided an IV
					if(null == _initVec)
					{ //Yes, have the alg create one
						_initVec = rijndael.IV;
					}
					else
					{ //No, give it to the alg.
						rijndael.IV = _initVec;
					}
					return rijndael.CreateEncryptor();
				}
				default:
				{
					throw new CryptographicException("Algorithm ID '" + _algorithmID +
						"' not supported.");
				}
			}

		}

		internal byte[] IV
		{
			get{return _initVec;}
			set{_initVec = value;}
		}

		internal byte[] Key
		{
			get{return _encKey;}
		}

	}

}
