using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBomb : EnemyBulletScript
{
    [HideInInspector]
    public Vector2 PlayerPos;
    // public Collider2D collider;
    List<PlayerController> players = new List<PlayerController>();
     Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void FixedUpdate()
    {


        float dit = Vector2.Distance(transform.position, PlayerPos);
      //  Debug.Log("transform.position = PlayerPos.position"+dit);

        if (dit<0.5f && GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
               animator.SetTrigger("Wait");
            transform.rotation = Quaternion.Euler(0,0,0);
            StartCoroutine(WaitToExplosion());
        }
    }
    IEnumerator WaitToExplosion()
    {
        yield return new WaitForSeconds(1);
        FindPlayersInZone();
        Explosion();

    }
    void FindPlayersInZone()
    {
        Collider2D[] ob = Physics2D.OverlapCircleAll(transform.position, 1);
        for (int i = 0; i < ob.Length; i++)
        {
            if (ob[i].tag ==TargetTag&& !players.Contains(ob[i].GetComponent<PlayerController>()))
            {
                players.Add(ob[i].GetComponent<PlayerController>());
            }
        }
    }
    void Explosion()
    {
        //Доделать анимацию!
        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerController>().TakeDamage(Damage);
        }
      animator.SetTrigger("Explosion");
       // Destroy(gameObject);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
