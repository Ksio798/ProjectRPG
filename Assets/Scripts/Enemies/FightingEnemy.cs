using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingEnemy : EnemyController
{
    Animator animator;
    public float TimeToAttack;
    float timer;
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
        {
            FollowTarget();
            if (followTarget!=null&&Vector2.Distance(transform.position, followTarget.position) <= PlayerAttackDistance)
                Attack();
        }
        else
            MoveByRoute();
        if (timer<TimeToAttack)
        {
            timer += Time.deltaTime;
        }
    }
    void Attack()
    {
        //Доделать анимацию!!!
        if (timer>=TimeToAttack)
        {
            followTarget.GetComponent<PlayerController>().TakeDamage(Damage);
            timer = 0;
        }
    }
}
