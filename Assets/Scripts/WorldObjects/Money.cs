using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Money : MonoBehaviour, IInteractable
{
    int cost;
    void Start()
    {

        if (OneSavePanel.SaveNum!=-1)
        {

            if (SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy.Contains(SaveHelper.CreateVector2D(transform.position))
                    && GameController.ActiveLevelID == SaveController.saves[OneSavePanel.SaveNum].LevelID)
                Destroy(gameObject);
        }


        cost = Random.Range(1, 51);
    }
    public void Interact(Transform other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();


        if (pc != null)
            pc.Inventory.AddMoney(cost);
        //else
        //    DataBase.Money += cost;

        SaveController.Instance.ObjToDesrtoy.Add(SaveHelper.CreateVector2D(transform.position));
        //  Debug.Log(DataBase.Money);
        Destroy(gameObject);
    }
    public bool InteractingByKeyPressing { get { return false; } }

    public KeyCode InteractableKey { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
}
