using System;
using System.Security.Cryptography;
using System.Text;

//namespace Sabresaurus.PlayerPrefsExtensions
//{
    public static class SimpleEncryption
	{
		// IMPORTANT: Make sure to change this key for each project you use this encryption in to help secure your
		// encrypted values. This key must be exactly 32 characters long (256 bit).
		private static string key = "TISBPD40TIMryz4FcN3H1QJWcsITl12y";

		// Cache the encryption provider
		private static RijndaelManaged provider = null;

		private static void SetupProvider()
		{
			provider = new RijndaelManaged();// Create a new encryption provider
            provider.Key = Encoding.ASCII.GetBytes(key);// Get the bytes from the supplied string key and use it as the provider's key
            provider.Mode = CipherMode.ECB;// Ensure that the same data is always encrypted the same way when used with the same key
        }

		/// <summary>
		/// Encrypts the specified string using the key stored in SimpleEncryption and returns the encrypted result
		/// </summary>
		public static string EncryptString(string sourceString)
		{
			if(provider == null)SetupProvider();// Encryption provider hasn't been set up yet, so set it up
			ICryptoTransform encryptor = provider.CreateEncryptor();// Create an encryptor to encrypt the bytes
			byte[] sourceBytes = Encoding.UTF8.GetBytes(sourceString);// Convert the source string into bytes to be encrypted
			byte[] outputBytes = encryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);// Encrypt the bytes using the encryptor we just created
			return Convert.ToBase64String(outputBytes);// Convert the encrypted bytes into a Base 64 string, so we can safely represent them as a string and return that string
		}

		/// <summary>
		/// Decrypts the specified string from its specified encrypted value into the returned decrypted value using the
		/// key stored in SimpleEncryption
		/// </summary>
		public static string DecryptString(string sourceString)
		{
			if(provider == null)SetupProvider();// Encryption provider hasn't been set up yet, so set it up
			ICryptoTransform decryptor = provider.CreateDecryptor();// Create a decryptor to decrypt the encrypted bytes
			byte[] sourceBytes = Convert.FromBase64String(sourceString);// Convert the base 64 string representing the encrypted bytes back into an array of encrypted bytes
			byte[] outputBytes = decryptor.TransformFinalBlock(sourceBytes, 0, sourceBytes.Length);// Use the decryptor we just created to decrypt those bytes
			return Encoding.UTF8.GetString(outputBytes);// Turn the decrypted bytes back into the decrypted string and return it
		}

		/// <summary>
		/// Encrypts the specified float value and returns an encrypted string
		/// </summary>
		public static string EncryptFloat(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);// Convert the float into its 4 bytes
			string base64 = Convert.ToBase64String(bytes);// Represent those bytes as a base 64 string
			return EncryptString(base64);// Return the encrypted version of that base 64 string
		}

		/// <summary>
		/// Encrypts the specified int value and returns an encrypted string
		/// </summary>
		public static string EncryptInt(int value)
		{
			byte[] bytes = BitConverter.GetBytes(value);// Convert the int value into its 4 bytes
			string base64 = Convert.ToBase64String(bytes);// Represent those bytes as a base 64 string
			return EncryptString(base64);// Return the encrypted version of that base 64 string
		}

		/// <summary>
		/// Decrypts the encrypted string representing a float into the decrypted float
		/// </summary>
		public static float DecryptFloat(string sourceString)
		{
			string decryptedString = DecryptString(sourceString);// Decrypt the encrypted string 
			byte[] bytes = Convert.FromBase64String(decryptedString);// Convert the decrypted Base 64 representation back into bytes
			return BitConverter.ToSingle(bytes, 0);// Turn the bytes back into a float and return it
		}

		/// <summary>
		/// Decrypts the encrypted string representing an int into the decrypted int
		/// </summary>
		public static int DecryptInt(string sourceString)
		{
			string decryptedString = DecryptString(sourceString);// Decrypt the encrypted string 
			byte[] bytes = Convert.FromBase64String(decryptedString);// Convert the decrypted Base 64 representation back into bytes
			return BitConverter.ToInt32(bytes, 0);// Turn the bytes back into a int and return it
		}
	}
//}