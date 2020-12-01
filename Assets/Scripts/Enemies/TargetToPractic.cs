using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TargetToPractic : BaseCharecter
{
    public Image image;
    public ParticleSystem particleSystem;
    public override void TakeDamage(float Dmg)
    {
        base.TakeDamage(Dmg);
        particleSystem.Play();
        UpdateHp();
    }

    void UpdateHp()
    {
        image.fillAmount = stats.health / stats.MaxHealth;
    }
}
