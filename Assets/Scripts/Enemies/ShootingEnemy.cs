using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : EnemyController
{
    Animator animator;
    public float TimeToAttack;
    float timer;
    public EnemyBulletScript bulletPrefab;
    public Transform ShootPos;
   
    public float shootForce = 500f;
  
    public float BulletDestroyTime = 2f;
    public string PlayerTeg = "Player";
    override protected void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetInteger("Movement", 1);
    }
    private void Update()
    {
        if (followTarget != null)
        {
            FollowTarget();
            if (followTarget != null && Vector2.Distance(transform.position, followTarget.position) <= PlayerAttackDistance)
                Attack();
        }
        else
            MoveByRoute();
        if (timer < TimeToAttack)
        {
            timer += Time.deltaTime;
        }
    }
    void Attack()
    {
        //Доделать анимацию!!!
        if (timer >= TimeToAttack)
        {
            EnemyBulletScript bullet = Instantiate(bulletPrefab); // создается копия пули на сцене. 
            bullet.transform.position = ShootPos.position; // пулю ставят в позицию Shooter
            bullet.Damage = Damage;
            bullet.TargetTag = PlayerTeg;


            bullet.GetComponent<Rigidbody2D>().AddForce((followTarget.position-transform.position )* shootForce); 
            Destroy(bullet.gameObject, BulletDestroyTime);
            

            timer = 0;
        }
    }
}
