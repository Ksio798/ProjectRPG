using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackMethod[] AttackMethods;
    public AttackMethod CurentAttack;

    int index = 0;
    void Start()
    {
        CurentAttack = AttackMethods[0];
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Tab))
        {

            index++;

            if (index >= AttackMethods.Length)
                index = 0;

            CurentAttack = AttackMethods[index];

        }


        if (CurentAttack != null)
        {
            if(CurentAttack.AttackInput())
            {
                CurentAttack.OnFire();
            }
        }
    }
}
