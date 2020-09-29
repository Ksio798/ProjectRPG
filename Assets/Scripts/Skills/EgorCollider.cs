using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgorCollider : MonoBehaviour
{

    public int Damage = 2;

    public string TargetTag;

    

    BaseCharecter damagedCharacter;  // для проверки, в кого попала пуля. Чтобы не был нанесен повторный урон

    private void OnTriggerEnter2D(Collider2D collision)
    {


        
        { 
        if (collision.gameObject.tag == TargetTag) // если тег столкновения равен тегу цели, то нанести урон персонажу и уничтожить пулю
        {
                Debug.Log("Collisiob");

                BaseCharecter character = collision.transform.GetComponentInParent<BaseCharecter>();

            //   if (character != damagedCharacter)// если пуля продолжает сталкиваться с тем же персонажем, то не надо повторно наносить урон
            {
                damagedCharacter = character;
                if (character != null)
                    character.TakeDamage(Damage);

            }
               
        }
    }

    }
}

 

