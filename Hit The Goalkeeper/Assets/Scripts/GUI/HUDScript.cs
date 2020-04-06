using UnityEngine;
using UnityEngine.UI;

namespace GUI
{
    public class HudScript : MonoBehaviour
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
      
            hpSlider.value = hp;

        }
    }
}
