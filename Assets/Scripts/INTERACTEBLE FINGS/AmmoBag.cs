using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBag : MonoBehaviour, IInteractable
{
    

    public KeyCode InteractableKey { get ; set ; }

    public bool InteractingByKeyPressing { get; set; }

    public void Interact(Transform other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if(pc != null)
        {
            pc.Inventory.AddAmmo(Random.Range(4, 8));
            Destroy(gameObject);
        }
    }


}
