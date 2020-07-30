using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicineChest : MonoBehaviour, IInteractable,IMedicineChest
{
    [SerializeField]
    int count;
    public int Count { get { return count; } }
    public void Interact(Transform other)
    {
        Destroy(gameObject);
    }
    public bool InteractingByKeyPressing { get { return false; } }

    public KeyCode InteractableKey { get ; set; }
}
