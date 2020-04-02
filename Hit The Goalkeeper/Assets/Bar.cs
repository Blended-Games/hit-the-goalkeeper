using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bar : MonoBehaviour
{
    #region Singleton

    public static Bar instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    #endregion 
   [ Header ("UnityStuff")]
    public Image heartBar;
    public float heart;
    public float startHealt=100f;
}
