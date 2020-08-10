using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Не трогать этот скрипт!!!!
//Тут всё так и задуманно
//Скрипт служит общим инвентарем для всех персонажей и хранит в себе некоторые особые предметы
public class CarInventory : MonoBehaviour, IInteractable
{
    PlayerController CurentPlayer;
    KeyCode key = KeyCode.E;
    public GameObject CanvCar;
    public static int MoneyCount;
    public static int MutagenCount;
    public static int SanorinCount;
    public static int MedChestCount;
    public static int BulletsCount;
    public KeyCode InteractableKey { get { return key; } set { key = value; } }

    public bool InteractingByKeyPressing { get { return true; } }

    public void Interact(Transform other)
    {
        if (CanvCar.activeSelf == false)
        {
            CanvCar.SetActive(true);
            CurentPlayer = other.gameObject.GetComponent<PlayerController>();
            CurentPlayer.CanMove = false;



        }
        else
        {
            CanvCar.SetActive(false);
            CurentPlayer.CanMove = true;
            CurentPlayer = null;
        }
    }
    public void AddMaxHealth()
    {
        if (MutagenCount>0)
        {
            CurentPlayer.MaxHealth++;
            MutagenCount--;


        }
    }









     
}
