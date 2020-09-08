using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeletortingEnemy : EnemyController
{
    protected override void Attack()
    {
        if (timer >= TimeToAttack)
        {
            followTarget.GetComponent<PlayerController>().TakeDamage(stats.Damage);
            timer = 0;
            Teleport();
        }
    }
    void Teleport()
    {

    }
}
