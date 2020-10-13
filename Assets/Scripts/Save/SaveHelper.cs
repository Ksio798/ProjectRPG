using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveHelper : MonoBehaviour
{

    public static void loadStats(statsToSave savedStats, Stats stats)
    {

        stats.Damage = savedStats.Damage;
        stats.damageResistanceInPercent = savedStats.damageResistanceInPercent;
        stats.manna = savedStats.manna;
        stats.MannaEarnPerSecond = savedStats.MannaEarnPerSecond;
        stats.MaxHealth = savedStats.MaxHealth;
        stats.MaxManna = savedStats.MaxManna;
        stats.Speed = savedStats.Speed;
        stats.health = savedStats.health;
        stats.AmmoCountUpdate = savedStats.AmmoCountUpdate;
        stats.DamageCountUpdate = savedStats.DamageCountUpdate;
        stats.damageResistanceInPercentCountUpdate = savedStats.damageResistanceInPercentCountUpdate;
        stats.HealthCountUpdate = savedStats.HealthCountUpdate;
        stats.MedChestCountUpdate = savedStats.MedChestCountUpdate;
        stats.SpeedCountUpdate = savedStats.SpeedCountUpdate;



    }
    public static void LoadInv(InventoryToSave inventoryToSave, Inventory inventory)
    {
        inventory.currentAmmo = inventoryToSave.currentAmmo;
        inventory.ShildCount = inventoryToSave.ShildCount;
        inventory.MaxAmmo = inventoryToSave.MaxAmmo;
        inventory.MaxMedicineChestCount = inventoryToSave.MaxMedicineChestCount;
        inventory.medicineChestCount = inventoryToSave.medicineChestCount;
        inventory.HealingPercentByMedicineChest = inventoryToSave.HealingPercentByMedicineChest;


    }

    public static statsToSave CreateStructStats(Stats stats)
    {
        statsToSave statsToSave = new statsToSave();
        statsToSave.Damage = stats.Damage;
        statsToSave.damageResistanceInPercent = stats.damageResistanceInPercent;
        statsToSave.manna = stats.manna;
        statsToSave.MannaEarnPerSecond = stats.MannaEarnPerSecond;
        statsToSave.MaxHealth = stats.MaxHealth;
        statsToSave.MaxManna = stats.MaxManna;
        statsToSave.Speed = stats.Speed;
        statsToSave.health = stats.health;
        statsToSave.AmmoCountUpdate = stats.AmmoCountUpdate;
        statsToSave.DamageCountUpdate = stats.DamageCountUpdate;
        statsToSave.damageResistanceInPercentCountUpdate = stats.damageResistanceInPercentCountUpdate;
        statsToSave.HealthCountUpdate = stats.HealthCountUpdate;
        statsToSave.MedChestCountUpdate = stats.MedChestCountUpdate;
        statsToSave.SpeedCountUpdate = stats.SpeedCountUpdate;

        return statsToSave;
    }
    public static InventoryToSave CreateSctructInv(Inventory inventory)
    {
        InventoryToSave inventoryToSave = new InventoryToSave();
        inventoryToSave.HealingPercentByMedicineChest = inventory.HealingPercentByMedicineChest;
        inventoryToSave.MaxAmmo = inventory.MaxAmmo;
        inventoryToSave.MaxMedicineChestCount = inventory.MaxMedicineChestCount;
        inventoryToSave.ShildCount = inventory.ShildCount;
        inventoryToSave.medicineChestCount = inventory.medicineChestCount;
        inventoryToSave.currentAmmo = inventory.currentAmmo;
        return inventoryToSave;


    }
    public static CarInvToSave CreateSctructCarInv()
    {
        CarInvToSave carInvToSave = new CarInvToSave();
        carInvToSave.BulletsCount = CarInventory.BulletsCount;
        carInvToSave.MedChestCount = CarInventory.MedChestCount;
        carInvToSave.MoneyCount = CarInventory.MoneyCount;
        carInvToSave.MutagenCount = CarInventory.MutagenCount;
        carInvToSave.SanorinCount = CarInventory.SanorinCount;
        return carInvToSave;

    }

    public static statsToSave GetStats(PlayerType playerType)
    {
        statsToSave stats = new statsToSave();
        if (playerType == PlayerType.Egor)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].statsEgor;
        }
        else if (playerType == PlayerType.Dimitry)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].statsDima;
        }
        else if (playerType == PlayerType.Maxim)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].statsMax;
        }
        else if (playerType == PlayerType.Alex)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].statsAlex;
        }
        return stats;
    }
    public static InventoryToSave GetInv(PlayerType playerType)
    {
        InventoryToSave stats = new InventoryToSave();
        if (playerType == PlayerType.Egor)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].InvEgor;
        }
        else if (playerType == PlayerType.Dimitry)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].InvDima;
        }
        else if (playerType == PlayerType.Maxim)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].InvMax;
        }
        else if (playerType == PlayerType.Alex)
        {
            stats = SaveController.saves[OneSavePanel.SaveNum].InvAlex;
        }
        return stats;
    }
    public static Vector2D[] CreateM()
    {
        Vector2D[] m;


        if (OneSavePanel.SaveNum>=0)
        {
            m = new Vector2D[SaveController.Instance.ObjToDesrtoy.Count + SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy.Length + 10];
            for (int i = 0; i < SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy.Length; i++)
            {
                SaveController.Instance.ObjToDesrtoy.Add ( SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy[i]);

            }
            Debug.Log("Added");
        }
        else
            m = new Vector2D[SaveController.Instance.ObjToDesrtoy.Count + 1];
           
       


        
        for (int i = 0; i < SaveController.Instance.ObjToDesrtoy.Count; i++)
        {
            m[i] = SaveController.Instance.ObjToDesrtoy[i];
        }
        return m;
    }
    public static Vector2D CreateVector2D(Vector2 vector)
    {
        Vector2D vector2D = new Vector2D();
        vector2D.X = vector.x;
        vector2D.Y = vector.y;

        return vector2D;
    }
    //public static Vector2 CreateVector(Vector2D vector)
    //{
    //    Vector2 vector2 = new Vector2();
    //    vector2.x = vector.X;
    //    vector2.x = vector.Y;

    //    return vector2;
    //}
}
