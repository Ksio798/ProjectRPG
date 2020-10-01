using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class PausMenuController : MonoBehaviour
{
    public GameObject PausPanel;
    public GameObject LoadSavePanel;
    public GameObject CreateSavePanel;
    public GameObject SettingsPanel;
   void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CreateSavePanel.SetActive(false);
            LoadSavePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            PausPanel.SetActive(!PausPanel.activeSelf);
            if (!PausPanel.activeSelf)
            {
                Time.timeScale = 1;
            }
            else  if(PausPanel.activeSelf)
            {
                Time.timeScale = 0;
            }

        }
    }
    public void LeaveToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Continue()
    {
        PausPanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void CreateSaveButton()
    {
        CreateSavePanel.SetActive(true);
        PausPanel.SetActive(false);
    }
    public void LoadSaveButton()
    {
        LoadSavePanel.SetActive(true);
        PausPanel.SetActive(false);
    }
    public void Settings()
    {
        SettingsPanel.SetActive(true);
        PausPanel.SetActive(false);
    }
}
