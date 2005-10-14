using System;
using System.Text;

namespace Adapdev.Cryptography
{

	/// <summary>
	/// Summary description for Crypto.
	/// </summary>
	public class Crypto
	{
		// Des
		private static string _password8 = "aZbX12Yu";
		// Rijndael / TripleDes
		private static string _password16 = "aI/c$kd8Hbb1R4nv";
		// Des / TripleDes
		private static string _vector8 = "xbhhU7yp";
		// Rijndael
		private static string _vector16 = "ai(hu#4x7^6txgGh";
		
		/// <summary>
		/// Creates a new <see cref="Crypto"/> instance.
		/// </summary>
		private Crypto()
		{
		}

		/// <summary>
		/// Encrypts the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="algorithm">Algorithm.</param>
		public static byte[] Encrypt(string text, EncryptionAlgorithm algorithm)
		{
			if(algorithm == EncryptionAlgorithm.Des)
			{
				return Encrypt(text, Crypto._password8, Crypto._vector8, algorithm);
			}
	
			if(algorithm == EncryptionAlgorithm.Rijndael)
			{
				return Encrypt(text, Crypto._password16, Crypto._vector16, algorithm);
			}
	
			if(algorithm == EncryptionAlgorithm.TripleDes)
			{
				return Encrypt(text, Crypto._password16, Crypto._vector8, algorithm);
			}

			throw new Exception(algorithm.ToString() + " is not supported.");
		}

		/// <summary>
		/// Encrypts the specified text.
		/// </summary>
		/// <param name="text">Text.</param>
		/// <param name="key">Key.</param>
		/// <param name="vector">Vector.</param>
		/// <param name="algorithm">Algorithm.</param>
		public static byte[] Encrypt(string text, string key, string vector, EncryptionAlgorithm algorithm)
		{
			Validate(algorithm, key, vector);

			byte[] tIV = null;
			byte[] tkey = null;
			byte[] cipherText = null;
			byte[] plainText = Encoding.ASCII.GetBytes(text);

			Encryptor e = new Encryptor(algorithm);
			tkey = Encoding.ASCII.GetBytes(key);
			tIV = Encoding.ASCII.GetBytes(vector);
			e.IV = tIV;

			cipherText = e.Encrypt(plainText, tkey);

			return cipherText;
		}

		/// <summary>
		/// Validates the specified algorithm.
		/// </summary>
		/// <param name="algorithm">Algorithm.</param>
		/// <param name="key">Key.</param>
		/// <param name="vector">Vector.</param>
		private static void Validate(EncryptionAlgorithm algorithm, string key, string vector)
		{
			if(algorithm == EncryptionAlgorithm.Des)
			{
				if(key.Length != 8) throw new Exception("key length must be 8 for " + algorithm.ToString());
				if(vector.Length != 8) throw new Exception("vector length must be 8 for " + algorithm.ToString());
			}
	
			if(algorithm == EncryptionAlgorithm.Rijndael)
			{
				if(key.Length != 16) throw new Exception("key length must be 16 for " + algorithm.ToString());
				if(vector.Length != 16) throw new Exception("vector length must be 16 for " + algorithm.ToString());
			}
	
			if(algorithm == EncryptionAlgorithm.TripleDes)
			{
				if(key.Length != 16) throw new Exception("key length must be 16 for " + algorithm.ToString());
				if(vector.Length != 8) throw new Exception("vector length must be 8 for " + algorithm.ToString());
			}
		}

		/// <summary>
		/// Decrypts the specified bytes.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="vector">Vector.</param>
		/// <param name="algorithm">Algorithm.</param>
		/// <returns></returns>
		public static string Decrypt(byte[] cipherText, string key, string vector, EncryptionAlgorithm algorithm)
		{
			Validate(algorithm, key, vector);

			byte[] tIV = null;
			byte[] tkey = null;
			byte[] plainText = null;

			Decryptor dec = new Decryptor(algorithm);
			tkey = Encoding.ASCII.GetBytes(key);
			tIV = Encoding.ASCII.GetBytes(vector);
			dec.IV = tIV;

			// Go ahead and decrypt.
			plainText = dec.Decrypt(cipherText, tkey);

			return Encoding.ASCII.GetString(plainText);
		}

		/// <summary>
		/// Encrypts the specified text.
		/// </summary>
		/// <param name="cipherText">CipherText.</param>
		/// <param name="algorithm">Algorithm.</param>
		public static string Decrypt(byte[] cipherText, EncryptionAlgorithm algorithm)
		{
			if(algorithm == EncryptionAlgorithm.Des)
			{
				return Decrypt(cipherText, Crypto._password8, Crypto._vector8, algorithm);
			}
	
			if(algorithm == EncryptionAlgorithm.Rijndael)
			{
				return Decrypt(cipherText, Crypto._password16, Crypto._vector16, algorithm);
			}
	
			if(algorithm == EncryptionAlgorithm.TripleDes)
			{
				return Decrypt(cipherText, Crypto._password16, Crypto._vector8, algorithm);
			}

			throw new Exception(algorithm.ToString() + " is not supported.");
		}
	}
}
