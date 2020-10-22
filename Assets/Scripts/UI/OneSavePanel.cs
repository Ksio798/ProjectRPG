using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OneSavePanel : MonoBehaviour
{
    public Image image;
    int levelID;
    int num;
    public static int SaveNum = -1;
    public void SetInfo(string name, string date, int levelId,string path, int saveNum)
    {
        GetComponentInChildren<TextMeshProUGUI>().text = $"{name}\n{date}";



       
        image.sprite = Sprite.Create(SaveController.Instance.LoadTexture(path), new Rect(0, 0, 1920, 1080), Vector2.one * 0.5f);
        num = saveNum;
        levelID = levelId;
    }
    public void onClick()
    {
        GameController.ActiveLevelID = levelID;
        SaveNum = num;
        SceneManager.LoadScene(levelID);
    }

}
