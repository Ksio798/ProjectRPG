using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Studing : MonoBehaviour
{
    public GameObject EdPanel;
    void Start()
    {
        ObjectViewZone objectViewZone = GetComponentInChildren<ObjectViewZone>();
        objectViewZone.OnObjEnterZone += OnEnterZone;
        objectViewZone.OnObjExitZone += OnExitZone;
    }
    void OnEnterZone()
    {
        EdPanel.SetActive(true);
    }
   void OnExitZone()
    {
        EdPanel.SetActive(false);
    }
}
