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
}
