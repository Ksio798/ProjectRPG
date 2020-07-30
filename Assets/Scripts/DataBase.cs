using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBase : MonoBehaviour
{
    public  int MaxMedicineChestCount = 3;
    public  int HealingPercentByMedicineChest = 50;
    public  int Money;
    public GunController gc;
   // public static DataBase Instance;
    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    public void AddMoney(int count)
    {
        Money += count;
        FindObjectOfType<PlayerUIController>().SetMoney(Money);
    }
    public void AddAmmo(int count)
    {
        gc.Ammo +=count;
    }
}
