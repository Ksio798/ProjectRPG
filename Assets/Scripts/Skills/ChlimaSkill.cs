using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChlimaSkill : AttackMethod
{
    int SkinIndex;
    List<Sprite> skins;
    public SpriteRenderer PlayerCurrentSkin;
    public Stats stats;
    public int ExtraDamage = 10;
    float BaseDamageLevel;
    public override void ClearAttackEffects()
    {
        stats.Damage = BaseDamageLevel;
        BaseDamageLevel = 0;
    }
    public override void OnFire(float damage)
    {
        if (BaseDamageLevel == 0)
        {
            BaseDamageLevel = stats.Damage;
            
        }
        stats.Damage = BaseDamageLevel + ExtraDamage;
        stats.manna--;
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
