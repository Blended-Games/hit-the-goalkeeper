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
    public int maxHP;

    public bool TakeDamage(int dmg)
    {
        maxHP -= dmg;

        return maxHP <= 0;
    }
}
