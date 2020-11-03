using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScphereHealEgor : MonoBehaviour
{
    public BaseCharecter charecter;
    public int AddHealth = 1;
    public float Waittime;
    private float timer;
    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        Debug.Log("COLLISION");
        if (timer <= 0)
        {
            if (collision.tag == "Player")
           {
  
           
         
                charecter = collision.GetComponent<BaseCharecter>();


                Debug.Log(charecter);
                charecter.addHealth(AddHealth);
                timer = Waittime;
            }
        }
    }
}
