using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class EnemySpawner : MonoBehaviour
{
    //доделать событие со смертью врагов
    public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
    public int SpawnCount;
    public float Interval;
    public List<Transform> transforms = new List<Transform>();
    public List<AmmoBag> ammoBags = new List<AmmoBag>();
    int DeathCount;
    public Flowchart Chart;
    public Transform HeroPoint;
    public void StartSpawn()
    {
       
        StartCoroutine(WaitToSpawn());
        GameController.CanCreateSave = false;
    }
    void Spawn()
    {
        EnemyController en = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count-1)]);
        Transform tr = transforms[Random.Range(0, transforms.Count - 1)];
        en.Drop = ammoBags[Random.Range(0, ammoBags.Count - 1)];
        en.gameObject.name += Random.Range(100, 1000).ToString();
        en.PointParent = tr;
        en.SetWalkingPoints();
        en.OnDie += EnemyDied;
    }
    void EnemyDied()
    {
        DeathCount++;
        //Debug.Log(DeathCount);
        if (DeathCount == SpawnCount)
        {
            //Вызов блока
            //Chart.ExecuteBlock("");
            SpawnHeroAfterAttack();
            Debug.Log("AllEnemyDied"); 
        GameController.CanCreateSave = true;
        }
    }
    void SpawnHeroAfterAttack()
    {
        Chart.gameObject.SetActive(true) ;
        //Chart.transform.position = HeroPoint.position;
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
