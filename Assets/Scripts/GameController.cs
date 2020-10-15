﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameController : MonoBehaviour
{
    [SerializeField]
    PlayerController EgorPrefab;
    [SerializeField]
    PlayerController DimaPrefab;
    [SerializeField]
    PlayerController MaxPrefab;
    [SerializeField]
    PlayerController AlexPrefab;
    public Transform StartPoint;
    [HideInInspector]
    public PlayerController Egor;
    [HideInInspector]
    public PlayerController Dima;
    [HideInInspector]
    public PlayerController Max;
    [HideInInspector]
    public PlayerController Alex;
    [HideInInspector]
    public PlayerController ActivePlayer;
    public PlayerUIController playerUIController;
    public CarInventory carInventory;
        [HideInInspector]
   public static bool CanSelect = true;
    public static int ActiveLevelID;
    public TextMeshProUGUI StartText;
    public string TextOnStart;
    Texture2D texture;
    float TargetTime = 5.5f;
    float time;
    void Start()
    {
        CreateTex();
        StartText.text = TextOnStart;

        PlayerStart();
    }
    void PlayerStart()
    {
        Egor = Instantiate(EgorPrefab);
        Dima = Instantiate(DimaPrefab);
        Max = Instantiate(MaxPrefab);
        Alex = Instantiate(AlexPrefab);
        Egor.playerUIController = playerUIController;
        Dima.playerUIController = playerUIController;
        Max.playerUIController = playerUIController;
        Alex.playerUIController = playerUIController;
        Egor.stats.SetStartHealth();
        Dima.stats.SetStartHealth();
        Max.stats.SetStartHealth();
        Alex.stats.SetStartHealth();
        if (OneSavePanel.SaveNum == -1)
        {
            Dima.transform.position = StartPoint.position;
            ActivePlayer = Dima;
            Egor.gameObject.SetActive(false);
            Alex.gameObject.SetActive(false);
            Max.gameObject.SetActive(false);
        }
        else
        {
            GetPlayerByType();
        }
    }
    void Update()
    {
        PlayerSelect();
    }
    void PlayerSelect()
    {
        if (CanSelect)
        {


            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Egor != ActivePlayer)
                {
                    Egor.transform.position = ActivePlayer.transform.position;
                    Egor.gameObject.SetActive(true);
                    ActivePlayer.gameObject.SetActive(false);
                    ActivePlayer = Egor;
                    CanSelect = false;
                    StartCoroutine(WaitToCanS());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Dima != ActivePlayer)
                {
                    Dima.transform.position = ActivePlayer.transform.position;
                    Dima.gameObject.SetActive(true);
                    ActivePlayer.gameObject.SetActive(false);
                    ActivePlayer = Dima;
                    CanSelect = false;
                    StartCoroutine(WaitToCanS());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Max != ActivePlayer)
                {
                    Max.transform.position = ActivePlayer.transform.position;
                    Max.gameObject.SetActive(true);
                    ActivePlayer.gameObject.SetActive(false);
                    ActivePlayer = Max;
                    CanSelect = false;
                    StartCoroutine(WaitToCanS());
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (Alex != ActivePlayer)
                {
                    Alex.transform.position = ActivePlayer.transform.position;
                    Alex.gameObject.SetActive(true);
                    ActivePlayer.gameObject.SetActive(false);
                    ActivePlayer = Alex;
                    CanSelect = false;
                    StartCoroutine(WaitToCanS());
                }
            }

            ActivePlayer.UpdateUI();
        }
    }

    void GetPlayerByType()
    {
        if (SaveController.saves[OneSavePanel.SaveNum].PlayerType == (int)PlayerType.Egor)
        {
            ActivePlayer = Egor;
        }
        else if (SaveController.saves[OneSavePanel.SaveNum].PlayerType == (int)PlayerType.Dimitry)
        {
            ActivePlayer = Dima;
        }
        else if (SaveController.saves[OneSavePanel.SaveNum].PlayerType == (int)PlayerType.Maxim)
        {
            ActivePlayer = Max;
        }
        else if (SaveController.saves[OneSavePanel.SaveNum].PlayerType == (int)PlayerType.Alex)
        {
            ActivePlayer = Alex;
        }
    }
    

    IEnumerator WaitToCanS()
    {
        yield return new WaitForSeconds(10);
        CanSelect = true;
    }
    IEnumerator WaitToTex()
    {
        while (time < TargetTime)
        {

            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;

            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    texture.SetPixel(i, j, Color.Lerp(Color.black, Color.clear, time / TargetTime));
                }
            }
            texture.Apply();
           
        }
        StartText.GetComponent<Animation>().Play();
    }
   void  CreateTex()
    {
        texture = new Texture2D(16, 16);


        time = 0;

        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                texture.SetPixel(i, j, Color.black);
            }
        }
        texture.Apply();
        StartCoroutine(WaitToTex());
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);

    }
}
