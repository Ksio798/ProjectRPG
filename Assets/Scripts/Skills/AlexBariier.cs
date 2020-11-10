using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexBariier : AttackMethod
{
    public GameObject Barrier;

    public GameObject CrossHair;

    public override void OnFire(Stats playerStats)
    {
        GameObject newBarrier = Instantiate(Barrier);
    }
    public void Update()
    {
        Barrier.transform.position = CrossHair.transform.position;
    }
}
