using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexBariier : AttackMethod
{
    public GameObject Barrier;

    public override void OnFire(Stats playerStats)
    {
        GameObject newBarrier = Instantiate(Barrier);
    }
}
