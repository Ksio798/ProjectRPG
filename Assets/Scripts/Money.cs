using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour, IInteractable
{
    int cost;
    void Start()
    {
        cost = Random.Range(1, 51);
    }
    public void Interact()
    {
        if (DataBase.Instance != null)
            DataBase.Instance.AddMoney(cost);
        else
            DataBase.Money += cost;

        
        Debug.Log(DataBase.Money);
        Destroy(gameObject);
    }
   public bool InteractingByKeyPressing { get { return false; } }
}
