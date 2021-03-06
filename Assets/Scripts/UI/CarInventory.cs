﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Не трогать этот скрипт!!!!(ок)
//Тут всё так и задуманно
//Скрипт служит общим инвентарем для всех персонажей и хранит в себе некоторые особые предметы
public class CarInventory : MonoBehaviour, IInteractable
{
    public bool CanUse = true;
    PlayerController CurrentPlayer;
    KeyCode key = KeyCode.E;
    public GameObject CanvCar;
    public static int MoneyCount;
    public static int MutagenCount = 50;
    public static int SanorinCount;
    public static int MedChestCount = 1;
    public static int BulletsCount = 5;
 // public Transform UpdateParametrsPanel;
    public UIPanelUpdate HealthP;
    public UIPanelUpdate DamageResistanceP;
    public UIPanelUpdate SpeedP;
    public UIPanelUpdate AttackP;
    public UIPanelUpdate MaxMedChestCountP;
    public UIPanelUpdate MaxBulletPanel;
    int lastValueMed;
    public Slider MedSlider;
    public TextMeshProUGUI TextPlayerMedChest;
    public TextMeshProUGUI TextInInventiryMed;
    int lastValueBullet;
    public Slider BulletsSlider;
    public TextMeshProUGUI TextPlayerBullets;
    public TextMeshProUGUI TextInInventoryBullets;
    


    public KeyCode InteractableKey { get { return key; } set { key = value; } }

    public bool InteractingByKeyPressing { get { return true; } }
    public PlayerUIController playerUIController;
    void Start()
    {
        if (OneSavePanel.SaveNum !=-1)
        {

        BulletsCount = SaveController.saves[OneSavePanel.SaveNum].carInv.BulletsCount;
        MedChestCount = SaveController.saves[OneSavePanel.SaveNum].carInv.MedChestCount;
        SanorinCount = SaveController.saves[OneSavePanel.SaveNum].carInv.SanorinCount;
        MutagenCount = SaveController.saves[OneSavePanel.SaveNum].carInv.MutagenCount;
        MoneyCount = SaveController.saves[OneSavePanel.SaveNum].carInv.MoneyCount;
        }
        else
        {
            MoneyCount = 0;
            MutagenCount = 50;
            SanorinCount = 0;
            MedChestCount = 1;
            BulletsCount = 5;
        }
    }


    public void Interact(Transform other)
    {
        if(CanUse)
            {
            if (CanvCar.activeSelf == false)
            {
                CanvCar.SetActive(true);
                CurrentPlayer = other.gameObject.GetComponent<PlayerController>();
                CurrentPlayer.CanMove = false;
                GameController.CanSelect = false;
                OnInteract();


            }
            else
            {
                GameController.CanSelect = true;
                CanvCar.SetActive(false);
                CurrentPlayer.CanMove = true;
                CurrentPlayer = null;
            }
        }
    }
    public void AddMaxHealth()
    {
        if (MutagenCount > 0 && CurrentPlayer.stats.HealthCountUpdate<10)
        {
            CurrentPlayer.stats.MaxHealth+=Mathematics.GetPercent(5,CurrentPlayer.stats.MaxHealth);
            MutagenCount--;
            CurrentPlayer.stats.HealthCountUpdate++;
            HealthP.UpdateImages(CurrentPlayer.stats.HealthCountUpdate-1);
            playerUIController.SetHp(CurrentPlayer.stats.MaxHealth, CurrentPlayer.stats.health);
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddDamageResistance()
    {
        if (MutagenCount > 0 && CurrentPlayer.stats.damageResistanceInPercentCountUpdate < 10)
        {
            CurrentPlayer.stats.damageResistanceInPercentCountUpdate++;
            DamageResistanceP.UpdateImages(CurrentPlayer.stats.damageResistanceInPercentCountUpdate-1);
            CurrentPlayer.stats.damageResistanceInPercent += 5;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddSpeed()
    {
        if (MutagenCount > 0 && CurrentPlayer.stats.SpeedCountUpdate < 10)
        {
            CurrentPlayer.stats.Speed += 7;
            CurrentPlayer.stats.SpeedCountUpdate++;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            SpeedP.UpdateImages(CurrentPlayer.stats.SpeedCountUpdate-1);
        }
    }
    public void AddAttack()
    {
        if (MutagenCount > 0 && CurrentPlayer.stats.DamageCountUpdate < 10)
        {
            CurrentPlayer.stats.Damage++ ;
            CurrentPlayer.stats.DamageCountUpdate++;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            AttackP.UpdateImages(CurrentPlayer.stats.DamageCountUpdate - 1);
        }
    }
    public void AddMaxMedChestCount()
    {
        if (MutagenCount > 0&& CurrentPlayer.stats.MedChestCountUpdate < 10)
        {
            CurrentPlayer.GetComponent<Inventory>().MaxMedicineChestCount++;
            MutagenCount--;
            CurrentPlayer.stats.MedChestCountUpdate++;
            playerUIController.SetMutagenCount(MutagenCount);
            MaxMedChestCountP.UpdateImages(CurrentPlayer.stats.MedChestCountUpdate - 1);
        }
    }
    public void AddMaxBulletCount()
    {
        if (MutagenCount > 0 && CurrentPlayer.stats.AmmoCountUpdate < 10)
        {
            CurrentPlayer.GetComponent<Inventory>().MaxAmmo+=5;
            MutagenCount--;
            CurrentPlayer.stats.AmmoCountUpdate++;
            playerUIController.SetMutagenCount(MutagenCount);
            MaxBulletPanel.UpdateImages(CurrentPlayer.stats.AmmoCountUpdate - 1);
        }
    }



    public void OnValueCh(Slider slider)
    {
       
        if (CurrentPlayer.Inventory.MedicineChestCount == CurrentPlayer.Inventory.MaxMedicineChestCount&& slider.value > lastValueMed)
            slider.value = lastValueMed;
       if(slider.value> CurrentPlayer.Inventory.MaxMedicineChestCount)
            slider.value = CurrentPlayer.Inventory.MaxMedicineChestCount;
        CurrentPlayer.Inventory.MedicineChestCount += (int)slider.value - lastValueMed;
        MedChestCount += lastValueMed - (int)slider.value;
        TextPlayerMedChest.text = $"{CurrentPlayer.Inventory.MedicineChestCount}";
        TextInInventiryMed.text = $"{MedChestCount}";
        lastValueMed = (int)slider.value;
        playerUIController.SetMedicineCount(CurrentPlayer.Inventory.MedicineChestCount);
    }
      public void OnValueChBullets()
    {
        if (CurrentPlayer.Inventory.Ammo == CurrentPlayer.Inventory.MaxAmmo && BulletsSlider.value > lastValueBullet)
            BulletsSlider.value = lastValueBullet;
        if (BulletsSlider.value > CurrentPlayer.Inventory.MaxAmmo)
            BulletsSlider.value = CurrentPlayer.Inventory.MaxAmmo;
        CurrentPlayer.Inventory.Ammo += (int)BulletsSlider.value - lastValueBullet;
        BulletsCount += lastValueBullet - (int)BulletsSlider.value;
        TextPlayerBullets.text = $"{CurrentPlayer.Inventory.Ammo}";
        TextInInventoryBullets.text = $"{BulletsCount}";
        lastValueBullet = (int)BulletsSlider.value;
        playerUIController.SetBullet((int)CurrentPlayer.Inventory.Ammo);
    }  
    void OnInteract()
    {
        HealthP.StartUpdateImages(CurrentPlayer.stats.HealthCountUpdate);
        DamageResistanceP.StartUpdateImages(CurrentPlayer.stats.damageResistanceInPercentCountUpdate);
        SpeedP.StartUpdateImages(CurrentPlayer.stats.SpeedCountUpdate);
        AttackP.StartUpdateImages(CurrentPlayer.stats.DamageCountUpdate);
        MaxMedChestCountP.StartUpdateImages(CurrentPlayer.stats.MedChestCountUpdate);
        MaxBulletPanel.StartUpdateImages(CurrentPlayer.stats.AmmoCountUpdate);
        MedSlider.maxValue = CurrentPlayer.Inventory.MedicineChestCount + MedChestCount;
        lastValueMed = CurrentPlayer.Inventory.MedicineChestCount;
        MedSlider.value = CurrentPlayer.Inventory.MedicineChestCount;
        TextPlayerMedChest.text = $"{CurrentPlayer.Inventory.MedicineChestCount}";
        TextInInventiryMed.text = $"{MedChestCount}";
        BulletsSlider.maxValue = CurrentPlayer.Inventory.Ammo + BulletsCount;
        lastValueBullet = (int)CurrentPlayer.Inventory.Ammo;
        BulletsSlider.value = CurrentPlayer.Inventory.Ammo;
        TextPlayerBullets.text = $"{CurrentPlayer.Inventory.Ammo}";
        TextInInventoryBullets.text = $"{BulletsCount}";
    }   
           
        
        
        



     









}
