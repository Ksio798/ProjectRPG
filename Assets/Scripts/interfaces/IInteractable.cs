using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IInteractable
    {


    KeyCode InteractableKey { get; set; }
     void Interact(Transform other);
    bool InteractingByKeyPressing { get; }
   
}

