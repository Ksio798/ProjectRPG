using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SuicidalEnemy : EnemyController
{
    //protected override void Update()
    //{
    //    base.Update();
    //    if (followTarget != null)
    //    {
    //        FollowTarget();
    //        if (followTarget != null && Vector2.Distance(transform.position, followTarget.position) <= PlayerAttackDistance)
    //            Attack();
    //    }
    //    else
    //        MoveByRoute();

    //}
   protected override void Attack()
    {
        followTarget.GetComponent<PlayerController>().TakeDamage(stats.Damage);
        Die();
    }
    protected override void MoveByRoute()
    {
        agent.speed = stats.Speed;
        base.MoveByRoute();
    }
    protected override void FollowTarget()
    {
        agent.speed = stats.Speed * 2.3f;
        base.FollowTarget();
    }

}
