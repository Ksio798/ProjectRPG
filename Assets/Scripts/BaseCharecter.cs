using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharecter : MonoBehaviour
{
    protected float health;
    public float Health { get { return health; } }
    public Stats stats;
    
    public bool CanMove = true;
    
    protected virtual void Start()
    {
        health = stats.MaxHealth;
        StartCoroutine(WaitToMannaRegen());
    }

   
    public virtual void addHealth(float amount)
    {
        if (stats!=null)
        {

        if (health + amount <= stats.MaxHealth)
        {
            health += amount;
        }
        else
            health = stats.MaxHealth;
        }
    }
    public virtual void TakeDamage(float Dmg)
    {
        float a = Dmg;
        if(stats!=null)
        Dmg -= Mathematics.GetPercent(stats.damageResistanceInPercent, a);
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
 IEnumerator WaitToMannaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (stats.manna<stats.MaxHealth)
            {
                stats.manna += stats.MannaEarnPerSecond;
                if (stats.manna > stats.MaxHealth)
                    stats.manna = stats.MaxHealth;
            }
           // Debug.Log(stats.manna);
        }
    }
 
}
