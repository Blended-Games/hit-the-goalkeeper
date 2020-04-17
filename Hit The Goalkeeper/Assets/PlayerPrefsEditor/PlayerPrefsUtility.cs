using System;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsUtility
{
	// Each encrypted player pref key is given a prefix (this helps the Editor to identify them)
	public const string KEY_PREFIX = "ENC-";

	// Each encrypted value is prefixed with a type identifier (because encrypted values are stored as strings)
	public const string floatValuePrefix = ":float:", intValuePrefix = ":int:", stringValuePrefix = ":string:";
    public const string boolValuePrefix = ":bool:", colorValuePrefix = ":Color:", color32ValuePrefix = ":Color32:";
    public const string vector2ValuePrefix = ":Vector2:", vector3ValuePrefix = ":Vector3:", vector4ValuePrefix = ":Vector4:";
    public const string quaternionValuePrefix = ":Quaternion:", matrix4x4ValuePrefix = ":Matrix4x4:", rectValuePrefix = ":Rect:", boundsValuePrefix = ":Bounds:";
    //----------------------------------------
    public const string intArrayPrefix = ":int[]:", stringArrayPrefix = ":string[]:", floatArrayPrefix = ":float[]:", boolArrayPrefix = ":bool[]:";
    public const string colorArrayPrefix = ":Color[]:", color32ArrayPrefix = ":Color32[]:";
    public const string vector2ArrayPrefix = ":Vector2[]:", vector3ArrayPrefix = ":Vector3[]:", vector4ArrayPrefix = ":Vector4[]:";
    public const string quaternionArrayPrefix = ":Quaternion[]:", matrix4x4ArrayPrefix = ":Matrix4x4[]:", rectArrayPrefix = ":Rect[]:", boundsArrayPrefix = ":Bounds[]:";

    /// <summary>
    /// Determines if the specified player pref key refers to an encrypted record
    /// </summary>
    public static bool IsEncryptedKey (string key)
	{
        return key.StartsWith(KEY_PREFIX);// Encrypted keys use a special prefix
	}

	/// <summary>
	/// Decrypts the specified key
	/// </summary>
	public static string DecryptKey(string encryptedKey)
	{
		if(encryptedKey.StartsWith(KEY_PREFIX))
		{
			string strippedKey = encryptedKey.Substring(KEY_PREFIX.Length);// Remove the key prefix from the encrypted key
			return SimpleEncryption.DecryptString(strippedKey);// Return the decrypted key
		}
		else throw new InvalidOperationException("Could not decrypt item, no match found in known encrypted key prefixes");
	}

	/// <summary>
	/// Helper method that can handle any of the encrypted player pref types, returning a float, int or string based
	/// on what type of value has been stored.
	/// </summary>
	public static object GetEncryptedValue(string encryptedKey, string encryptedValue)
	{
        // See what type identifier the encrypted value starts
        if (encryptedValue.StartsWith(floatValuePrefix)) return GetEncryptedFloat(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));// It's a float, so decrypt it as a float and return the value
        else if (encryptedValue.StartsWith(intValuePrefix)) return GetEncryptedInt(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));// It's an int, so decrypt it as an int and return the value
        else if (encryptedValue.StartsWith(stringValuePrefix)) return GetEncryptedString(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));// It's a string, so decrypt it as a string and return the value
        else if (encryptedValue.StartsWith(boolValuePrefix)) return GetEncryptedBool(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedValue.StartsWith(colorValuePrefix)) return GetEncryptedColor(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(color32ValuePrefix)) return GetEncryptedColor32(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector2ValuePrefix)) return GetEncryptedVector2(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector3ValuePrefix)) return GetEncryptedVector3(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector4ValuePrefix)) return GetEncryptedVector4(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(quaternionValuePrefix)) return GetEncryptedQuaternion(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(matrix4x4ValuePrefix)) return GetEncryptedMatrix4x4(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(rectValuePrefix)) return GetEncryptedRect(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(boundsValuePrefix)) return GetEncryptedBounds(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(intArrayPrefix)) return GetEncryptedIntArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(stringArrayPrefix)) return GetEncryptedStringArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(floatArrayPrefix)) return GetEncryptedFloatArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(boolArrayPrefix)) return GetEncryptedBoolArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(colorArrayPrefix)) return GetEncryptedColorArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(color32ArrayPrefix)) return GetEncryptedColor32Array(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector2ArrayPrefix)) return GetEncryptedVector2Array(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector3ArrayPrefix)) return GetEncryptedVector3Array(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(vector4ArrayPrefix)) return GetEncryptedVector4Array(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(quaternionArrayPrefix)) return GetEncryptedQuaternionArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(matrix4x4ArrayPrefix)) return GetEncryptedMatrix4x4Array(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(rectArrayPrefix)) return GetEncryptedRectArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));
        else if (encryptedKey.StartsWith(boundsArrayPrefix)) return GetEncryptedBoundsArray(SimpleEncryption.DecryptString(encryptedKey.Substring(KEY_PREFIX.Length)));

        else throw new InvalidOperationException("Could not decrypt item, no match found in known encrypted key prefixes");
	}

    public static string DecryptData(string key, string prefix)
    {
        string encryptedKey = KEY_PREFIX + SimpleEncryption.EncryptString(key);
        string data = PlayerPrefs.GetString(encryptedKey);
        data = data.Substring(prefix.Length);
        return data = SimpleEncryption.DecryptString(data);
    }
    #region [Float]

    /// <summary>
    /// Encrypted version of PlayerPrefs.SetFloat(), stored key and value is encrypted in player prefs
    /// </summary>
    public static void SetEncryptedFloat(string key, float value)
	{
		string encryptedKey = SimpleEncryption.EncryptString(key);
		string encryptedValue = SimpleEncryption.EncryptFloat(value);
		
		PlayerPrefs.SetString(KEY_PREFIX + encryptedKey, floatValuePrefix + encryptedValue);// Store the encrypted key and value (with relevant identifying prefixes) in PlayerPrefs
	}

	/// <summary>
	/// Encrypted version of PlayerPrefs.GetFloat(), an unencrypted key is passed and the value is returned decrypted
	/// </summary>
	public static float GetEncryptedFloat(string key, float defaultValue = 0.0f)
	{
		string encryptedKey = KEY_PREFIX + SimpleEncryption.EncryptString(key);// Encrypt and prefix the key so we can look it up from player prefs
		string fetchedString = PlayerPrefs.GetString(encryptedKey);// Look up the encrypted value

		if(!string.IsNullOrEmpty(fetchedString))
		{
			fetchedString = fetchedString.Remove(0, 1);// Strip out the type identifier character
			return SimpleEncryption.DecryptFloat (fetchedString);// Decrypt and return the float value
		}
		else return defaultValue;// No existing player pref value, so return defaultValue instead
	}
    /// <summary>
    /// Save an array of float values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetFloatArray(string key, float[] values)
    {
        string data = string.Empty;
        foreach (int value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(floatArrayPrefix + key, data);
    }

    /// <summary>
    /// Get an array of float values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>float[]</returns>
    public static float[] GetFloatArray(string key, float[] defaultValue = default(float[]))
    {
        List<float> results = new List<float>();
        string[] dataSplit = PlayerPrefs.GetString(floatArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty) results.Add(float.Parse(data));
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static void SetEncryptedFloatArray(string key, float[] values)
    {
        string data = string.Empty;
        foreach (int value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), floatArrayPrefix + SimpleEncryption.EncryptString(data));
    }

    public static float[] GetEncryptedFloatArray(string key, float[] defaultValue = default(float[]))
    {
        string data = DecryptData(key, floatArrayPrefix);
        List<float> results = new List<float>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty) results.Add(float.Parse(_data));
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [int]
    /// <summary>
    /// Encrypted version of PlayerPrefs.SetInt(), stored key and value is encrypted in player prefs
    /// </summary>
    public static void SetEncryptedInt(string key, int value)
    {
        string encryptedKey = SimpleEncryption.EncryptString(key);
        string encryptedValue = SimpleEncryption.EncryptInt(value);

        PlayerPrefs.SetString(KEY_PREFIX + encryptedKey, intValuePrefix + encryptedValue);// Store the encrypted key and value (with relevant identifying prefixes) in PlayerPrefs
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.GetInt(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static int GetEncryptedInt(string key, int defaultValue = 0)
	{
		string encryptedKey = KEY_PREFIX + SimpleEncryption.EncryptString(key);// Encrypt and prefix the key so we can look it up from player prefs
		string fetchedString = PlayerPrefs.GetString(encryptedKey);// Look up the encrypted value

		if(!string.IsNullOrEmpty(fetchedString))
		{
			fetchedString = fetchedString.Remove(0, 1);// Strip out the type identifier character
			return SimpleEncryption.DecryptInt (fetchedString);// Decrypt and return the int value
		}
		else return defaultValue;// No existing player pref value, so return defaultValue instead
	}
    /// <summary>
    /// Sava an array of integer values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetIntArray(string key, int[] values)
    {
        string data = string.Empty;
        foreach (int value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(intArrayPrefix + key, data);
    }

    /// <summary>
    /// Get an array of integer values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>int[]</returns>
    public static int[] GetIntArray(string key, int[] defaultValue = default(int[]))
    {
        List<int> results = new List<int>();
        string[] dataSplit = PlayerPrefs.GetString(intArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty) results.Add(int.Parse(data));
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static void SetEncryptedIntArray(string key, int[] values)
    {
        string data = string.Empty;
        foreach (int value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), intArrayPrefix + SimpleEncryption.EncryptString(data));
    }

    public static int[] GetEncryptedIntArray(string key, int[] defaultValue = default(int[]))
    {
        List<int> results = new List<int>();
        string data = DecryptData(key, intArrayPrefix);
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty) results.Add(int.Parse(_data));
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [String]
    /// <summary>
    /// Encrypted version of PlayerPrefs.SetString(), stored key and value is encrypted in player prefs
    /// </summary>
    public static void SetEncryptedString(string key, string value)
    {
        string encryptedKey = SimpleEncryption.EncryptString(key);
        string encryptedValue = SimpleEncryption.EncryptString(value);

        PlayerPrefs.SetString(KEY_PREFIX + encryptedKey, stringValuePrefix + encryptedValue);// Store the encrypted key and value (with relevant identifying prefixes) in PlayerPrefs
    }

    /// <summary>
    /// Encrypted version of PlayerPrefs.GetString(), an unencrypted key is passed and the value is returned decrypted
    /// </summary>
    public static string GetEncryptedString(string key, string defaultValue = "")
	{
		string encryptedKey = KEY_PREFIX + SimpleEncryption.EncryptString(key);// Encrypt and prefix the key so we can look it up from player prefs
		string fetchedString = PlayerPrefs.GetString(encryptedKey);// Look up the encrypted value

		if(!string.IsNullOrEmpty(fetchedString))
		{
			fetchedString = fetchedString.Remove(0, 1);// Strip out the type identifier character
			return SimpleEncryption.DecryptString (fetchedString);// Decrypt and return the string value
		}
		else return defaultValue;// No existing player pref value, so return defaultValue instead
	}
    /// <summary>
    /// Save a string array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetStringArray(string key, string[] values)
    {
        string data = string.Empty;
        foreach (string value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(stringArrayPrefix + key, data);
    }

    /// <summary>
    /// Get an array of string values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>string[]</returns>
    public static string[] GetStringArray(string key, string[] defaultValue = default(string[]))
    {
        List<string> results = new List<string>();
        string[] dataSplit = PlayerPrefs.GetString(stringArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty) results.Add(data);
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static void SetEncryptedStringArray(string key, string[] values)
    {
        string data = string.Empty;
        foreach (string value in values)
        {
            data += value + "|";
        }
        PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), stringArrayPrefix + SimpleEncryption.EncryptString(data));
    }

    public static string[] GetEncryptedStringArray(string key, string[] defaultValue = default(string[]))
    {
        string data = DecryptData(key, stringArrayPrefix);
        List<string> results = new List<string>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty) results.Add(_data);
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Bool]
    /// <summary>
    /// Helper method to store a bool in PlayerPrefs (stored as an int)
    /// Store the bool as an int (1 for true, 0 for false)
    /// </summary>
    public static void SetBool(string key, bool value)
	{
			PlayerPrefs.SetString(key,value? "1":"0");
	}

	/// <summary>
	/// Helper method to retrieve a bool from PlayerPrefs (stored as an int)
	/// </summary>
	public static bool GetBool(string key, bool defaultValue = false)
	{
		// Use HasKey to check if the bool has been stored (as int defaults to 0 which is ambiguous with a stored False)
		if(PlayerPrefs.HasKey(key))
		{
			string value = PlayerPrefs.GetString(key);
            return value != "0";// As in C, assume zero is false and any non-zero value (including its intended 1) is true
		}
		else return defaultValue;// No existing player pref value, so return defaultValue instead
	}

    public static void SetEncryptedBool(string key, bool value) {
        string encryptedKey = SimpleEncryption.EncryptString(key), encryptedValue = SimpleEncryption.EncryptString(value ? "1" : "0");
        PlayerPrefs.SetString(KEY_PREFIX + encryptedKey, boolValuePrefix + encryptedValue);
        Debug.Log("Saved" + encryptedValue);
    }

    public static bool GetEncryptedBool(string key, bool defaultValue = false) {

        string encryptedKey = KEY_PREFIX + SimpleEncryption.EncryptString(key);
        string fetchedString = PlayerPrefs.GetString(encryptedKey);
        if (!string.IsNullOrEmpty(fetchedString))
        {
            fetchedString = fetchedString.Substring(boolValuePrefix.Length);
            fetchedString = SimpleEncryption.DecryptString(fetchedString);
            return fetchedString == "1";
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save a bool array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetBoolArray(string key, bool[] values)
    {
        string data = string.Empty;
        foreach (bool value in values)
        {
            data += (value == true) ? '1' : '0';
            data += "|";
        }
        PlayerPrefs.SetString(boolArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a bool array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>bool[]</returns>
    public static bool[] GetBoolArray(string key, bool[] defaultValue = default(bool[]))
    {
        List<bool> results = new List<bool>();
        string[] dataSplit = PlayerPrefs.GetString(boolArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    results.Add(data == "1" ? true : false);
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static void SetEncryptedBoolArray(string key, bool[] values)
    {
        string data = string.Empty;
        foreach (bool value in values)
        {
            data += (value == true) ? '1' : '0';
            data += "|";
        }
        PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), boolArrayPrefix + SimpleEncryption.EncryptString(data));
    }

    public static bool[] GetEncryptedBoolArray(string key, bool[] defaultValue = default(bool[]))
    {
        List<bool> results = new List<bool>();
        string data = DecryptData(key, boolArrayPrefix);
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    results.Add(_data == "1" ? true : false);
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
    #endregion
    #region [Color]
/// <summary>
    /// Save a Color value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetColor(string key, Color value, bool encrypt = false)
    {
        string data = value.r + "," + value.g + "," + value.b + "," + value.a;
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), colorValuePrefix + SimpleEncryption.EncryptString(data));
        else        PlayerPrefs.SetString(colorValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Color value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Color GetColor(string key, Color defaultValue = default(Color))
    {
        string data = PlayerPrefs.GetString(colorValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Color(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    public static Color GetEncryptedColor(string key, Color defaultValue = default(Color))
    {
        string data = DecryptData(key, colorValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Color(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save a Color array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetColorArray(string key, Color[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Color value in values)
        {
            data += value.r + "," + value.g + "," + value.b + "," + value.a;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), colorArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(colorArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Color array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Color[]</returns>
    public static Color[] GetColorArray(string key, Color[] defaultValue = default(Color[]))
    {
        List<Color> results = new List<Color>();
        string[] dataSplit = PlayerPrefs.GetString(colorArrayPrefix + key).Split('|');

        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Color(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Color[] GetEncryptedColorArray(string key, Color[] defaultValue = default(Color[]))
    {
        List<Color> results = new List<Color>();
        string data = DecryptData(key, colorArrayPrefix);
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Color(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Color32]
    /// <summary>
    /// Save a Color32 value 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetColor32(string key, Color32 value, bool encrypt = false)
    {
        string data = (value.r + "," + value.g + "," + value.b + "," + value.a);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), color32ValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(color32ValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Color32 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Color32 GetColor32(string key, Color32 defaultValue = default(Color32))
    {
        string data = PlayerPrefs.GetString(color32ValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Color32(byte.Parse(dataSplit[0]), byte.Parse(dataSplit[1]), byte.Parse(dataSplit[2]), byte.Parse(dataSplit[3]));
        else return defaultValue;
    }

    public static Color32 GetEncryptedColor32(string key, Color32 defaultValue = default(Color32))
    {
        string data = DecryptData(key, color32ValuePrefix);
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split(',');
            return new Color32(byte.Parse(dataSplit[0]), byte.Parse(dataSplit[1]), byte.Parse(dataSplit[2]), byte.Parse(dataSplit[3]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Color32 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetColor32Array(string key, Color32[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Color32 value in values)
        {
            data += value.r + "," + value.g + "," + value.b + "," + value.a;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), color32ArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(color32ArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Color32 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Color32[]</returns>
    public static Color32[] GetColor32Array(string key, Color32[] defaultValue = default(Color32[]))
    {
        List<Color32> results = new List<Color32>();
        string[] dataSplit = PlayerPrefs.GetString(color32ArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Color32(byte.Parse(split[0]), byte.Parse(split[1]), byte.Parse(split[2]), byte.Parse(split[3])));
                }

            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Color32[] GetEncryptedColor32Array(string key, Color32[] defaultValue = default(Color32[]))
    {
        string data = DecryptData(key, color32ArrayPrefix);
        List<Color32> results = new List<Color32>();

        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Color32(byte.Parse(split[0]), byte.Parse(split[1]), byte.Parse(split[2]), byte.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Vector2]
    /// <summary>
    /// Save a Vector2 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector2(string key, Vector2 value, bool encrypt = false)
    {
        string data = (value.x + "," + value.y);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector2ValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(vector2ValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Vector2 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector2</returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = default(Vector2))
    {
        string data = PlayerPrefs.GetString(vector2ValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector2(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]));
        else return defaultValue;
    }

    public static Vector2 GetEncryptedVector2(string key, Vector2 defaultValue = default(Vector2))
    {
        string data = DecryptData(key, vector2ValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Vector2(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save a Vector2 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector2Array(string key, Vector2[] values, bool encrypted = false)
    {
        string data = string.Empty;
        foreach (Vector2 value in values)
        {
            data += value.x + "," + value.y;
            data += "|";
        }
        if(encrypted) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector2ArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(vector2ArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Vector2 array values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector2[] GetVector2Array(string key, Vector2[] defaultValue = default(Vector2[]))
    {
        List<Vector2> results = new List<Vector2>();
        string[] dataSplit = PlayerPrefs.GetString(vector2ArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Vector2(float.Parse(split[0]), float.Parse(split[1])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Vector2[] GetEncryptedVector2Array(string key, Vector2[] defaultValue = default(Vector2[]))
    {
        string data = DecryptData(key, vector2ArrayPrefix);
        List<Vector2> results = new List<Vector2>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Vector2(float.Parse(split[0]), float.Parse(split[1])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Vector3]
    /// <summary>
    /// Save a Vector3 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector3(string key, Vector3 value, bool encrypt = false)
    {
        string data = (value.x + "," + value.y + "," + value.z);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector3ValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(vector3ValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Vector3 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector3</returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default(Vector3))
    {
        string data = PlayerPrefs.GetString(vector3ValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector3(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]));
        else return defaultValue;
    }

    public static Vector3 GetEncryptedVector3(string key, Vector3 defaultValue = default(Vector3))
    {
        string data = DecryptData(key, vector3ValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Vector3(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Vector3 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector3Array(string key, Vector3[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Vector3 value in values)
        {
            data += value.x + "," + value.y + "," + value.z;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector3ArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(vector3ArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Vector3 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector3[] GetVector3Array(string key, Vector3[] defaultValue = default(Vector3[]))
    {
        List<Vector3> results = new List<Vector3>();
        string[] dataSplit = PlayerPrefs.GetString(vector3ArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Vector3[] GetEncryptedVector3Array(string key, Vector3[] defaultValue = default(Vector3[]))
    {
        string data = DecryptData(key, vector3ArrayPrefix);
        List<Vector3> results = new List<Vector3>();

        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Vector4]
    /// <summary>
    /// Save a vector4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector4(string key, Vector4 value, bool encrypt = false)
    {
        string data = (value.x + "," + value.y + "," + value.z + "," + value.w);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector4ValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(vector4ValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Vector4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector4</returns>
    public static Vector4 GetVector4(string key, Vector4 defaultValue = default(Vector4))
    {
        string data = PlayerPrefs.GetString(vector4ValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector4(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    public static Vector4 GetEncryptedVector4(string key, Vector4 defaultValue = default(Vector4))
    {
        string data = DecryptData(key, vector4ValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Vector4(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Vector4 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector4Array(string key, Vector4[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Vector4 value in values)
        {
            data += value.x + "," + value.y + "," + value.z + "," + value.w;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), vector4ArrayPrefix + SimpleEncryption.EncryptString(data));
        PlayerPrefs.SetString(vector4ArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Vector4 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Vector4[] GetVector4Array(string key, Vector4[] defaultValue = default(Vector4[]))
    {
        List<Vector4> results = new List<Vector4>();
        string[] dataSplit = PlayerPrefs.GetString(vector4ArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Vector4(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Vector4[] GetEncryptedVector4Array(string key, Vector4[] defaultValue = default(Vector4[]))
    {
        string data = DecryptData(key, vector4ArrayPrefix);
        List<Vector4> results = new List<Vector4>();

        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Vector4(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Quaternion]
    /// <summary>
    /// Save a Quaternion value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetQuaternion(string key, Quaternion value, bool encrypt = false)
    {
        string data = (value.x + "," + value.y + "," + value.z + "," + value.w);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), quaternionValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(quaternionValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Quaternion value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Quaternion</returns>
    public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default(Quaternion))
    {
        string data = PlayerPrefs.GetString(quaternionValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Quaternion(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    public static Quaternion GetEncryptedQuaternion(string key, Quaternion defaultValue = default(Quaternion))
    {
        string data = DecryptData(key, quaternionValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Quaternion(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Quaternion values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetQuaternionArray(string key, Quaternion[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Quaternion value in values)
        {
            data += value.x + "," + value.y + "," + value.z + "," + value.w;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), quaternionArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(quaternionArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Quaternion array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Quaternion[] GetQuaternionArray(string key, Quaternion[] defaultValue = default(Quaternion[]))
    {
        List<Quaternion> results = new List<Quaternion>();
        string[] dataSplit = PlayerPrefs.GetString(quaternionArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Quaternion(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Quaternion[] GetEncryptedQuaternionArray(string key, Quaternion[] defaultValue = default(Quaternion[]))
    {
        string data = DecryptData(key, quaternionArrayPrefix);
        List<Quaternion> results = new List<Quaternion>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Quaternion(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
#endregion
    #region [Matrix4x4]
    /// <summary>
    /// Save a Matrix4x4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetMatrix4x4(string key, Matrix4x4 value, bool encrypt = false)
    {
        string data = (value.m00 + "," + value.m01 + "," + value.m02 + "," + value.m03 + "," + value.m10 + "," + value.m11 + "," + value.m12 + "," + value.m13 + "," + value.m20 + "," + value.m21 + "," + value.m22 + "," + value.m23 + "," + value.m30 + "," + value.m31 + "," + value.m32 + "," + value.m33);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), matrix4x4ValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(matrix4x4ValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Matrix4x4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Matrix4x4</returns>
    public static Matrix4x4 GetMatrix4x4(string key, Matrix4x4 defaultValue = default(Matrix4x4))
    {
        string data = PlayerPrefs.GetString(matrix4x4ValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0)
        {
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3])));
            matrix.SetRow(1, new Vector4(float.Parse(dataSplit[4]), float.Parse(dataSplit[5]), float.Parse(dataSplit[6]), float.Parse(dataSplit[7])));
            matrix.SetRow(2, new Vector4(float.Parse(dataSplit[8]), float.Parse(dataSplit[9]), float.Parse(dataSplit[10]), float.Parse(dataSplit[11])));
            matrix.SetRow(3, new Vector4(float.Parse(dataSplit[12]), float.Parse(dataSplit[13]), float.Parse(dataSplit[14]), float.Parse(dataSplit[15])));

            return matrix;
        }
        else return defaultValue;
    }

    public static Matrix4x4 GetEncryptedMatrix4x4(string key, Matrix4x4 defaultValue = default(Matrix4x4))
    {
        string data = DecryptData(key, matrix4x4ValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            Matrix4x4 matrix = new Matrix4x4();
            matrix.SetRow(0, new Vector4(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3])));
            matrix.SetRow(1, new Vector4(float.Parse(dataSplit[4]), float.Parse(dataSplit[5]), float.Parse(dataSplit[6]), float.Parse(dataSplit[7])));
            matrix.SetRow(2, new Vector4(float.Parse(dataSplit[8]), float.Parse(dataSplit[9]), float.Parse(dataSplit[10]), float.Parse(dataSplit[11])));
            matrix.SetRow(3, new Vector4(float.Parse(dataSplit[12]), float.Parse(dataSplit[13]), float.Parse(dataSplit[14]), float.Parse(dataSplit[15])));
            return matrix;
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Matrix4x4 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetMatrix4x4Array(string key, Matrix4x4[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Matrix4x4 value in values)
        {
            data += value.m00 + "," + value.m01 + "," + value.m02 + "," + value.m03 + "," + value.m10 + "," + value.m11 + "," + value.m12 + "," + value.m13 + "," + value.m20 + "," + value.m21 + "," + value.m22 + "," + value.m23 + "," + value.m30 + "," + value.m31 + "," + value.m32 + "," + value.m33;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), matrix4x4ArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(matrix4x4ArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Matrix4x4 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Matrix4x4[] GetMatrix4x4Array(string key, Matrix4x4[] defaultValue = default(Matrix4x4[]))
    {
        List<Matrix4x4> results = new List<Matrix4x4>();
        string[] dataSplit = PlayerPrefs.GetString(matrix4x4ArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    Matrix4x4 matrix = new Matrix4x4();
                    matrix.SetRow(0, new Vector4(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                    matrix.SetRow(1, new Vector4(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6]), float.Parse(split[7])));
                    matrix.SetRow(2, new Vector4(float.Parse(split[8]), float.Parse(split[9]), float.Parse(split[10]), float.Parse(split[11])));
                    matrix.SetRow(3, new Vector4(float.Parse(split[12]), float.Parse(split[13]), float.Parse(split[14]), float.Parse(split[15])));
                    results.Add(matrix);
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Matrix4x4[] GetEncryptedMatrix4x4Array(string key, Matrix4x4[] defaultValue = default(Matrix4x4[]))
    {
        string data = DecryptData(key, matrix4x4ArrayPrefix);
        List<Matrix4x4> results = new List<Matrix4x4>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    Matrix4x4 matrix = new Matrix4x4();
                    matrix.SetRow(0, new Vector4(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                    matrix.SetRow(1, new Vector4(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6]), float.Parse(split[7])));
                    matrix.SetRow(2, new Vector4(float.Parse(split[8]), float.Parse(split[9]), float.Parse(split[10]), float.Parse(split[11])));
                    matrix.SetRow(3, new Vector4(float.Parse(split[12]), float.Parse(split[13]), float.Parse(split[14]), float.Parse(split[15])));
                    results.Add(matrix);
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
    #endregion
    #region [Rect]
    /// <summary>
    /// Save a Rect value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetRect(string key, Rect value, bool encrypt = false)
    {
        string data = (value.x + "," + value.y + "," + value.width + "," + value.height);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), rectValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(rectValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Rect value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Rect</returns>
    public static Rect GetRect(string key, Rect defaultValue = default(Rect))
    {
        string data = PlayerPrefs.GetString(rectValuePrefix + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Rect(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    public static Rect GetEncryptedRect(string key, Rect defaultValue = default(Rect))
    {
        string data = DecryptData(key, rectValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            return new Rect(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        }
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Rect values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetRectArray(string key, Rect[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Rect value in values)
        {
            data += value.x + "," + value.y + "," + value.width + "," + value.height;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), rectArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(rectArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Rect array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Rect[] GetRectArray(string key, Rect[] defaultValue = default(Rect[]))
    {
        List<Rect> results = new List<Rect>();
        string[] dataSplit = PlayerPrefs.GetString(rectArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {

                    string[] split = data.Split(',');
                    results.Add(new Rect(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }

    public static Rect[] GetEncryptedRectArray(string key, Rect[] defaultValue = default(Rect[]))
    {
        string data = DecryptData(key, rectArrayPrefix);
        List<Rect> results = new List<Rect>();

        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Rect(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                }
            }
            return results.ToArray();
        }
        else return defaultValue;
    }
    #endregion
    #region [Bounds]
    /// <summary>
    /// Save a Bounds value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBounds(string key, Bounds value, bool encrypt = false)
    {
        string data = (value.center.x + "," + value.center.y + "," + value.center.z + "," + value.extents.x + "," + value.extents.y + "," + value.extents.z);
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), boundsValuePrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(boundsValuePrefix + key, data);
    }

    /// <summary>
    /// Get a Bounds value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Bounds GetBounds(string key, Bounds defaultValue = default(Bounds))
    {
        string data = PlayerPrefs.GetString(boundsValuePrefix + key);

        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0)
        {
            Vector3 center = new Vector3(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]));
            Vector3 extends = new Vector3(float.Parse(dataSplit[3]), float.Parse(dataSplit[4]), float.Parse(dataSplit[5]));
            return new Bounds(center, extends * 2);
        }
        else return defaultValue;
    }

    public static Bounds GetEncryptedBounds(string key, Bounds defaultValue = default(Bounds))
    {
        string data = DecryptData(key, boundsValuePrefix);
        if (!string.IsNullOrEmpty(data)) {
            string[] dataSplit = data.Split(',');
            Vector3 center = new Vector3(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]));
            Vector3 extends = new Vector3(float.Parse(dataSplit[3]), float.Parse(dataSplit[4]), float.Parse(dataSplit[5]));
            return new Bounds(center, extends * 2);
        }
        else return defaultValue;
    }

    /// <summary>
    /// Save an array of Bounds values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetBoundsArray(string key, Bounds[] values, bool encrypt = false)
    {
        string data = string.Empty;
        foreach (Bounds value in values)
        {
            data += value.center.x + "," + value.center.y + "," + value.center.z + "," + value.extents.x + "," + value.extents.y + "," + value.extents.z;
            data += "|";
        }
        if(encrypt) PlayerPrefs.SetString(KEY_PREFIX + SimpleEncryption.EncryptString(key), boundsArrayPrefix + SimpleEncryption.EncryptString(data));
        else PlayerPrefs.SetString(boundsArrayPrefix + key, data);
    }

    /// <summary>
    /// Get a Bounds array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Bounds[] GetBoundsArray(string key, Bounds[] defaultValue = default(Bounds[]))
    {
        List<Bounds> results = new List<Bounds>();
        string[] dataSplit = PlayerPrefs.GetString(boundsArrayPrefix + key).Split('|');
        if (dataSplit.Length > 0)
        {
            foreach (string data in dataSplit)
            {
                if (data != string.Empty)
                {
                    string[] split = data.Split(',');
                    results.Add(new Bounds(new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2])), new Vector3(float.Parse(split[3]), float.Parse(split[4]), float.Parse(split[5]))));
                }
            }
            return results.ToArray();
        }
        return defaultValue;
    }

    public static Bounds[] GetEncryptedBoundsArray(string key, Bounds[] defaultValue = default(Bounds[]))
    {
        string data = DecryptData(key, boundsArrayPrefix);
        List<Bounds> results = new List<Bounds>();
        if (!string.IsNullOrEmpty(data))
        {
            string[] dataSplit = data.Split('|');
            foreach (string _data in dataSplit)
            {
                if (_data != string.Empty)
                {
                    string[] split = _data.Split(',');
                    results.Add(new Bounds(new Vector3(float.Parse(split[0]), float.Parse(split[1]), float.Parse(split[2])), new Vector3(float.Parse(split[3]), float.Parse(split[4]), float.Parse(split[5]))));
                }
            }
            return results.ToArray();
        }
        return defaultValue;
    }
    #endregion
    
    //-----------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Helper method to store an enum value in PlayerPrefs (stored using the string name of the enum)
    /// </summary>
    public static void SetEnum(string key, Enum value)
	{
		PlayerPrefs.SetString(key, value.ToString());// Convert the enum value to its string name (as opposed to integer index) and store it in a string PlayerPref
	}

    /// <summary>
    /// Generic helper method to retrieve an enum value from PlayerPrefs and parse it from its stored string into the 
    /// specified generic type. This method should generally be preferred over the non-generic equivalent
    /// </summary>
    public static T GetEnum<T>(string key, T defaultValue = default(T)) where T : struct
    {
        string stringValue = PlayerPrefs.GetString(key);// Fetch the string value from PlayerPrefs
                                                        // Existing value, so parse it using the supplied generic type and cast before returning it
                                                        // No player pref for this, just return default. If no default is supplied this will be the enum's default

        return !string.IsNullOrEmpty(stringValue) ? (T)Enum.Parse(typeof(T), stringValue) : defaultValue;
    }

	/// <summary>
	/// Non-generic helper method to retrieve an enum value from PlayerPrefs (stored as a string). Default value must be
	/// passed, passing null will mean you need to do a null check where you call this method. Generally try to use the
	/// generic version of this method instead: GetEnum<T>
	/// </summary>
	public static object GetEnum(string key, Type enumType, object defaultValue)
	{
		// Fetch the string value from PlayerPrefs
		string value = PlayerPrefs.GetString (key);
        // Existing value, parse it using the supplied type, then return the result as an object
        // No player pref for this key, so just return supplied default. It's required to supply a default value,
        // you can just pass null, but you would then need to do a null check where you call non-generic GetEnum().
        // Consider using GetEnum<T>() which doesn't require a default to be passed (supplying default(T) instead)

        return !string.IsNullOrEmpty(value)? Enum.Parse(enumType, value):defaultValue;
	}

	/// <summary>
	/// Helper method to store a DateTime (complete with its timezone) in PlayerPrefs as a string
	/// </summary>
	public static void SetDateTime(string key, DateTime value)
	{
		// Convert to an ISO 8601 compliant string ("o"), so that it's fully qualified, then store in PlayerPrefs
		PlayerPrefs.SetString(key, value.ToString("o", CultureInfo.InvariantCulture));
	}

	/// <summary>
	/// Helper method to retrieve a DateTime from PlayerPrefs (stored as a string) and return a DateTime complete with
	/// timezone (works with UTC and local DateTimes)
	/// </summary>
	public static DateTime GetDateTime(string key, DateTime defaultValue = new DateTime())
	{
		// Fetch the string value from PlayerPrefs
		string stringValue = PlayerPrefs.GetString(key);
// Make sure to parse it using Roundtrip Kind otherwise a local time would come out as UTC
// No existing player pref value, so return defaultValue instead

        return !string.IsNullOrEmpty(stringValue)? DateTime.Parse(stringValue, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind):defaultValue;
	}

	/// <summary>
	/// Helper method to store a TimeSpan in PlayerPrefs as a string
	/// </summary>
	public static void SetTimeSpan(string key, TimeSpan value)
	{
		// Use the TimeSpan's ToString() method to encode it as a string which is then stored in PlayerPrefs
		PlayerPrefs.SetString(key, value.ToString());
	}

    /// <summary>
    /// Helper method to retrieve a TimeSpan from PlayerPrefs (stored as a string)
    /// </summary>
    public static TimeSpan GetTimeSpan(string key, TimeSpan defaultValue = new TimeSpan())
    {
        // Fetch the string value from PlayerPrefs
        string stringValue = PlayerPrefs.GetString(key);
        // Parse the string and return the TimeSpan
        // No existing player pref value, so return defaultValue instead

        return !string.IsNullOrEmpty(stringValue) ? TimeSpan.Parse(stringValue) : defaultValue;
    }
}
