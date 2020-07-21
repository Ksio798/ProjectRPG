using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScphereHealEgor : MonoBehaviour
{
    public BaseCharecter charecter;
    public int AddHealth = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            charecter.addHealth(AddHealth);

        }
    }
}
