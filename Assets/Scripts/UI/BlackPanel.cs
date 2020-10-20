using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackPanel : MonoBehaviour
{
    public GameObject text;
   public void TextAnimStart()
    {
        text.GetComponent<Animation>().Play();
    }
}
