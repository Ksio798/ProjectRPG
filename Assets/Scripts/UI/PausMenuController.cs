using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausMenuController : MonoBehaviour
{
    public int resWidth = 1920;
    public int resHeight = 1080;
    public GameObject PausPanel;
    public GameObject LoadSavePanel;
    public GameObject CreateSavePanel;
    public GameObject SettingsPanel;
    public Image ScreenShot;
  public  Texture2D texture;
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
            texture = getScreenShot();
        }
    }
    Texture2D getScreenShot()
    {


        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Camera.main.targetTexture = rt;
        Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        Camera.main.targetTexture = null;
        RenderTexture.active = null; // JC: added to avoid errors
        Destroy(rt);
        screenShot.Apply();
        return screenShot;

    }
    public void CreateSaveButton()
    {
    ScreenShot.sprite = Sprite.Create(texture,
                new Rect(0, 0, resWidth, resHeight),
                  Vector2.zero, 100);
        CreateSavePanel.SetActive(true);
        PausPanel.SetActive(false);
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
