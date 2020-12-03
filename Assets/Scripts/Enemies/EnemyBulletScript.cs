using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [HideInInspector]
    public float Damage;
    [HideInInspector]
    public string TargetTag;
    [HideInInspector]
    public string GroundTag;
    PlayerController damagedPlayer;  // для проверки, в кого попала пуля. Чтобы не был нанесен повторный урон
    private void OnTriggerEnter2D(Collider2D collision)
    {

      

        if (collision.gameObject.tag == TargetTag) // если тег столкновения равен тегу цели, то нанести урон персонажу и уничтожить пулю
        {


          

            PlayerController Player = collision.transform.GetComponentInParent<PlayerController>();
            if (Player != damagedPlayer)// если пуля продолжает сталкиваться с тем же персонажем, то не надо повторно наносить урон
            {
                damagedPlayer = Player;

            if (Player != null)
                Player.TakeDamage(Damage);
            }
            // Destroy(gameObject);
            StartCoroutine(WaitToDestroy());
        }
            

        
    }
    IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
}