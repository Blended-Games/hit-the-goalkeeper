using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


    public class HUDScript : MonoBehaviour
    {
        public Text pointText;
        public Slider hpSlider;
        // Start is called before the first frame update
        
        public void SetHud(Unit unit)
        {
            pointText.text = "Lvl " + unit.point;
            hpSlider.maxValue = unit.maxHP;
            hpSlider.value = unit.currentHP;

        }
        
        public void SetHp(int hp)
        {
      
            hpSlider.value -= hp;

        }
    }
