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

        Debug.Log(SavePanel);
        Debug.Log(SavePanel.transform);
        if (FirstStart)
        {
        SaveController.Instance.LoadData();
            FirstStart = false;
        }

        SaveController.Instance.Unsubscribe();
         SaveController.Instance.OnDeliteAllSaves += LoadSavesOnPanel;
        LoadSavesOnPanel();

    }

    void LoadSavesOnPanel()
    {
        Debug.Log(SaveController.saves.Count);
        ClearSaveP();
      
        if (SaveController.saves != null && SaveController.saves.Count != 0)
        {
            for (int i = SaveController.saves.Count - 1; i > -1; i--)
            {
                OneSavePanel panel = Instantiate(oneSavePanelPrefab);
                panel.SetInfo(SaveController.saves[i].SaveName, SaveController.saves[i].Date, SaveController.saves[i].LevelID, SaveController.saves[i].TexturePath, i);
                panel.GetComponent<Button>().onClick.AddListener(() => { panel.onClick(); });
                panel.transform.SetParent(SavePanel.transform);
            }
        }
        else if (SaveController.saves == null || SaveController.saves.Count == 0)
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
   void ClearSaveP()
    {
        List<Transform> transforms = new List<Transform>();
        Debug.Log(SavePanel);
        Debug.Log(SavePanel.transform);
        for (int i = 1; i < SavePanel.transform.childCount; i++)
        {
            transforms.Add(SavePanel.transform.GetChild(i));
        }
      
        for (int i = 0; i < transforms.Count; i++)
        {
            Destroy(transforms[i].gameObject);
        }
    }
    public void DeliteSaveButton()
    {
        SaveController.Instance.DeleteAllSaves();
    }
}
