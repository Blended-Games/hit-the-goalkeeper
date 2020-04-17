using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
//using pixel

public class DemoPPE : MonoBehaviour {

    public bool boolean;
    public Color color;
    public Color32 color32;
    public Vector2 vec2;
    public Vector3 vec3;
    public Vector4 vec4;
    public Quaternion quat;
    //_----------------------Poco usados
    public Matrix4x4 matrix;
    public Rect rect;
    public Bounds bounds;

    //-----------------------No aparecen en el inspector
    public bool[] booleanArray;
    public int[] intArray;
    public Color[] colorArray;
    public Color32[] color32Array;
    public Vector2[] vec2Array;
    public Vector3[] vec3Array;
    public Vector4[] vec4Array;
    public Quaternion[] quatArray;
    //_----------------------Poco usados
    public Gradient[] gradArray;
    public AnimationCurve[] curveArray;
    public Matrix4x4[] matrixArray;
    public Rect[] rectArray;
    public Bounds[] boundsArray;

    public void Start() {
        RandomVarriables();
    }

    public void Save () {
        PlayerPrefsUtility.SetBool("bool", boolean);
        PlayerPrefsUtility.SetColor("color", color);
        PlayerPrefsUtility.SetColor32("color32", color32);
        PlayerPrefsUtility.SetVector2("vector2", vec2);
        PlayerPrefsUtility.SetVector3("vector3", vec3);
        PlayerPrefsUtility.SetVector4("vector4", vec4);
        PlayerPrefsUtility.SetQuaternion("quaternion", quat);

        PlayerPrefsUtility.SetMatrix4x4("matrix4x4", matrix);
        PlayerPrefsUtility.SetRect("rect", rect);
        PlayerPrefsUtility.SetBounds("bounds", bounds);

        //------------------------------------------------
        
        PlayerPrefsUtility.SetBoolArray("bool", booleanArray);
        PlayerPrefsUtility.SetIntArray("int", intArray);
        PlayerPrefsUtility.SetColorArray("color", colorArray);
        PlayerPrefsUtility.SetColor32Array("color32", color32Array);
        PlayerPrefsUtility.SetVector2Array("vector2", vec2Array);
        PlayerPrefsUtility.SetVector3Array("vector3", vec3Array);
        PlayerPrefsUtility.SetVector4Array("vector4", vec4Array);
        PlayerPrefsUtility.SetQuaternionArray("quaternion", quatArray);

        PlayerPrefsUtility.SetMatrix4x4Array("matrix4x4", matrixArray);
        PlayerPrefsUtility.SetRectArray("rect", rectArray);
        PlayerPrefsUtility.SetBoundsArray("bounds", boundsArray);
        
    }

    public void Load() {

        boolean = PlayerPrefsUtility.GetBool("bool");
        color = PlayerPrefsUtility.GetColor("color");
        color32 = PlayerPrefsUtility.GetColor32("color32");
        vec2 = PlayerPrefsUtility.GetVector2("vector2");
        vec3 = PlayerPrefsUtility.GetVector3("vector3");
        vec4 = PlayerPrefsUtility.GetVector4("vector4" );
        quat = PlayerPrefsUtility.GetQuaternion("quaternion");

        matrix = PlayerPrefsUtility.GetMatrix4x4("matrix4x4");
        rect = PlayerPrefsUtility.GetRect("rect");
        bounds = PlayerPrefsUtility.GetBounds("bounds");

        //-------------------------------------------------
        
        booleanArray = PlayerPrefsUtility.GetBoolArray("bool");
        intArray = PlayerPrefsUtility.GetIntArray("int");
        colorArray = PlayerPrefsUtility.GetColorArray("color");
        color32Array = PlayerPrefsUtility.GetColor32Array("color32");
        vec2Array = PlayerPrefsUtility.GetVector2Array("vector2");
        vec3Array = PlayerPrefsUtility.GetVector3Array("vector3");
        vec4Array = PlayerPrefsUtility.GetVector4Array("vector4");
        quatArray = PlayerPrefsUtility.GetQuaternionArray("quaternion");

        matrixArray = PlayerPrefsUtility.GetMatrix4x4Array("matrix4x4");
        rectArray = PlayerPrefsUtility.GetRectArray("rect");
        boundsArray = PlayerPrefsUtility.GetBoundsArray("bounds");
        
    }

    public void RandomVarriables()
    {
        boolean = Random.value >= 0.5 ? true : false;
        color32 = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255));
        color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        vec2 = new Vector2(Random.Range(0f, 255f), Random.Range(0f, 255f));
        vec3 = new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        vec4 = new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        quat = new Quaternion(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));

        matrix = new Matrix4x4();
        matrix.SetRow(0, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrix.SetRow(1, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrix.SetRow(2, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrix.SetRow(3, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        rect = new Rect(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f));
        bounds = new Bounds(new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)),new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));

        //--------------------------------------------------------

        booleanArray = new bool[3] { Random.value >= 0.5 ? true : false, Random.value >= 0.5 ? true : false, Random.value >= 0.5 ? true : false };
        intArray = new int[3] { Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3)};
        color32Array = new Color32[3] { new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255)),new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255)),new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255)) };
        colorArray = new Color[3] { new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) };
        vec2Array = new Vector2[3] { new Vector2(Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector2(Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector2(Random.Range(0f, 255f), Random.Range(0f, 255f)) };
        vec3Array = new Vector3[3] { new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)) };
        vec4Array = new Vector4[3] { new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)) };
        quatArray = new Quaternion[3] { new Quaternion(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Quaternion(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Quaternion(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)) };

        matrixArray = new Matrix4x4[3];
        matrixArray[0] = new Matrix4x4();
        matrixArray[0].SetRow(0, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[0].SetRow(1, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[0].SetRow(2, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[0].SetRow(3, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));

        matrixArray[1] = new Matrix4x4();
        matrixArray[1].SetRow(0, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[1].SetRow(1, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[1].SetRow(2, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[1].SetRow(3, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));

        matrixArray[2] = new Matrix4x4();
        matrixArray[2].SetRow(0, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[2].SetRow(1, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[2].SetRow(2, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));
        matrixArray[2].SetRow(3, new Vector4(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)));

        rectArray = new Rect[3] { new Rect(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Rect(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Rect(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)) };
        boundsArray = new Bounds[3] { new Bounds(new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f))), new Bounds(new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f))), new Bounds(new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f)), new Vector3(Random.Range(0f, 255f), Random.Range(0f, 255f), Random.Range(0f, 255f))) };
    }

    public static string[] GetLoadedScenes() {
        List<string> loadedScenes = new List<string>();
        if (SceneManager.sceneCount > 0) {
            for (int i = 0; i < SceneManager.sceneCount; ++i) {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.isLoaded) loadedScenes.Add(scene.name);
            }
        }
        return loadedScenes.ToArray();
    }
}
