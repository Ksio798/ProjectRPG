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
    protected override void Update()
    {
        base.Update();

        GunMoving();


    }
    void GunMoving()
    {
        Vector3 pointerPosition = Input.mousePosition;
        Vector3 difference = Camera.main.ScreenToWorldPoint(pointerPosition) - transform.position;
        float scaleX = Mathf.Sign(transform.parent.localScale.x);
        if (scaleX < 0)
            difference = transform.position - Camera.main.ScreenToWorldPoint(pointerPosition);
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
             if (scaleX == 1)
             {
                 if (rotateZ <= -80f)
                 {
                     rotateZ = -80f;
                 }
                 if (rotateZ >= 80f)
                 {
                     rotateZ = 80f;
                 }
             }
        if (scaleX == -1)
        {
            if (rotateZ <= -80f)
            {
                rotateZ = -80f;
            }
            if (rotateZ >= 80f)
            {
                rotateZ = 80f;
            }
        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);
      

            Debug.Log("rotatiob");
    }
    public override void OnFire(Stats playerStats)
    {
        Debug.Log("fire");
        if (CurrentWeapon != null)
        {
            Debug.Log("fire2");
            if (timeShot <= 0)
            {
                Debug.Log("fire3");
                if (GetComponentInParent<Inventory>().Ammo > 0)
                {

                    Debug.Log("fire");
                    Vector3 direction = shotDir.right ;
                    GameObject bullet = Instantiate(CurrentWeapon.ammo, shotDir.position, transform.rotation);
                    //Debug.Log(bullet.transform.position +"   "+shotDir.position);

                    Vector3 pos = bullet.transform.position;
                    pos.z = 0;
                    bullet.transform.position = pos;
                    bullet.GetComponent<BulletScipt>().TargetTag = "Enemy";
                    bullet.GetComponent<BulletScipt>().Damage = CurrentWeapon.Damage+(int)playerStats.Damage;
                    if (transform.parent.localScale.x > 0)
                        bullet.GetComponent<Rigidbody2D>().AddForce(direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce
                    else
                        bullet.GetComponent<Rigidbody2D>().AddForce(-1 * direction * CurrentWeapon.Force);// затем прикладываем к  компоненту Rigidbody2D выктор силы: вправо от объекта Shooter с силой shootForce

                    Destroy(bullet.gameObject, CurrentWeapon.BulletDestroyTime); // уничтожаем копию пули с задержкой
                    timeShot = CurrentWeapon.CoolDown;

                    GetComponentInParent<Inventory>().Ammo--;
                    FireAttack?.Invoke();
                }

            }
        }
    }



    public void SetNewWeapon(WeaponData newWeapon)
    {

        //1 удаляем предыдущее
      
        if (CurrentWeapon != null)
        {
            CurrentWeapon.GetComponent<Collider2D>().enabled = true;
            Debug.Log(CurrentWeapon.name + "    " + newWeapon.name);
            CurrentWeapon.transform.parent = null;
           // Destroy(CurrentWeapon.gameObject);
        }
        CurrentWeapon = newWeapon;
        Vector3 scale = newWeapon.transform.localScale;
        newWeapon.transform.SetParent(transform);
        newWeapon.GetComponent<Collider2D>().enabled = false;
        newWeapon.transform.localPosition = newWeapon.LocalPosition;
        newWeapon.transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y,1);
        newWeapon.transform.right = transform.right;
        shotDir = newWeapon.transform.GetChild(0);

    }

}
