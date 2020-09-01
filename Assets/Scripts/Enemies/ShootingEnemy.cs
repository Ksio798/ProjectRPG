using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyController
{
    Animator animator;
   
  
   
   
    
    public string PlayerTeg = "Player";
    public EnemyShooter shooter;
    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetInteger("Movement", 1);
    }
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
        //Доделать анимацию!!!
        if (timer >= TimeToAttack)
        {
            shooter.Shoot(stats.Damage, PlayerTeg, followTarget.position-transform.position);
            

            timer = 0;
        }
    }
}
