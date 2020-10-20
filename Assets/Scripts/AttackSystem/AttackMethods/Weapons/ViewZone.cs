using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewZone : MonoBehaviour
{
    public event System.Action<Transform, bool> OnObjectEnterZone;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnObjectEnterZone?.Invoke(collision.transform, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OnObjectEnterZone?.Invoke(collision.transform, false);
    }
}
