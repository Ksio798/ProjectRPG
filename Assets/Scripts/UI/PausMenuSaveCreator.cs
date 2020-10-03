using System;
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

        if (inputField.text!=""&& inputField.text != " ")
        {
            PlayerController player = FindObjectOfType<PlayerController>();
        string path = SaveController.Instance.getTextureFilePath($"{UnityEngine.Random.Range(1,10000000)}" +".png");
        File.WriteAllBytes(path, (byte[])pausMenuController.texture.EncodeToPNG());
            SaveController.Instance.CreateSave(SceneManager.GetActiveScene().buildIndex, inputField.text,  DateTime.Today.ToString("dd.MM.yyyy"), path, player.transform.position,
             player.stats, (int)player.CurrentptayerType);
            inputField.text = "";
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    
}
