using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AttackMethod : MonoBehaviour
{

    public  UnityEvent FireAttack;
    protected float timeShot;
    public abstract void OnFire(Stats playerStats);
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
   protected virtual void Update()
    {


        if (timeShot > 0)
            timeShot -= Time.deltaTime;
    }
}
