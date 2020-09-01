using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public EnemyBulletScript bulletPrefab;
    public Transform ShootPos;
    public float BulletDestroyTime = 2f;
    public float shootForce = 500f;
    public void Shoot(float Dmg, string PlayerTeg, Vector2 vector)
    {
        EnemyBulletScript bullet = Instantiate(bulletPrefab); // создается копия пули на сцене. 
        bullet.transform.position = ShootPos.position; // пулю ставят в позицию Shooter
        bullet.Damage = Dmg;
        bullet.TargetTag = PlayerTeg;


        bullet.GetComponent<Rigidbody2D>().AddForce(vector* shootForce);
        Destroy(bullet.gameObject, BulletDestroyTime);
    }
    public void BombShoot(float Dmg, string PlayerTeg, Vector2 vector, Vector2 PlayerPos)
    {
        EnemyBomb bomb = Instantiate(bulletPrefab)as EnemyBomb;
        bomb.transform.position = ShootPos.position;
        bomb.Damage = Dmg;
        bomb.TargetTag = PlayerTeg;
        bomb.PlayerPos = PlayerPos;
       // Vector2 dir = target.position - transform.position;
        float time = 70 * Time.fixedDeltaTime;
        Vector2 start = new Vector2(vector.x, vector.y) / time - 0.5f * Physics2D.gravity * time;
      //  start.y *= -1;
        bomb.GetComponent<Rigidbody2D>().velocity = start;
        bomb.GetComponent<Rigidbody2D>().AddTorque(500);
    }
}
