using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingEnemy : EnemyController
{
    Animator animator;

    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if(animator != null)
        animator.SetInteger("Movement", 1);
    }
    private void Update()
    {
        if (followTarget != null)
            FollowTarget();
        else
            MoveByRoute();

    }

}
