using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : AttackMethod
{
    public float offset;  
    private Transform shotDir;
    public Transform crossHair;
    public WeaponData CurrentWeapon;
    public Transform holdPoint;
    public WeaponData deafultWeapon;


    private void Start()
    {
        if(deafultWeapon != null)
        SetNewWeapon(deafultWeapon);
    }

    public override void OnFire(float damage)
    {
        if (CurrentWeapon != null)
        {
            if (timeShot <= 0)
            {
                if (GetComponentInParent<Inventory>().Ammo > 0)
                {
                    Vector3 direction = crossHair.position - shotDir.position;
                    GameObject bullet = Instantiate(CurrentWeapon.ammo, shotDir.position, transform.rotation);

                    bullet.GetComponent<BulletScipt>().TargetTag = "Enemy";
                    bullet.GetComponent<BulletScipt>().Damage = CurrentWeapon.Damage+(int)damage;
                    if (transform.parent.localScale.x > 0)
                        bullet.GetComponent<Rigidbody2D>().AddForce(direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce
                    else
                        bullet.GetComponent<Rigidbody2D>().AddForce(-1 * direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce

                    Destroy(bullet.gameObject, CurrentWeapon.BulletDestroyTime); // уничтожаем копию пули с задержкой
                    timeShot = CurrentWeapon.CoolDown;

                    GetComponentInParent<Inventory>().Ammo--;

                }

            }
        }
    }



    public void SetNewWeapon(WeaponData newWeapon)
    {

        //1 удаляем предыдущее

        if (CurrentWeapon != null)
        {
            Destroy(CurrentWeapon.gameObject);
        }
        CurrentWeapon = newWeapon;
        newWeapon.transform.SetParent(transform);
        newWeapon.transform.localPosition = newWeapon.LocalPosition;
        newWeapon.transform.right = transform.right;
        shotDir = newWeapon.transform.GetChild(0);

    }

}
