using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxSkill : AttackMethod
{
    public GameObject TurretPrefab;
    public GameObject TurretMask;


    public GameObject CrossHair;

    public int MaxTurretsCount;
    int countOfSpawned;
    public override void OnFire(float damage)
    {
        GameObject newTurret = Instantiate(TurretPrefab);
        newTurret.transform.position = TurretMask.transform.position;
      //  newTurret.transform.localScale = Vector3.one;

    }


    protected override void Update()
    {
      base.Update();
        Debug.Log("ferfer");
        TurretMask.transform.position = CrossHair.transform.position;
    }

  void  onTurretDied()
    { }
    private void OnEnable()
    {
        TurretMask.SetActive(true);
    }
    private void OnDisable()
    {
        TurretMask.SetActive(false);
    }
}
