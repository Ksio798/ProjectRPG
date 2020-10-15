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
    public Texture2D texture;
    public GameController gameController;
    public OneSavePanel oneSavePanelPrefab;
    public GameObject SavePanel;
    public GameObject SaveButton;
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
                GameController.CanSelect = true;
            }
            else if (PausPanel.activeSelf)
            {
                Time.timeScale = 0;
                GameController.CanSelect = false;
            }
            if (SaveController.saves != null && SaveController.saves.Count != 0)
            {
                SaveButton.SetActive(true);
                UpdateSavesPanel();
            }
            else
            {
                SaveButton.SetActive(false);
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
        OneSavePanel.SaveNum = -1;
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
    void UpdateSavesPanel()
    {
         
        for (int i = 0; i < SavePanel.gameObject.transform.childCount; i++)
        {
            Destroy(SavePanel.transform.GetChild(i).gameObject);
        }

        if (SaveController.saves != null)
        {
            for (int i = SaveController.saves.Count - 1; i > -1; i--)
            {
                OneSavePanel panel = Instantiate(oneSavePanelPrefab);
                panel.SetInfo(SaveController.saves[i].SaveName, SaveController.saves[i].Date, SaveController.saves[i].LevelID, SaveController.saves[i].TexturePath, i);
                panel.GetComponent<Button>().onClick.AddListener(() => { panel.onClick(); });
                panel.transform.SetParent(SavePanel.transform);
            }
        }
    }



}
