using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShooting : MonoBehaviour
{
    public Transform BulletPrefab;
    public Transform ShootPos;
    public float CollDown;
    public float Force;
    float ShootAttackTimer = 0f;
    public int BulletDestroytime;
    void Start()
    {
        if(ShootAttackTimer>0)
        {
            ShootAttackTimer -= Time.deltaTime;
        }
    }

  public void Shoot(Vector3 direction,int damage,string TargetTag)
    {
        //if(ShootAttackTimer<=0)
        {
            Transform bullet = Instantiate(BulletPrefab);
            bullet.GetComponent<BulletScipt>().TargetTag = TargetTag;
            bullet.GetComponent<BulletScipt>().Damage = damage;
            bullet.transform.position = ShootPos.position;
            bullet.GetComponent<Rigidbody2D>().AddForce(direction * Force);
            Destroy(bullet.gameObject, BulletDestroytime);
            ShootAttackTimer = CollDown;
        }

    }
}
