using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour, IInteractable,IMedicineChest
{
    [SerializeField]
    int count;
    public int Count { get { return count; } }
    public void Interact()
    {
        Destroy(gameObject);
    }
    public bool InteractingByKeyPressing { get { return false; } }
}
