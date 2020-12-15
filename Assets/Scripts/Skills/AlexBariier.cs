using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlexBariier : AttackMethod
{
    public GameObject Barrier;
    public GameObject BarrierMask;

    public GameObject CrossHair;

    public override void OnFire(Stats playerStats)
    {
        if (playerStats.manna > 0)
        {
            GameObject newBarrier = Instantiate(Barrier);
            playerStats.manna -= 20;
            FireAttack?.Invoke();
            Destroy(newBarrier.gameObject, 5);
        }
    }
    public new void Update()
    {
        Barrier.transform.position = CrossHair.transform.position;
    }

}
