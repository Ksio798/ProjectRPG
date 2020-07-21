using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public static int MaxMedicineChestCount = 3;
    public static int HealingPercentByMedicineChest = 50;
    public static int Money;
    public static DataBase Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddMoney(int count)
    {
        Money += count;
      
    }
}
