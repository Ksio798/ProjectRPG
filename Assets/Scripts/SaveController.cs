﻿using System;
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
    //public Texture2D SaveImagine;
    public string SaveName;
    public string Date;
    public int LevelID;
    public string TexturePath;

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
    public void CreateSave(int levelId, string SaveName, string date, string TexturePath)
    {
        SaveData newSD = new SaveData();
        newSD.LevelID = levelId;
        newSD.SaveName = SaveName;
        newSD.TexturePath = TexturePath;
       // newSD.SaveImagine = image;
        newSD.Date = date;
        saves.Add(newSD);
        if (saves.Count>8)
        {
            saves.RemoveAt(0);
        }
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
    public void DeleteAllSaves()
    {
        saves.Clear();
    }

    public void DeleteSave(int levelId)
    {

        int index = saves.FindIndex(x => x.LevelID == levelId);

        if (index != -1)
            saves.RemoveAt(index);
    }
    public Texture2D LoadTexture(string imgName)
    {
        var filePath = getTextureFilePath(imgName);
        if (File.Exists(filePath))
        {
        Debug.Log(filePath);
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(1920, 1080, TextureFormat.ARGB32, true);
            tex.LoadImage(bytes);
            tex.Apply();
            return tex;
        }
        return null;
    }
           
           

            
    public string getTextureFilePath(string imgName)
    {

         //string filePath = Path.Combine(Application.persistentDataPath, imgName + ".png");
       string filePath = Path.Combine(Application.persistentDataPath, imgName );
        return filePath;

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