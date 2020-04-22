using System;
using System.Collections;
using Accessables;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GUI2
{
    public class HUDScript : MonoBehaviour
    {
        public TextMeshProUGUI pointText, damageText;

        [SerializeField] private Slider hpSlider, hpSliderBack;

        private DOTweenAnimation damageTextAnim;
        private void Start()
        {
            damageTextAnim = damageText.GetComponent<DOTweenAnimation>();
        }

        public void SetHud(Unit unit)
        {
            pointText.text = unit.maxHP.ToString();
            hpSlider.maxValue = unit.maxHP;
            hpSlider.value = unit.maxHP;
            hpSliderBack.maxValue = unit.maxHP;
            hpSliderBack.value = unit.maxHP;
        }

        public void SetHp(int hp)
        {
            hpSlider.value -= hp;
            pointText.text = hpSlider.value.ToString();
            DOTween.To(() => hpSliderBack.value, newValue => hpSliderBack.value = newValue, hpSliderBack.value - hp, 1.75f);
        }

        public void SetDamage(int damage)
        {
            damageText.enabled = true;
            damageText.text = "-" + damage;
            damageTextAnim.tween.Play();
            damageTextAnim.tween.SetAutoKill(false);
        }

        public void TextMeshDisable()
        {
            damageText.enabled = false;
            damageTextAnim.DORewind();
        }
    }
}