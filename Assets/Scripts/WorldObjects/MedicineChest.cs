using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MedicineChest : MonoBehaviour, IInteractable,IMedicineChest
{
    [SerializeField]
    int count;
    public int Count { get { return count; } }
    void Start()
    {
        if (OneSavePanel.SaveNum != -1)
        {

        if ( SaveController.saves[OneSavePanel.SaveNum].ObjToDestroy.Contains(SaveHelper.CreateVector2D(transform.position))
                && GameController.ActiveLevelID == SaveController.saves[OneSavePanel.SaveNum].LevelID)
            Destroy(gameObject);
        }
      
    }
    public void Interact(Transform other)
    {
        SaveController.Instance.ObjToDesrtoy.Add(SaveHelper.CreateVector2D(transform.position));
        Destroy(gameObject);
    }
    public bool InteractingByKeyPressing { get { return false; } }

    public KeyCode InteractableKey { get ; set; }
}
