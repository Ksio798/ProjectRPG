using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MainMenuButton : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource clip;
    public AudioSource clip2;
    public void OnPointerEnter(PointerEventData eventData)
    {
        clip.Play();
    }
    public void OnClick()
    {
        clip2.Play();
    }

}
