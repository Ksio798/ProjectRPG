using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Не трогать этот скрипт!!!!
//Тут всё так и задуманно
//Скрипт служит общим инвентарем для всех персонажей и хранит в себе некоторые особые предметы
public class CarInventory : MonoBehaviour, IInteractable
{
    PlayerController CurrentPlayer;
    KeyCode key = KeyCode.E;
    public GameObject CanvCar;
    public static int MoneyCount;
    public static int MutagenCount = 5;
    public static int SanorinCount;
    public static int MedChestCount;
    public static int BulletsCount;
  public Transform UpdateParametrsPanel;
    public UIPanelUpdate HealthP;
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
            for (int i = 0; i < UpdateParametrsPanel.childCount; i++)
            {
                UpdateParametrsPanel.GetChild(i).GetComponent<UIPanelUpdate>().StartUpdateImages(CurrentPlayer.CurrentptayerType);
            }
           // HealthP.StartUpdateImages(CurrentPlayer.CurrentptayerType);


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
        if (MutagenCount>0)
        {
            CurrentPlayer.MaxHealth+=Mathematics.GetPercent(5,CurrentPlayer.MaxHealth);
            MutagenCount--;
            HealthP.UpdateImages(CurrentPlayer.CurrentptayerType);
            playerUIController.SetMutagenCount(MutagenCount);
        }
    }
    public void AddDamageResistance()
    {
        if (MutagenCount > 0)
        {

            CurrentPlayer.damageResistanceInPercent += 5;
        MutagenCount--;
        }
    }
    public void AddSpeed()
    {
        if (MutagenCount > 0)
        {
            CurrentPlayer.Speed += 10;
            MutagenCount--;
        }
    }
    public void AddMaxShildCount()
    {
        if (MutagenCount > 0)
        {
            CurrentPlayer.MaxshildCount++;
            MutagenCount--;
        }
    }
    public void AddMaxMedChestCount()
    {
        if (MutagenCount > 0)
        {
            CurrentPlayer.GetComponent<DataBase>().MaxMedicineChestCount++;
            MutagenCount--;
        }
    }










}
