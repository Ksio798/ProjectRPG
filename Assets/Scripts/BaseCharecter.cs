using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharecter : MonoBehaviour
{//доделать проверку повторного попадания
    //protected float health;
    //public float Health { get { return health; } }
    public Stats stats;
    
    public bool CanMove = true;
    
    protected virtual void Start()
    {
        //if(SaveController.saves ==null|| SaveController.saves.Count == 0)
        stats.health = stats.MaxHealth;
        StartCoroutine(WaitToMannaRegen());
    }

   
    public virtual void addHealth(float amount)
    {
        if (stats!=null)
        {

        if (stats.health + amount <= stats.MaxHealth)
        {
                stats.health += amount;
        }
        else
                stats.health = stats.MaxHealth;
        }
    }
    public virtual void TakeDamage(float Dmg)
    {
        float a = Dmg;
        if(stats!=null)
        Dmg -= Mathematics.GetPercent(stats.damageResistanceInPercent, a);
        stats.health -= Dmg;
        
        if (stats.health <= 0)
        {
           
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
protected virtual IEnumerator WaitToMannaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (stats.manna<stats.MaxManna)
            {
                stats.manna += stats.MannaEarnPerSecond;
                if (stats.manna > stats.MaxManna)
                    stats.manna = stats.MaxManna;
            }
         //   Debug.Log(stats.manna+name);
        }
    }
 
}
