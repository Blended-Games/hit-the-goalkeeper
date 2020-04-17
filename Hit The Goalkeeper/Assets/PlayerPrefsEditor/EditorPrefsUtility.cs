using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Esta es una clase que aumenta los metodos para la clase EditorPrefs
/// EditorPrefsExtension es una clase estatica, asi que puedes llamar los metodos desde cualquier lugar 
/// cada vez que lo necesites
/// 
/// </summary>
public static class EditorPrefsUtility
{

    /// <summary>
    /// Check if exist a key
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool HasKey(string key)
    {
        string[] types = { "{0}", ":bool:{0}", ":Color:{0}", ":Color32:{0}", ":Vector2:{0}", ":Vector3:{0}", ":Vector4:{0}", ":Quaternion:{0}", ":Matrix4x4:{0}", ":Rect:{0}", ":Bounds:{0}", ":int[]:{0}", ":float[]:{0}", ":bool[]:{0}", ":Color[]:{0}", ":Color32[]:{0}", ":Vector2[]:{0}", ":Vector3[]:{0}", ":Vector4[]:{0}", ":Quaternion[]:{0}", ":Matrix4x4[]:{0}", ":Rect[]:{0}", ":Bounds[]:{0}" };
        bool flag = false;
        foreach (string type in types)
        {
            if (EditorPrefs.HasKey(string.Format(type, key)))
                flag = true;
        }
        return flag;
    }
    #region [Bool]
    /// <summary>
    /// Save a boolean value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBool(string key, bool value)
    {
        EditorPrefs.SetInt(":bool:" + key, value ? 1 : 0);
    }

    /// <summary>
    /// Get a bool value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static bool GetBool(string key, bool defaultValue)
    {
        if (EditorPrefs.GetInt(":bool:" + key) == 1) return true;
        else if (EditorPrefs.GetInt(":bool:" + key) == 0) return false;
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
            data += (value == true) ? 1.ToString() : 0.ToString();
            data += "|";
        }
        EditorPrefs.SetString(":bool[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":bool[]:" + key).Split('|');
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
    #endregion
    #region [Color]
    /// <summary>
    /// Save a Color value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetColor(string key, Color value)
    {
        string data = value.r + "," + value.g + "," + value.b + "," + value.a;
        EditorPrefs.SetString(":Color:" + key, data);
    }

    /// <summary>
    /// Get a Color value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Color GetColor(string key, Color defaultValue = default(Color))
    {
        string data = EditorPrefs.GetString(":Color:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Color(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    /// <summary>
    /// Save a Color array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetColorArray(string key, Color[] values)
    {
        string data = string.Empty;
        foreach (Color value in values)
        {
            data += value.r + "," + value.g + "," + value.b + "," + value.a;
            data += "|";
        }
        EditorPrefs.SetString(":Color[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Color[]:" + key).Split('|');

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
    #endregion
    #region [Color32]
    /// <summary>
    /// Save a Color32 value 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetColor32(string key, Color32 value)
    {
        string data = (value.r + "," + value.g + "," + value.b + "," + value.a);
        EditorPrefs.SetString(":Color32:" + key, data);
    }

    /// <summary>
    /// Get a Color32 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultKey"></param>
    /// <returns></returns>
    public static Color32 GetColor32(string key, Color32 defaultKey = default(Color32))
    {
        string data = EditorPrefs.GetString(":Color32:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Color32(byte.Parse(dataSplit[0]), byte.Parse(dataSplit[1]), byte.Parse(dataSplit[2]), byte.Parse(dataSplit[3]));
        else return defaultKey;
    }

    /// <summary>
    /// Save an array of Color32 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetColor32Array(string key, Color32[] values)
    {
        string data = string.Empty;
        foreach (Color32 value in values)
        {
            data += value.r + "," + value.g + "," + value.b + "," + value.a;
            data += "|";
        }
        EditorPrefs.SetString(":Color32[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Color32[]:" + key).Split('|');
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
    #endregion
    #region [Vector2]

    /// <summary>
    /// Save a Vector2 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector2(string key, Vector2 value)
    {
        string data = (value.x + "," + value.y);
        EditorPrefs.SetString(":Vector2:" + key, data);
    }


    /// <summary>
    /// Get a Vector2 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector2</returns>
    public static Vector2 GetVector2(string key, Vector2 defaultValue = default(Vector2))
    {
        string data = EditorPrefs.GetString(":Vector2:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector2(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]));
        else return defaultValue;
    }


    /// <summary>
    /// Save a Vector2 array
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector2Array(string key, Vector2[] values)
    {
        string data = string.Empty;
        foreach (Vector2 value in values)
        {
            data += value.x + "," + value.y;
            data += "|";
        }
        EditorPrefs.SetString(":Vector2[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Vector2[]:" + key).Split('|');
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
    #endregion
    #region [Vector3]

    /// <summary>
    /// Save a Vector3 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector3(string key, Vector3 value)
    {
        string data = (value.x + "," + value.y + "," + value.z);
        EditorPrefs.SetString(":Vector3:" + key, data);
    }

    /// <summary>
    /// Get a Vector3 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector3</returns>
    public static Vector3 GetVector3(string key, Vector3 defaultValue = default(Vector3))
    {
        string data = EditorPrefs.GetString(":Vector3:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector3(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]));
        else return defaultValue;
    }

    /// <summary>
    /// Save an array of Vector3 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector3Array(string key, Vector3[] values)
    {
        string data = string.Empty;
        foreach (Vector3 value in values)
        {
            data += value.x + "," + value.y + "," + value.z;
            data += "|";
        }
        EditorPrefs.SetString(":Vector3[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Vector3[]:" + key).Split('|');
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
    #endregion
    #region [Vector4]

    /// <summary>
    /// Save a vector4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetVector4(string key, Vector4 value)
    {
        string data = (value.x + "," + value.y + "," + value.z + "," + value.w);
        EditorPrefs.SetString(":Vector4:" + key, data);
    }

    /// <summary>
    /// Get a Vector4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Vector4</returns>
    public static Vector4 GetVector4(string key, Vector4 defaultValue = default(Vector4))
    {
        string data = EditorPrefs.GetString(":Vector4:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Vector4(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }

    /// <summary>
    /// Save an array of Vector4 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetVector4Array(string key, Vector4[] values)
    {
        string data = string.Empty;
        foreach (Vector4 value in values)
        {
            data += value.x + "," + value.y + "," + value.z + "," + value.w;
            data += "|";
        }
        EditorPrefs.SetString(":Vector4[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Vector4[]:" + key).Split('|');
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
    #endregion
    #region [Quaternion]

    /// <summary>
    /// Save a Quaternion value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetQuaternion(string key, Quaternion value)
    {
        string data = (value.x + "," + value.y + "," + value.z + "," + value.w);
        EditorPrefs.SetString(":Quaternion:" + key, data);
    }

    /// <summary>
    /// Get a Quaternion value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Quaternion</returns>
    public static Quaternion GetQuaternion(string key, Quaternion defaultValue = default(Quaternion))
    {
        string data = EditorPrefs.GetString(":Quaternion:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Quaternion(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }


    /// <summary>
    /// Save an array of Quaternion values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetQuaternionArray(string key, Quaternion[] values)
    {
        string data = string.Empty;
        foreach (Quaternion value in values)
        {
            data += value.x + "," + value.y + "," + value.z + "," + value.w;
            data += "|";
        }
        EditorPrefs.SetString(":Quaternion[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Quaternion[]:" + key).Split('|');
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
    #endregion
    #region [Matrix4x4]

    /// <summary>
    /// Save a Matrix4x4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetMatrix4x4(string key, Matrix4x4 value)
    {
        string data = (value.m00 + "," + value.m01 + "," + value.m02 + "," + value.m03 + "," + value.m10 + "," + value.m11 + "," + value.m12 + "," + value.m13 + "," + value.m20 + "," + value.m21 + "," + value.m22 + "," + value.m23 + "," + value.m30 + "," + value.m31 + "," + value.m32 + "," + value.m33);
        EditorPrefs.SetString(":Matrix4x4:" + key, data);
    }

    /// <summary>
    /// Get a Matrix4x4 value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Matrix4x4</returns>
    public static Matrix4x4 GetMatrix4x4(string key, Matrix4x4 defaultValue = default(Matrix4x4))
    {
        string data = EditorPrefs.GetString(":Matrix4x4:" + key);
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

    /// <summary>
    /// Save an array of Matrix4x4 values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetMatrix4x4Array(string key, Matrix4x4[] values)
    {
        string data = string.Empty;
        foreach (Matrix4x4 value in values)
        {
            data += value.m00 + "," + value.m01 + "," + value.m02 + "," + value.m03 + "," + value.m10 + "," + value.m11 + "," + value.m12 + "," + value.m13 + "," + value.m20 + "," + value.m21 + "," + value.m22 + "," + value.m23 + "," + value.m30 + "," + value.m31 + "," + value.m32 + "," + value.m33;
            data += "|";
        }
        EditorPrefs.SetString(":Matrix4x4[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Matrix4x4[]:" + key).Split('|');
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
    #endregion
    #region [Rect]

    /// <summary>
    /// Save a Rect value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetRect(string key, Rect value)
    {
        string data = (value.x + "," + value.y + "," + value.width + "," + value.height);
        EditorPrefs.SetString(":Rect:" + key, data);
    }

    /// <summary>
    /// Get a Rect value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns>Rect</returns>
    public static Rect GetRect(string key, Rect defaultValue = default(Rect))
    {
        string data = EditorPrefs.GetString(":Rect:" + key);
        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0) return new Rect(float.Parse(dataSplit[0]), float.Parse(dataSplit[1]), float.Parse(dataSplit[2]), float.Parse(dataSplit[3]));
        else return defaultValue;
    }
    /// <summary>
    /// Save an array of Rect values
    /// </summary>
    /// <param name="key"></param>
    /// <param name="values"></param>
    public static void SetRectArray(string key, Rect[] values)
    {
        string data = string.Empty;
        foreach (Rect value in values)
        {
            data += value.x + "," + value.y + "," + value.width + "," + value.height;
            data += "|";
        }
        EditorPrefs.SetString(":Rect[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Rect[]:" + key).Split('|');
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
    #endregion
    #region [Bounds]

    /// <summary>
    /// Save a Bounds value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void SetBounds(string key, Bounds value)
    {
        string data = (value.center.x + "," + value.center.y + "," + value.center.z + "," + value.extents.x + "," + value.extents.y + "," + value.extents.z);
        EditorPrefs.SetString(":Bounds:" + key, data);
    }

    /// <summary>
    /// Get a Bounds value
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static Bounds GetBounds(string key, Bounds defaultValue = default(Bounds))
    {
        string data = EditorPrefs.GetString(":Bounds:" + key);

        string[] dataSplit = data.Split(',');
        if (dataSplit.Length > 0)
        {
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
    public static void SetBoundsArray(string key, Bounds[] values)
    {
        string data = string.Empty;
        foreach (Bounds value in values)
        {
            data += value.center.x + "," + value.center.y + "," + value.center.z + "," + value.extents.x + "," + value.extents.y + "," + value.extents.z;
            data += "|";
        }
        EditorPrefs.SetString(":Bounds[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":Bounds[]:" + key).Split('|');
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
    #endregion
    #region [Int, String,Float Array]

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
        EditorPrefs.SetString(":int[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":int[]:" + key).Split('|');
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
    
    //----------------------------------------------------------------------------------------------------------------------------------------------------------------

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
        EditorPrefs.SetString(":string[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":string[]:" + key).Split('|');
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

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

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
        EditorPrefs.SetString(":float[]:" + key, data);
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
        string[] dataSplit = EditorPrefs.GetString(":float[]:" + key).Split('|');
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
    #endregion
}
