using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackMethod
{
    public GameObject ColliderAttackZone;
    public string TargetTag;
    public int damage;
    private bool isAttacking;
    public override void OnFire(Stats playerStats)
    {
        ColliderAttackZone.SetActive(true);
    }
    public override bool AttackInput()
    {
        return Input.GetButton("Fire1");
    }
    public override void ClearAttackEffects()
    {
        ColliderAttackZone.SetActive(false);
    }




}
