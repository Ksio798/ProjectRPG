using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : AttackMethod
{


    public Transform crossHair;

    public Animator SkillAnimation;
    public override void OnFire(float damage)
    {


        Vector3 direction = crossHair.position - transform.position;

        transform.right = direction;
        SkillAnimation.Play("SkillAnimation");

        timeShot = 2;
    }
}

