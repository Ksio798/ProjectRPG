using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgorAttack : AttackMethod
{


    public Transform crossHair;

    public Animator SkillAnimation;

    public Stats stats;

    public int mannaMinus;
    public override void OnFire(float damage)
    {

        if (stats.manna > 0)
        { 
        Vector3 direction = crossHair.position - transform.position;

        transform.right = direction;
        SkillAnimation.Play("SkillAnimation");

        timeShot = 2;
          
            stats.manna -= mannaMinus;
        }
    }
}

