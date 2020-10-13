using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Speed;
    public int SpeedCountUpdate;
    public float Damage;
    public int DamageCountUpdate;
    public float MaxHealth;
    public int HealthCountUpdate;
    public float damageResistanceInPercent;
    public int damageResistanceInPercentCountUpdate;
    public float manna;
    public float MaxManna;
    public float MannaEarnPerSecond;
    public float health;
    public int AmmoCountUpdate;
    public int MedChestCountUpdate;
    public void SetStartHealth()
    {
        if(OneSavePanel.SaveNum == -1)
        health = MaxHealth;
    }

}
