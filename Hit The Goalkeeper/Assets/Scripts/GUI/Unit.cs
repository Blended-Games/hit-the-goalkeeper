using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
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