using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    public AttackMethod[] ActiveAttacks;
    public AttackMethod CurrentPassiveAttack;
    public AttackMethod[] PassiveAttack;

    public AttackMethod CurrentActiveAttack;
    public BaseCharecter CurrentCharacter;
    int currentActiveIndex = 0;
    int currentPassiveIndex = 0;

    void Start()
    {

        //Добавить проерку на null - отсутствие аттак
        if(PassiveAttack!=null&&PassiveAttack.Length!=0)
        CurrentPassiveAttack = PassiveAttack[0];


        if(ActiveAttacks!=null&&ActiveAttacks.Length!=0)
        CurrentActiveAttack = ActiveAttacks[0];



      
    }

    // Update is called once per frame
    void Update()
    {
        if (ActiveAttacks != null && ActiveAttacks.Length != 0)
        {
            currentActiveIndex = changeAttack(CurrentActiveAttack, ActiveAttacks, KeyCode.Tab, currentActiveIndex);
            CurrentActiveAttack = ActiveAttacks[currentActiveIndex];

        }
        if (PassiveAttack != null && PassiveAttack.Length != 0)
        {
            currentPassiveIndex = changeAttack(CurrentPassiveAttack, PassiveAttack, KeyCode.LeftControl, currentPassiveIndex);
            CurrentPassiveAttack = PassiveAttack[currentPassiveIndex];
        }
        attackInput(CurrentActiveAttack);
        attackInput(CurrentPassiveAttack);

    }

    int changeAttack(AttackMethod attackSlot, AttackMethod[] possibleAttacks, KeyCode key, int attackIndex)
    {

        if (Input.GetKeyDown(key))
        {

            attackIndex++;

            if (attackIndex >= possibleAttacks.Length)
                attackIndex = 0;

        
        }



        return attackIndex;
    }


    void attackInput(AttackMethod attackSlot)
    {

        if (attackSlot != null)
        {
            if (attackSlot.AttackInput())
            {
                attackSlot.OnFire(CurrentCharacter.stats.Damage);
            }
        }
    }
    
}
