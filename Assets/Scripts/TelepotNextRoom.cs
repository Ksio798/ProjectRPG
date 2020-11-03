using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelepotNextRoom : MonoBehaviour, IInteractable
{
    public Transform PointTP;
    public KeyCode InteractableKey { get; set; }
    public void Interact(Transform other)
    {
        other.position = PointTP.position;

       
    }
    public bool InteractingByKeyPressing { get { return false; } }
}
