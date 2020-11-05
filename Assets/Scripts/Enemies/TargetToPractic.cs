using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetToPractic : BaseCharecter
{
    public Image image;
    public override void TakeDamage(float Dmg)
    {
        base.TakeDamage(Dmg);
        UpdateHp();
    }

    void UpdateHp()
    {
        image.fillAmount = stats.health / stats.MaxHealth;
    }
}
