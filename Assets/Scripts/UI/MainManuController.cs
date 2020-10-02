using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManuController : MonoBehaviour
{
    
    public GameObject SavePanel;
    public GameObject SettingsPanel;
    public GameObject LoadButton;
    public OneSavePanel oneSavePanelPrefab;
    public static bool FirstStart = true;
    void Start()
    {
        if (FirstStart)
        {
        SaveController.Instance.LoadData();
            FirstStart = false;
        }
        if (SaveController.saves!=null)
        {
            for (int i = SaveController.saves.Count-1; i > -1; i--)
            {
                OneSavePanel panel = Instantiate(oneSavePanelPrefab);
                panel.SetInfo(SaveController.saves[i].SaveName, SaveController.saves[i].Date, SaveController.saves[i].LevelID, SaveController.saves[i].TexturePath);
                panel.GetComponent<Button>().onClick.AddListener(() => { panel.onClick(); });
                panel.transform.SetParent(SavePanel.transform);
            }
        }
         if(SaveController.saves.Count == 0|| SaveController.saves == null)
        {
            LoadButton.SetActive(false);
       
        }

    }


   public void LoadS(int index)
    {
        StartCoroutine(WaitToLoad(index));
    }
    public void SavePanelA()
    {
        SavePanel.SetActive(!SavePanel.activeSelf);
        SettingsPanel.SetActive(false);
    }
    public void SettingsPanelA()
    {
        SettingsPanel.SetActive(!SettingsPanel.activeSelf);
        SavePanel.SetActive(false);
    }
    public void Exit()
    {
        
        SaveController.Instance.SaveData();
        Application.Quit();
    }
    IEnumerator WaitToLoad(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
   

}
