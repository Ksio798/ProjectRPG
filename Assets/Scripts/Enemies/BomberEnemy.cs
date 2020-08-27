using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberEnemy : ShootingEnemy
{

    protected override void Attack()
    {
        if (timer >= TimeToAttack)
        {
            shooter.BombShoot(stats.Damage, PlayerTeg, followTarget.position - transform.position, followTarget.position);


            timer = 0;
        }
    }

}
