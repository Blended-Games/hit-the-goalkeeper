using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    #region Singleton

    public static Unit main;

    private void Awake()
    {
        main = this;
    }

    #endregion

    public string UnitName;
    public int point;

    public int damage;

    // değiştirilen değer
    public int currentHP;
    public int maxHP ;
    
    #region  SetHp
    private void Start()
     {
     if (gameObject.tag == "goalkeeper" &&PlayerPrefs.GetInt("highlevel") ==0)
     {
         maxHP = 70;
         PlayerPrefs.SetInt("maxHP",maxHP);
     }
      else if (gameObject.tag == "Player")
     {
         maxHP=100;
     }

    if (gameObject.tag == "goalkeeper" && PlayerPrefs.GetInt("highlevel") > 0)
     {
       maxHP =PlayerPrefs.GetInt("maxHP")+((PlayerPrefs.GetInt("maxHP") * 10) / 100);
       
        PlayerPrefs.SetInt("maxHP",maxHP);
     }

   }
   #endregion
    public bool TakeDamage(int dmg)
    {
        maxHP -= dmg;

        return maxHP <= 0;
    }
}
