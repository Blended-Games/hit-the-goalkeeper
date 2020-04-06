using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //  #region Singleton
    // public static Unit instance;
    // private void Awake()
    // {
    //     if (instance != null && instance != this)
    //     {
    //         Destroy(gameObject);
    //         return;
    //     }
    //     instance = this;
    // }

    // #endregion
   public string UnitName;
   public int point;
   public int damage;
   // değiştirilen değer
   public int currentHP;
   public int maxHP;
   public bool TakeDamage(int dmg){
     
      currentHP-=dmg;

      if(currentHP<=0){
         return true;
      }
      else 
         return false;
    }
}
