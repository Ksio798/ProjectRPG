using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class EnemySpawner : MonoBehaviour
{
    public bool NeedDialog;
    public bool NeedCreteSave;
    public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
    public int SpawnCount;
    public float Interval;
    public List<Transform> transforms = new List<Transform>();
    public List<Dropping> Drops = new List<Dropping>();
    int DeathCount;
    public Flowchart Chart;
  //  public Transform HeroPoint;
    public void StartSpawn()
    {
       
        StartCoroutine(WaitToSpawn());
        GameController.CanCreateSave = false;
    }
    void Spawn()
    {
        EnemyController en = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count)]);
        Transform tr = transforms[Random.Range(0, transforms.Count)];
        en.Drop = Drops[Random.Range(0, Drops.Count)];
        en.gameObject.name += Random.Range(100, 1000).ToString();
        en.PointParent = tr;
        en.SetWalkingPoints();
        en.OnDie += EnemyDied;
    }
    void EnemyDied()
    {
        DeathCount++;
      
        if (DeathCount == SpawnCount)
        {
            //Вызов блока
            //Chart.ExecuteBlock("");
            if(NeedDialog)
            SpawnHeroAfterAttack();
            if(NeedCreteSave)
        GameController.CanCreateSave = true;
          
        }
    }
    void SpawnHeroAfterAttack()
    {
        if(Chart!=null)
        Chart.gameObject.SetActive(true) ;
       
    }
    IEnumerator WaitToSpawn()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            Spawn();
            yield return new WaitForSeconds(Interval);
        }
    }

}
