using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
	public string currentTime;
	public string name;
	public Text healthText;
    public Text damageText;
	public Text moneyText;

	public int healthLimit;
	public int damageLimit;

	public int health;
    public int damage;
	public int money;

	public int maxHealth = 0;
	public int maxDamage = 0;
	public int maxMoney = 0;
	public int maxPoints = 0;
    // Start is called before the first frame update
    void Start()
    {
		if (!LoadPlayerStatus ())
		{
			if (StaticVariables.playerName == null)
			{
				StaticVariables.playerName = "Stranger";
			}
			name = StaticVariables.playerName;
	    	healthLimit = 50;
	    	damageLimit = 100;
        	health = 30;
			damage = 10;
			money = 100;
		}
		updatePlayerStatus();
    }

    // Update is called once per frame
    public void updatePlayerStatus()
    {
        healthText.text = health.ToString()+"/"+ healthLimit.ToString();
		damageText.text = damage.ToString()+"/"+ damageLimit.ToString();
		moneyText.text = money.ToString();
		maxHealth = Math.Max(health, maxHealth);
		maxDamage = Math.Max(damage, maxDamage);
		maxMoney = Math.Max(money, maxMoney);
		maxPoints = Math.Max(maxHealth*3+maxDamage*5+maxMoney*2, maxPoints);
		currentTime = System.DateTime.Now.ToString();
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

	public void SavePlayerStatus ()
	{
		SaveSystem.SavePlayerStatus(this);
	}

	public bool LoadPlayerStatus ()
	{
		PlayerData data = SaveSystem.LoadPlayerStatus();
		if (data != null)
		{
			name = data.name;
        	healthLimit = data.healthLimit;
        	damageLimit = data.damageLimit;
        	health = data.health;
        	damage = data.damage;
        	money = data.money;
        	maxHealth = data.maxHealth;
        	maxDamage = data.maxDamage;
        	maxMoney = data.maxMoney;
        	maxPoints = data.maxPoints;
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool TakeDamage(int dmg) {
        health -= dmg;

        if (health <= 0)
            return true;
        else
            return false;
    }
}