using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackMethod : MonoBehaviour
{
    protected float timeShot;
    public abstract void OnFire(float damage);
    public virtual bool AttackInput()
    {
        return Input.GetButtonDown("Fire1");
      
    }
    public virtual void ClearAttackEffects()
    {

    }
    // Start is called before the first frame update
   protected void Start()
    {
        
    }

    // Update is called once per frame
   protected void Update()
    {


        if (timeShot > 0)
            timeShot -= Time.deltaTime;
    }
}
