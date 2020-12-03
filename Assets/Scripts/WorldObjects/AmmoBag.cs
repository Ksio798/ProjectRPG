using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AmmoBag : Dropping, IInteractable
{
    //доделать выпадение предметов
    //public Vector2 Pos;
    public KeyCode InteractableKey { get ; set ; }

    public bool InteractingByKeyPressing { get { return false; } }
    void Start()
    {
        if (OneSavePanel.SaveNum != -1)
        {

            if (SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy.Contains(SaveHelper.CreateVector2D(transform.position))
                    && GameController.ActiveLevelID == SaveController.saves[OneSavePanel.SaveNum].LevelID)
            {
                Destroy(gameObject);
            }
        }

    }
    //void FixedUpdate()
    //{


    //    float dit = Vector2.Distance(transform.position, Pos);
    //    //  Debug.Log("transform.position = PlayerPos.position"+dit);

    //    if (dit < 0.5f && GetComponent<Rigidbody2D>().bodyType != RigidbodyType2D.Static)
    //    {
    //        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    //        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //        transform.rotation = Quaternion.Euler(0, 0, 0);
    //    }
           
         
    //}
    public void Interact(Transform other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();

        if (pc != null)
        {
            if (SaveController.Instance != null)
            SaveController.Instance.ObjToDesrtoy.Add(SaveHelper.CreateVector2D(transform.position));
            pc.Inventory.AddAmmo(Random.Range(4, 8));
            Destroy(gameObject);
           
        }
    }
   
     
     

}
