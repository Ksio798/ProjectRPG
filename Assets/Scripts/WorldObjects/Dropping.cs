using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropping : MonoBehaviour
{
    public IEnumerator WaitToStop()
    {
        yield return new WaitForSeconds(Random.Range(0, 0.6f));
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
