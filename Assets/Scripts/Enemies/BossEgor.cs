using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEgor : EnemyController
{
    public bool Activated = false;


    public void AtivateBoss()
    {
        Activated = true;
    }
         
}
