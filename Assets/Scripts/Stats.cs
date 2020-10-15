using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float Speed;
    [HideInInspector]
    public int SpeedCountUpdate;
    public float Damage;
    [HideInInspector]
    public int DamageCountUpdate;
    public float MaxHealth;
    [HideInInspector]
    public int HealthCountUpdate;
    public float damageResistanceInPercent;
    [HideInInspector]
    public int damageResistanceInPercentCountUpdate;
    public float manna;
    public float MaxManna;
    public float MannaEarnPerSecond;
    public float health;
    [HideInInspector]
    public int AmmoCountUpdate;
    [HideInInspector]
    public int MedChestCountUpdate;
    public void SetStartHealth()
    {
        if(OneSavePanel.SaveNum == -1)
        health = MaxHealth;
    }

}
