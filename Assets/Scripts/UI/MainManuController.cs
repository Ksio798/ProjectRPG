using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainManuController : MonoBehaviour
{
    public GameObject SavePanel;
    public GameObject SettingsPanel;
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
        Application.Quit();
    }
    IEnumerator WaitToLoad(int index)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(index);
    }
   

}
