using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSkill : AttackMethod
{
    int SkinIndex;
    List<Sprite> skins;
    public SpriteRenderer PlayerCurrentSkin;
    public Stats stats;
    public int SpeedLow = 10;
    float baseSpeed;
    float LeaseSpeedCurrent = 50;
    public override void ClearAttackEffects()
    {
        stats.Speed = baseSpeed;
        baseSpeed = 0;
    }
    public override void OnFire(float damage)
    {
        if (baseSpeed == 0)
        {
            baseSpeed = stats.Speed;

        }
        stats.Speed = baseSpeed + SpeedLow;
        stats.manna--;
    }
    public override bool AttackInput()
    {
        return Input.GetKey(KeyCode.Space);
    }
    public void SpeedLease()
    {
        stats.Speed -= LeaseSpeedCurrent;
    }


    public void ChangeSkin()
    {
        SkinIndex++;
        PlayerCurrentSkin.sprite = skins[SkinIndex];
    }
}
