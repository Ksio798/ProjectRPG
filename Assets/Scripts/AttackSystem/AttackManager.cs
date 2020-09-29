using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackMethod[] AttackMethods;
    public AttackMethod CurrentPassiveAttack;
    public AttackMethod[] PassiveAttack;

    public AttackMethod CurrentActiveAttack;
    public BaseCharecter CurrentCharacter;
    int currentActiveIndex = 0;
    int currentPassiveIndex = 0;

    void Start()
    {

        //Добавить проерку на null - отсутствие аттак
        if(PassiveAttack!=null)
        CurrentPassiveAttack = PassiveAttack[0];



        CurrentActiveAttack = AttackMethods[0];
    }

    // Update is called once per frame
    void Update()
    {

        currentActiveIndex = changeAttack(CurrentActiveAttack, AttackMethods, KeyCode.Tab, currentActiveIndex);
        currentPassiveIndex = changeAttack(CurrentPassiveAttack, PassiveAttack, KeyCode.LeftControl, currentPassiveIndex);


    }

    int changeAttack(AttackMethod attackSlot, AttackMethod[] possibleAttacks, KeyCode key, int attackIndex)
    {

        if (Input.GetKeyDown(key))
        {

            attackIndex++;

            if (attackIndex >= possibleAttacks.Length)
                attackIndex = 0;

            attackSlot = possibleAttacks[attackIndex];

        }


        if (attackSlot != null)
        {
            if (attackSlot.AttackInput())
            {
                attackSlot.OnFire(CurrentCharacter.stats.Damage);
            }
        }
        return attackIndex;
    }
}
