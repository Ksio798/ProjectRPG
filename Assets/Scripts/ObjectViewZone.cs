using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectViewZone : MonoBehaviour
{
    public event System.Action OnObjEnterZone;
    public event System.Action OnObjExitZone;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Player")
        OnObjEnterZone?.Invoke();
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            OnObjExitZone?.Invoke();
    }

}
