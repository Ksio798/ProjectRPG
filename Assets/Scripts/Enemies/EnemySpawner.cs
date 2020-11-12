using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<EnemyController> EnemyPrefabs = new List<EnemyController>();
    public int SpawnCount;
    public List<Transform> transforms = new List<Transform>();
    public void StartSpawn()
    {
        StartCoroutine(WaitToSpawn());
        GameController.CanCreateSave = false;
    }
    void Spawn()
    {
        EnemyController en = Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Count-1)]);
        Transform tr = transforms[Random.Range(0, transforms.Count - 1)];
        en.PointParent = tr;
        en.SetWalkingPoints();
    }
    IEnumerator WaitToSpawn()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            Spawn();
            yield return new WaitForSeconds(1);
        }
        GameController.CanCreateSave = true;
    }

}
