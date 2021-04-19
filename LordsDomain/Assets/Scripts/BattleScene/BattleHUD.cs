using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
    public Slider hpSlider;

	public void SetHUD(PlayerStatus unit)
	{
		nameText.text = unit.name;
		hpSlider.maxValue = unit.healthLimit;
		hpSlider.value = unit.health;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
