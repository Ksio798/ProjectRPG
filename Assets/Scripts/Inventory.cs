using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  int MaxMedicineChestCount = 3;
    public  int HealingPercentByMedicineChest = 50;
    public  int Money;
    public int MaxAmmo = 30;
    public GunController gc;
   // public static DataBase Instance;
    //void Awake()
    //{
    //    //if (Instance == null)
    //    //{
    //    //    Instance = this;
    //    //    DontDestroyOnLoad(gameObject);
    //    //}
    //    //else
    //    //{
    //    //    Destroy(gameObject);
    //    //}
    //}
    public void AddMoney(int count)
    {
        Money += count;
        FindObjectOfType<PlayerUIController>().SetMoney(Money);
    }
    public void AddAmmo(int count)
    {
        if (gc!=null)
        {

        if (gc.Ammo+count<=MaxAmmo)
        {
        gc.Ammo +=count;
        }
        else
        {
            gc.Ammo = MaxAmmo;
            CarInventory.BulletsCount += count - MaxAmmo;

        }
        }

    }
}
