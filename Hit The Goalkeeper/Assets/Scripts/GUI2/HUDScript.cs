using UnityEngine;
using UnityEngine.UI;
using Accessables;
 using TMPro;
 using System;
 using System.Collections;


    public class HUDScript : MonoBehaviour
    {
       public TextMeshProUGUI pointText;

        [SerializeField]
        private Slider hpSlider, hpSliderBack;
        // Start is called before the first frame update

    public void Start()
    {
       
    }
       public void SetHud(Unit unit)
        {
            pointText.text=unit.maxHP.ToString();
            hpSlider.maxValue = unit.maxHP;
            hpSlider.value = unit.maxHP;
           hpSliderBack.maxValue = unit.maxHP;
           hpSliderBack.value = unit.maxHP;
            
        }

       public IEnumerator SetHp(int hp)
        {
            
            
            hpSlider.value -=hp;
            yield  return  new WaitForSeconds(.1f);
             pointText.text=hpSlider.value.ToString();
            yield  return  new WaitForSeconds(.2f);
            hpSliderBack.value-=hp;          
         
        }
    }