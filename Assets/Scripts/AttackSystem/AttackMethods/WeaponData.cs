using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour, IInteractable
{
    public GameObject ammo;
    public float Range;
    public float CoolDown;
    public float Force;
    public float BulletDestroyTime = 2f;
    public int Damage;

    // public Weapon Prefab;
    public Vector3 LocalPosition;

    [SerializeField]
     bool interactByKey;
    [SerializeField]
    KeyCode interactionKey;
    public bool InteractingByKeyPressing { get { return interactByKey; } }

    public KeyCode InteractableKey { get ; set; }

    public void Interact(Transform other)
    {

        RangeAttack gc = other.GetComponentInChildren<RangeAttack>();

       //transform.position = holdPoint.position;

    
            gc.SetNewWeapon(this);
        
    }
}
