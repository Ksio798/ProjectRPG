using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexColliderAttack : AttackMethod
{
    public Transform crossHair;

    public Animator SkillAnimation;

    public Stats stats;

    public int mannaMinus;
    public override void OnFire(Stats playerStats)
    {

        if (stats.manna > 0)
        {
            Vector3 direction = crossHair.position - transform.position;

            transform.right = direction;
            SkillAnimation.Play("OvoshAttack");

            timeShot = 2;

            stats.manna -= mannaMinus;

            FireAttack?.Invoke();
        }
    }
}
