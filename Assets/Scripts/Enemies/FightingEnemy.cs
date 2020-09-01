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
    //protected override void Update()
    //{
    //    base.Update();
    //    if (followTarget != null)
    //    {
    //        FollowTarget();
    //        if (followTarget!=null&&Vector2.Distance(transform.position, followTarget.position) <= PlayerAttackDistance)
    //            Attack();
    //    }
    //    else
    //        MoveByRoute();
       
    //}
   protected override void Attack()
    {
        //Доделать анимацию!!!
        if (timer>=TimeToAttack)
        {
            followTarget.GetComponent<PlayerController>().TakeDamage(stats.Damage);
            timer = 0;
        }
    }
}
