using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
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
    public GameObject InfoPanel;
    public GameObject GameOverPanel;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CreateSavePanel.SetActive(false);
          // LoadSavePanel.SetActive(false);
            SettingsPanel.SetActive(false);
            if (LoadSavePanel.activeSelf)
            {
                LoadSavePanel.SetActive(false);
                GameOverPanel.SetActive(true);
            }
            if(!GameOverPanel.activeSelf)
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
        if (GameController.CanCreateSave)
        {

        ScreenShot.sprite = Sprite.Create(texture,
                    new Rect(0, 0, resWidth, resHeight),
                      Vector2.zero, 100);
        CreateSavePanel.SetActive(true);
        PausPanel.SetActive(false);
        }
        else
        {
            InfoPanel.SetActive(true);
            InfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Can't create save now";
            StartCoroutine(WaitToInfoPanel());
        }
    }
    public void LeaveToMainMenu()
    {
        OneSavePanel.SaveNum = -1;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        GameController.CanSelect = true;
    }
    public void Continue()
    {
        PausPanel.SetActive(false);
        Time.timeScale = 1;
    }

   
    public void Settings()
    {
        SettingsPanel.SetActive(true);
        PausPanel.SetActive(false);
    }

    public void LoadLastSave()
    {
        if (SaveController.saves!=null&& SaveController.saves.Count != 0)
        {

        if (GameController.ActiveLevelID == SaveController.saves[SaveController.saves.Count - 1].LevelID)
        {
            OneSavePanel.SaveNum = SaveController.saves.Count - 1;
            SceneManager.LoadScene(GameController.ActiveLevelID);
        }
        else
        {
        GameController.ActiveLevelID = SaveController.saves[SaveController.saves.Count - 1].LevelID;
            OneSavePanel.SaveNum = SaveController.saves.Count - 1;
            SceneManager.LoadScene(SaveController.saves[SaveController.saves.Count - 1].LevelID);
        }
        }


    }
    public void LoadSaveButton()
    {
        if (SaveController.saves != null && SaveController.saves.Count != 0)
        {
            for (int i = SaveController.saves.Count - 1; i > -1; i--)
            {
                OneSavePanel panel = Instantiate(oneSavePanelPrefab);
                panel.SetInfo(SaveController.saves[i].SaveName, SaveController.saves[i].Date, SaveController.saves[i].LevelID, SaveController.saves[i].TexturePath, i);
                panel.GetComponent<Button>().onClick.AddListener(() => { panel.onClick(); });
                panel.transform.SetParent(LoadSavePanel.transform);
            }
        }
        GameOverPanel.SetActive(false);
        LoadSavePanel.SetActive(true);
    }
    public void GameOver()
    {
        GameOverPanel.SetActive(true);
        GameController.CanSelect = true;
    }




   IEnumerator WaitToInfoPanel()
    {
        yield return new WaitForSeconds(1);
        InfoPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Can't create save now";
        InfoPanel.SetActive(false);
    }



}
