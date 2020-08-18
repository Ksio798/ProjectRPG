using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyViewZone : MonoBehaviour
{
    public event System.Action<Transform> OnObjEnterZone;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnObjEnterZone?.Invoke(other.transform);
    }

}
