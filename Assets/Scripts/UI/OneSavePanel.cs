using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OneSavePanel : MonoBehaviour
{
    int levelID;
    public void SetInfo(string name, string date, int levelId,string path)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"{name}\n{date}";
        
           
        
        GetComponentInChildren<Image>().sprite = Sprite.Create(SaveController.Instance.LoadTexture(path),
                new Rect(0, 0, 1920, 1080),
                  Vector2.zero, 100);
        levelID = levelId;
        
    }
    public void onClick()
    {
        SceneManager.LoadScene(levelID);
    }

}
