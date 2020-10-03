using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{
   
   public static void loadStats(statsToSave savedStats, Stats stats)
    {

        stats.Damage = savedStats.Damage;
           stats.damageResistanceInPercent = savedStats.damageResistanceInPercent;
             stats.manna = savedStats.manna;
             stats.MannaEarnPerSecond = savedStats.MannaEarnPerSecond;
             stats.MaxHealth = savedStats.MaxHealth;
             stats.MaxManna = savedStats.MaxManna;
             stats.Speed = savedStats.Speed;
    }
    public static statsToSave CreateStructStats(Stats stats)
    {
        statsToSave statsToSave = new statsToSave();
        statsToSave.Damage = stats.Damage;
        statsToSave.damageResistanceInPercent = stats.damageResistanceInPercent;
        statsToSave.manna = stats.manna;
        statsToSave.MannaEarnPerSecond = stats.MannaEarnPerSecond;
        statsToSave.MaxHealth = stats.MaxHealth;
        statsToSave.MaxManna = stats.MaxManna;
        statsToSave.Speed = stats.Speed;
        return statsToSave;
    }
}
