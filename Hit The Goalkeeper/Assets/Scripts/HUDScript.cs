using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Text pointText;
    public Slider hpSlider;
    // Start is called before the first frame update

    public void SetHUD(Unit unit)
    {
        pointText.text = "Lvl " + unit.point;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

    }
    public void SetHP(int hp)
    {
      
     hpSlider.value = hp;

    }
}
