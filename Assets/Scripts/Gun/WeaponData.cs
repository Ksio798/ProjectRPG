using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public GameObject ammo;
    public float Range;
    public float CoolDown;
    public float Force;
    public float BulletDestroyTime = 2f;
    public int Damage;

    // public Weapon Prefab;
    public Vector3 LocalPosition;
}
