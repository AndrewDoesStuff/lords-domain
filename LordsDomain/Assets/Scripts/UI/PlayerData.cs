using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] position;
    public string currentTime;
    public string name;
    public int healthLimit;
    public int damageLimit;

    public int health;
    public int damage;
    public int money;

    public int maxHealth = 0;
    public int maxDamage = 0;
    public int maxMoney = 0;
    public int maxPoints = 0;

    public PlayerData(PlayerStatus ps)
    {
        currentTime = ps.currentTime;
        name = ps.name;
        healthLimit = ps.healthLimit;
        damageLimit = ps.damageLimit;
        health = ps.health;
        damage = ps.damage;
        money = ps.money;
        maxHealth = ps.maxHealth;
        maxDamage = ps.maxDamage;
        maxMoney = ps.maxMoney;
        maxPoints = ps.maxPoints;
        position = new float[3];
        position[0] = ps.transform.position.x;
        position[1] = ps.transform.position.y;
        position[2] = ps.transform.position.z;
    }
}
