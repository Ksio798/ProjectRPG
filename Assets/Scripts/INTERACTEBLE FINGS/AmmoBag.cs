using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class AmmoBag : MonoBehaviour, IInteractable
{
    

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
