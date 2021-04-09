using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
	public Text healthText;
    public Text damageText;
	public Text moneyText;

	public int healthLimit;
	public int damageLimit;

	public int health;
    public int damage;
	public int money;
    // Start is called before the first frame update
    void Start()
    {
	    healthLimit = 50;
	    damageLimit = 100;
        health = 30;
		damage = 10;
		money = 100;
		updatePlayerStatus();
    }

    // Update is called once per frame
    public void updatePlayerStatus()
    {
        healthText.text = health.ToString()+"/"+ healthLimit.ToString();
		damageText.text = damage.ToString()+"/"+ damageLimit.ToString();
		moneyText.text = money.ToString();
    }

	public bool addHealth(int value)
	{
		if(health == healthLimit)
		{
			return false;
		}
		else
		{
			health = Math.Min(health+value, healthLimit);
			updatePlayerStatus();
			return true;
		}
	}

	public bool addDamage(int value)
	{
		if(damage == damageLimit)
		{
			return false;
		}
		else
		{
			damage = Math.Min(damage+value, damageLimit);
			updatePlayerStatus();
			return true;
		}
	}

	public bool useMoney(int value)
	{
		if(value > money)
		{
			return false;
		}
		else
		{
			money = money - value;
			updatePlayerStatus();
			return true;
		}
	}

	public void treatment()
	{
		if (useMoney(20))
		{
			addHealth(10);
		}
	}

	public void upgrade()
	{
		if (useMoney(50))
		{
			addDamage(5);
		}
	}
}