﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharecter : MonoBehaviour
{
    protected float health;
   public float MaxHealth = 5;
    public float damageResistanceInPercent;
    public bool CanMove = true;
   
   public virtual void Start()
    {
        health = 1;
    }


    public virtual void addHealth(float amount)
    {
        if (health + amount <= MaxHealth)
        {
            health += amount;
        }
        else
            health = MaxHealth;
    }
    public virtual void TakeDamage(float Dmg)
    {
        float a = Dmg;
        Dmg -= Mathematics.GetPercent(damageResistanceInPercent, a);
        health -= Dmg;
        
        if (health<=0)
        {
           
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
   
}
