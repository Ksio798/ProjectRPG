using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChlimaSkill : AttackMethod
{
    int SkinIndex;
    List<Sprite> skins;
    public SpriteRenderer PlayerCurrentSkin;
  
    public int ExtraDamage = 10;
    float BaseDamageLevel =0;

    Stats stats;
    public override void ClearAttackEffects()
    {
       // stats.Damage = BaseDamageLevel;
        BaseDamageLevel = 0;
    }
    public override void OnFire(Stats playerStats)
    {
        if (BaseDamageLevel == 0)
        {
            BaseDamageLevel = playerStats.Damage;
            stats = playerStats;
        }
        playerStats.Damage = BaseDamageLevel + ExtraDamage;
        playerStats.manna--;
        FireAttack?.Invoke();
    }
    
    
    public override bool AttackInput()
    {
        return Input.GetKey(KeyCode.Space);
    }


    public void ChangeSkin()
    {
        SkinIndex++;
        PlayerCurrentSkin.sprite = skins[SkinIndex];
    }
  

}
