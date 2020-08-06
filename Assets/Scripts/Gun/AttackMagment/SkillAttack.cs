using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : AttackMethod
{



    public Animator SkillAnimation;
    public override void OnFire()
    {
        SkillAnimation.Play("SkillAnimation");

        timeShot = 2;
    }
}
