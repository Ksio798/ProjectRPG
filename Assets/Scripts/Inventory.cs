using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public  int MaxMedicineChestCount = 1;
    public  int HealingPercentByMedicineChest = 30;
    public  int Money;
    public int MaxAmmo = 30;
   public float ShildCount = 0;
    [SerializeField]
    int medicineChestCount = 1;
    public int MedicineChestCount
    {
        get { return medicineChestCount; }
        set
        {
            if (value <= MaxMedicineChestCount)
            {
                medicineChestCount = value;
               
            }
            else
            {
                medicineChestCount = MaxMedicineChestCount;
                int a = value;
                int b = a - MaxMedicineChestCount;

               
                CarInventory.MedChestCount += b;

              
            }


        }
    }
    public GunController gc;
  
    public void AddMoney(int count)
    {
        CarInventory.MoneyCount += count;
        GetComponent<PlayerController>().playerUIController.SetMoney(CarInventory.MoneyCount);
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
        GetComponent<PlayerController>().playerUIController.SetBullet(gc.Ammo);
    }
}
