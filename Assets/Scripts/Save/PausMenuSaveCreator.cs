﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class PausMenuSaveCreator : MonoBehaviour
{
    public TMP_InputField inputField;
    public PausMenuController pausMenuController;

    //вызывается при нажатии на кнопку
    public void CreateSave()
    {

        if (inputField.text != "" && inputField.text != " ")
        {

            string path = SaveController.Instance.getTextureFilePath($"{UnityEngine.Random.Range(1, 10000000)}" + ".png");
            File.WriteAllBytes(path, (byte[])pausMenuController.texture.EncodeToPNG());
            SaveController.Instance.CreateSave(SceneManager.GetActiveScene().buildIndex, inputField.text, DateTime.Today.ToString("dd.MM.yyyy"), path,
                pausMenuController.gameController.ActivePlayer.transform.position,
           (int)pausMenuController.gameController.ActivePlayer.CurrentptayerType, pausMenuController.gameController.Egor.stats, pausMenuController.gameController.Dima.stats,
           pausMenuController.gameController.Max.stats, pausMenuController.gameController.Alex.stats,
            pausMenuController.gameController.Egor.Inventory, pausMenuController.gameController.Dima.Inventory, pausMenuController.gameController.Max.Inventory,
            pausMenuController.gameController.Alex.Inventory, false);

            inputField.text = "";
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void CreateQuickSave(int index)
    {
        string path = "QuickSave";
        SaveController.Instance.CreateSave(index, path, DateTime.Today.ToString("dd.MM.yyyy"), path,
            pausMenuController.gameController.ActivePlayer.transform.position,
       (int)pausMenuController.gameController.ActivePlayer.CurrentptayerType, pausMenuController.gameController.Egor.stats, pausMenuController.gameController.Dima.stats,
       pausMenuController.gameController.Max.stats, pausMenuController.gameController.Alex.stats,
        pausMenuController.gameController.Egor.Inventory, pausMenuController.gameController.Dima.Inventory, pausMenuController.gameController.Max.Inventory,
        pausMenuController.gameController.Alex.Inventory, true);
    }


}
