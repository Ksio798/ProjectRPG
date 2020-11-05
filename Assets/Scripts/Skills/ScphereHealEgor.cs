using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScphereHealEgor : AttackMethod
{
    public BaseCharecter charecter;
    public int AddHealth = 1;
    public float Waittime;


    public CircleCollider2D HealBoundaryCollider;
  
    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log("COLLISION");
        if (timeShot <= 0)
        {
            if (collision.tag == "Player")
           {
  
           
         
                charecter = collision.GetComponent<BaseCharecter>();


                Debug.Log(charecter);
                charecter.addHealth(AddHealth);
                timeShot = Waittime;
            }
        }
    }

    public override void OnFire(Stats playerStats)
    {
        HealBoundaryCollider.enabled = true;
    }
    public override bool AttackInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
    public override void ClearAttackEffects()
    {
        HealBoundaryCollider.enabled = false;
    }
}
