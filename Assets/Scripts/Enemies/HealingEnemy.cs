using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEnemy : EnemyController
{
    bool canHealing = true;
    public float HealinInSecond;
    Coroutine coroutine;
    public Transform HomePoint;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(WaitToHeal());

    }
    protected override void Update()
    {

       // healtLevelBehave();
        base.Update();
        //if (Input.GetKeyDown(KeyCode.X))
        //{
        //    TakeDamage(6);
        //}
    }
    public override void TakeDamage(float Dmg )
    {
        base.TakeDamage(Dmg);
        canHealing = false;
        if(coroutine !=null)
       StopCoroutine(coroutine);
        coroutine = StartCoroutine(WaitTocanHeal());
    }

    IEnumerator WaitToHeal()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if(canHealing)
            addHealth(HealinInSecond);
            UpdateHp();
        }
    }
    IEnumerator WaitTocanHeal()
    {
        yield return new WaitForSeconds(7);
        canHealing = true;
    }

   
    protected override void Move()
    {
        if (stats.health > Mathematics.GetPercent(50, stats.MaxHealth))
        {
            agent.speed = stats.Speed;
            base.Move();
        }
        else
        {

            agent.speed = stats.Speed * 2;
            agent.SetDestination(HomePoint.position);
           
        }

    }
          



    protected override void Attack()
    {
        if (timer >= TimeToAttack)
        {
            followTarget.GetComponent<PlayerController>().TakeDamage(stats.Damage);
            timer = 0;
        }
    }


}
