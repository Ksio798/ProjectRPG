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

    Stats stas;


    public override void OnFire(Stats playerstats)
    {

        if (MaxTurretsCount > countOfSpawned)
        {
            GameObject newTurret = Instantiate(TurretPrefab);
            newTurret.transform.position = TurretMask.transform.position;

            newTurret.GetComponent<TurretScript>().OnTurretDestroyed.AddListener(() => countOfSpawned--);
            //  newTurret.transform.localScale = Vector3.one;
            countOfSpawned++;
            FireAttack?.Invoke();


        }


    }


    protected override void Update()
    {
        base.Update();

        TurretMask.transform.position = CrossHair.transform.position;
    }

    void onTurretDied()
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
