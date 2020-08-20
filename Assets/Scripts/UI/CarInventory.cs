using System.Collections;
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
    public void Interact(Transform other)
    {
        if(CanUse)
            {
            if (CanvCar.activeSelf == false)
            {
                CanvCar.SetActive(true);
                CurrentPlayer = other.gameObject.GetComponent<PlayerController>();
                CurrentPlayer.CanMove = false;

                OnInteract();


            }
            else
            {
                CanvCar.SetActive(false);
                CurrentPlayer.CanMove = true;
                CurrentPlayer = null;
            }
        }
    }
    public void AddMaxHealth()
    {
        if (MutagenCount > 0 && HealthP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.stats.MaxHealth+=Mathematics.GetPercent(5,CurrentPlayer.stats.MaxHealth);
            MutagenCount--;
            HealthP.UpdateImages(CurrentPlayer.CurrentptayerType);
            playerUIController.SetHp(CurrentPlayer.stats.MaxHealth, CurrentPlayer.Health);
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddDamageResistance()
    {
        if (MutagenCount > 0 && DamageResistanceP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            DamageResistanceP.UpdateImages(CurrentPlayer.CurrentptayerType);
            CurrentPlayer.stats.damageResistanceInPercent += 5;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddSpeed()
    {
        if (MutagenCount > 0 && SpeedP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.stats.Speed += 7;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            SpeedP.UpdateImages(CurrentPlayer.CurrentptayerType);
        }
    }
    public void AddAttack()
    {
        if (MutagenCount > 0 && AttackP.GetPlayerIndex(CurrentPlayer.CurrentptayerType) < 10)
        {
            CurrentPlayer.stats.Damage++ ;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            AttackP.UpdateImages(CurrentPlayer.CurrentptayerType);
        }
    }
    public void AddMaxMedChestCount()
    {
        if (MutagenCount > 0&& MaxMedChestCountP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.GetComponent<Inventory>().MaxMedicineChestCount++;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            MaxMedChestCountP.UpdateImages(CurrentPlayer.CurrentptayerType);
        }
    }
    public void AddMaxBulletCount()
    {
        if (MutagenCount > 0 && MaxBulletPanel.GetPlayerIndex(CurrentPlayer.CurrentptayerType) < 10)
        {
            CurrentPlayer.GetComponent<Inventory>().MaxAmmo+=5;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            MaxBulletPanel.UpdateImages(CurrentPlayer.CurrentptayerType);
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
        if (CurrentPlayer.Inventory.gc.Ammo == CurrentPlayer.Inventory.MaxAmmo && BulletsSlider.value > lastValueBullet)
            BulletsSlider.value = lastValueBullet;
        if (BulletsSlider.value > CurrentPlayer.Inventory.MaxAmmo)
            BulletsSlider.value = CurrentPlayer.Inventory.MaxAmmo;
        CurrentPlayer.Inventory.gc.Ammo += (int)BulletsSlider.value - lastValueBullet;
        BulletsCount += lastValueBullet - (int)BulletsSlider.value;
        TextPlayerBullets.text = $"{CurrentPlayer.Inventory.gc.Ammo}";
        TextInInventoryBullets.text = $"{BulletsCount}";
        lastValueBullet = (int)BulletsSlider.value;
        playerUIController.SetBullet((int)CurrentPlayer.Inventory.gc.Ammo);
    }  
    void OnInteract()
    {
        HealthP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        DamageResistanceP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        SpeedP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        AttackP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        MaxMedChestCountP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        MaxBulletPanel.StartUpdateImages(CurrentPlayer.CurrentptayerType);
        MedSlider.maxValue = CurrentPlayer.Inventory.MedicineChestCount + MedChestCount;
        lastValueMed = CurrentPlayer.Inventory.MedicineChestCount;
        MedSlider.value = CurrentPlayer.Inventory.MedicineChestCount;
        TextPlayerMedChest.text = $"{CurrentPlayer.Inventory.MedicineChestCount}";
        TextInInventiryMed.text = $"{MedChestCount}";
        BulletsSlider.maxValue = CurrentPlayer.Inventory.gc.Ammo + BulletsCount;
        lastValueBullet = (int)CurrentPlayer.Inventory.gc.Ammo;
        BulletsSlider.value = CurrentPlayer.Inventory.gc.Ammo;
        TextPlayerBullets.text = $"{CurrentPlayer.Inventory.gc.Ammo}";
        TextInInventoryBullets.text = $"{BulletsCount}";
    }   
           
        
        
        



     









}
