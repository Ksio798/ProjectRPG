using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float offset;

    public float Ammo;

    private Transform shotDir;
    private float timeShot;
    // public float StartTime;
    // public float shootForce = 500f;



    public Transform crossHair;

    WeaponData CurrentWeapon;
    public Transform holdPoint;
    // Start is called before the first frame update
    void Start()
    {

        Ammo = 5;

       
      

        
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentWeapon != null)
        {


            if (Input.GetButtonDown("Fire1"))
            {
                OnFire();
            }
        }

        if (timeShot > 0)
            timeShot -= Time.deltaTime;

    }


    public void OnFire()
    {
        if (timeShot <= 0)
        {
            if (Ammo > 0)
            {
                Vector3 direction = crossHair.position - shotDir.position;
                GameObject bullet = Instantiate(CurrentWeapon.ammo, shotDir.position, transform.rotation);

                bullet.GetComponent<BulletScipt>().TargetTag = "Enemy";
                bullet.GetComponent<BulletScipt>().Damage = CurrentWeapon.Damage;
                if (transform.parent.localScale.x > 0)
                    bullet.GetComponent<Rigidbody2D>().AddForce(direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce
                else
                    bullet.GetComponent<Rigidbody2D>().AddForce(-1 * direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce

                Destroy(bullet.gameObject, CurrentWeapon.BulletDestroyTime); // уничтожаем копию пули с задержкой
                timeShot = CurrentWeapon.CoolDown;

                Ammo--;

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
