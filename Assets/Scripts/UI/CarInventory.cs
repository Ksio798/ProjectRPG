using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//Не трогать этот скрипт!!!!
//Тут всё так и задуманно
//Скрипт служит общим инвентарем для всех персонажей и хранит в себе некоторые особые предметы
public class CarInventory : MonoBehaviour, IInteractable
{
    PlayerController CurrentPlayer;
    KeyCode key = KeyCode.E;
    public GameObject CanvCar;
    public static int MoneyCount;
    public static int MutagenCount = 50;
    public static int SanorinCount;
    public static int MedChestCount = 1;
    public static int BulletsCount;
 // public Transform UpdateParametrsPanel;
    public UIPanelUpdate HealthP;
    public UIPanelUpdate DamageResistanceP;
    public UIPanelUpdate SpeedP;
    public UIPanelUpdate MaxShildCountP;
    public UIPanelUpdate MaxMedChestCountP;
    int lastValue;
    public Slider MedSlider;
    public TextMeshProUGUI TextPlayerMedChest;
    public TextMeshProUGUI TextInInventiryMed;
    public KeyCode InteractableKey { get { return key; } set { key = value; } }

    public bool InteractingByKeyPressing { get { return true; } }
    public PlayerUIController playerUIController;
    public void Interact(Transform other)
    {
        if (CanvCar.activeSelf == false)
        {
            CanvCar.SetActive(true);
            CurrentPlayer = other.gameObject.GetComponent<PlayerController>();
            CurrentPlayer.CanMove = false;
            //for (int i = 0; i < UpdateParametrsPanel.childCount; i++)
            //{
            //    UpdateParametrsPanel.GetChild(i).GetComponent<UIPanelUpdate>().StartUpdateImages(CurrentPlayer.CurrentptayerType);
            //}
           HealthP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
            DamageResistanceP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
            SpeedP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
            MaxShildCountP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
            MaxMedChestCountP.StartUpdateImages(CurrentPlayer.CurrentptayerType);
            MedSlider.maxValue = CurrentPlayer.MedicineChestCount + MedChestCount;
              lastValue = CurrentPlayer.MedicineChestCount;
              MedSlider.value = CurrentPlayer.MedicineChestCount;
          //  Debug.Log(MedSlider.maxValue+"    "+MedSlider.value+"    "+MedSlider.minValue+"   "+CurrentPlayer.MedicineChestCount);
          //  MedSlider.value = 3;
            TextPlayerMedChest.text = $"{CurrentPlayer.MedicineChestCount}";
            TextInInventiryMed.text = $"{MedChestCount}";
        }
        else
        {
            CanvCar.SetActive(false);
            CurrentPlayer.CanMove = true;
            CurrentPlayer = null;
        }
    }
    public void AddMaxHealth()
    {
        if (MutagenCount > 0 && HealthP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.MaxHealth+=Mathematics.GetPercent(5,CurrentPlayer.MaxHealth);
            MutagenCount--;
            HealthP.UpdateImages(CurrentPlayer.CurrentptayerType);
            playerUIController.SetHp(CurrentPlayer.MaxHealth, CurrentPlayer.Health);
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddDamageResistance()
    {
        if (MutagenCount > 0 && DamageResistanceP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            DamageResistanceP.UpdateImages(CurrentPlayer.CurrentptayerType);
            CurrentPlayer.damageResistanceInPercent += 5;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddSpeed()
    {
        if (MutagenCount > 0 && SpeedP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.Speed += 7;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            SpeedP.UpdateImages(CurrentPlayer.CurrentptayerType);
        }
    }
    public void AddMaxShildCount()
    {
        if (MutagenCount > 0&& MaxShildCountP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {

            CurrentPlayer.MaxshildCount++;
            MaxShildCountP.UpdateImages(CurrentPlayer.CurrentptayerType);
            MutagenCount--;
            playerUIController.SetShild(CurrentPlayer.MaxshildCount, CurrentPlayer.ShildCount);
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddMaxMedChestCount()
    {
        if (MutagenCount > 0&& MaxMedChestCountP.GetPlayerIndex(CurrentPlayer.CurrentptayerType)<10)
        {
            CurrentPlayer.GetComponent<DataBase>().MaxMedicineChestCount++;
            MutagenCount--;
            playerUIController.SetMutagenCount(MutagenCount);
            MaxMedChestCountP.UpdateImages(CurrentPlayer.CurrentptayerType);
        }
    }
    public void OnValueCh(Slider slider)
    {
        //if (slider.value>lastValue)
        //{

        //    Debug.Log("value <lastValue" + lastValue +"   "+ CurrentPlayer.MedicineChestCount +"  "+ CurrentPlayer.Inventory.MaxMedicineChestCount);

        //    if (CurrentPlayer.MedicineChestCount == CurrentPlayer.Inventory.MaxMedicineChestCount)
        //    {
        //        slider.value = lastValue;
        //    }
        //    else
        //    {
        //        CurrentPlayer.MedicineChestCount += (int)slider.value - lastValue; 
        //        MedChestCount -= (int)slider.value - lastValue; 
        //    }
        //}
        //else if(slider.value<lastValue)
        //{



        //    CurrentPlayer.MedicineChestCount -=lastValue- (int)slider.value;
        //    MedChestCount += lastValue - (int)slider.value;
        //}
        if (CurrentPlayer.MedicineChestCount == CurrentPlayer.Inventory.MaxMedicineChestCount&& slider.value > lastValue)
            slider.value = lastValue;
       if(slider.value> CurrentPlayer.Inventory.MaxMedicineChestCount)
            slider.value = CurrentPlayer.Inventory.MaxMedicineChestCount;
        CurrentPlayer.MedicineChestCount += (int)slider.value - lastValue;
        MedChestCount += lastValue - (int)slider.value;
        TextPlayerMedChest.text = $"{CurrentPlayer.MedicineChestCount}";
        TextInInventiryMed.text = $"{MedChestCount}";
        lastValue = (int)slider.value;
        playerUIController.SetMedicineCount(CurrentPlayer.MedicineChestCount);
        
       
           
        
        
        



      //  Debug.Log(CurrentPlayer.MedicineChestCount+"     "+MedChestCount);
     //   Debug.Log(MedSlider.maxValue + "    " + MedSlider.value + "    " + MedSlider.minValue + "   " + CurrentPlayer.MedicineChestCount);
    }









}
