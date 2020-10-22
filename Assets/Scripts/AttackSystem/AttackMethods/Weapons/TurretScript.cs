using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    Transform target;
    public ViewZone viewZone;
    public string TargetTag;
    public TurretShooting turret;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        viewZone.OnObjectEnterZone += View;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Attack()
    {
        Vector3 direction = target.position - transform.position;
        turret.Shoot(direction, damage, TargetTag);
    }
    void View(Transform other, bool enter)
    {
        if (enter)
        {
            if (other.tag == TargetTag)
            { 
            target = other;
            Attack();
            }
        }
        else
        {
            if (target == other)
                target = null;
        }
        
    }
}
