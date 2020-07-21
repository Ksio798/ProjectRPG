using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScipt : MonoBehaviour
{
    [HideInInspector]
    public int Damage = 2;
    [HideInInspector]
    public string TargetTag;

    public string GroundTag;
    BaseCharecter damagedCharacter;  // для проверки, в кого попала пуля. Чтобы не был нанесен повторный урон

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == TargetTag) // если тег столкновения равен тегу цели, то нанести урон персонажу и уничтожить пулю
        {
           BaseCharecter character = collision.transform.GetComponentInParent<BaseCharecter>();

            if (character != damagedCharacter)// если пуля продолжает сталкиваться с тем же персонажем, то не надо повторно наносить урон
            {
                damagedCharacter = character;
                if (character != null)
                    character.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == GroundTag) // если пуля столкнулась с землей, то уничтожить пулю
        {

            Debug.Log("Destroy bullet" + collision.gameObject.tag);
            // Destroy(gameObject);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == GroundTag) // если пуля столкнулась с землей, то уничтожить пулю
        {

            Debug.Log("Destroy bullet" + collision.gameObject.tag);
            // Destroy(gameObject);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
    }
}
