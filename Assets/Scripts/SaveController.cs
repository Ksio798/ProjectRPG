using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class SaveData
{
    public Texture2D SaveImagine;
    public string SaveName;
    public string Date;
    public int LevelID;

}
public class SaveController : MonoBehaviour
{
    public string FileName = "saves.svs";
    public static List<SaveData> saves = new List<SaveData>();
    public static SaveController Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void CreateSave(int levelId, string SaveName, Texture2D image, string date)
    {
        SaveData newSD = new SaveData();
        newSD.LevelID = levelId;
        newSD.SaveName = SaveName;

        newSD.SaveImagine = image;
        newSD.Date = date;
        saves.Add(newSD);

    }
    public void SaveData()
    {

        BinaryFormatter bf = new BinaryFormatter();

        string path = getFilePath();
        if (File.Exists(path))
        {
            FileStream file = File.OpenWrite(path);
            bf.Serialize(file, saves);
            file.Close();
        }
        else
        {
            FileStream file = File.Create(path);
            bf.Serialize(file, saves);
            file.Close();
        }






    }
    public void LoadData()
    {

        BinaryFormatter bf = new BinaryFormatter();


        string path = getFilePath();
        if (File.Exists(path))
        {
            FileStream file = File.Open(path, FileMode.Open);
            saves = (List<SaveData>)bf.Deserialize(file);

            file.Close();
        }



    }
    string getFilePath()
    {
        //        string path = Application.dataPath + "\\" + FileName;
        //#if UNITY_ANDROID
        //        path = "jar:file://" + Application.dataPath + "!/assets/" + FileName;

        //#endif

        string filePath = Path.Combine(Application.persistentDataPath, FileName);
        return filePath;

    }
}
