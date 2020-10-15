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

    public string SaveName;
    public string Date;
    public int LevelID;
    public string TexturePath;
    public float PlayerPosX;
    public float PlayerPosY;
    public int PlayerType;
    public statsToSave statsEgor;
    public statsToSave statsDima;
    public statsToSave statsMax;
    public statsToSave statsAlex;
    public InventoryToSave InvEgor;
    public InventoryToSave InvDima;
    public InventoryToSave InvMax;
    public InventoryToSave InvAlex;
    public CarInvToSave carInv;
    public Vector2D[] ObjToDestroy;



}
[System.Serializable]
public struct statsToSave
{
    public float Speed;
    public int SpeedCountUpdate;
    public float Damage;
    public int DamageCountUpdate;
    public float MaxHealth;
    public int HealthCountUpdate;
    public float damageResistanceInPercent;
    public int damageResistanceInPercentCountUpdate;
    public float manna;
    public float MaxManna;
    public float MannaEarnPerSecond;
    public float health;
    public int AmmoCountUpdate;
    public int MedChestCountUpdate;
}
[System.Serializable]
public struct InventoryToSave
{
    public int MaxMedicineChestCount;
    public int HealingPercentByMedicineChest;
    public int MaxAmmo;
    public int currentAmmo;
    public float ShildCount;
    public int medicineChestCount;
}
[System.Serializable]
public struct CarInvToSave
{
    public int MoneyCount;
    public int MutagenCount;
    public int SanorinCount;
    public int MedChestCount;
    public int BulletsCount;
}
[System.Serializable]
public struct Vector2D
{
    public float X;
    public float Y;
}

public class SaveController : MonoBehaviour
{
    public string FileName = "saves.svs";
    public static List<SaveData> saves = new List<SaveData>();
    public static SaveController Instance;
    public List<Vector2D> ObjToDesrtoy = new List<Vector2D>();


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
    
    public void CreateSave(int levelId, string SaveName, string date, string TexturePath, Vector2 pos, int playerType,
        Stats EgorS, Stats DimaS, Stats MaxS, Stats AlexS, Inventory EgorInv, Inventory DimaInv, Inventory MaxInv, Inventory AlexInv)
    {
        SaveData newSD = new SaveData();
        newSD.LevelID = levelId;
        newSD.SaveName = SaveName;
        newSD.TexturePath = TexturePath;
        newSD.Date = date;
        newSD.PlayerPosX = pos.x;
        newSD.PlayerPosY = pos.y;
        newSD.PlayerType = playerType;
        newSD.statsEgor = SaveHelper.CreateStructStats(EgorS);
        newSD.statsDima = SaveHelper.CreateStructStats(DimaS);
        newSD.statsMax = SaveHelper.CreateStructStats(MaxS);
        newSD.statsAlex = SaveHelper.CreateStructStats(AlexS);
        newSD.InvEgor = SaveHelper.CreateSctructInv(EgorInv); ;
        newSD.InvDima = SaveHelper.CreateSctructInv(DimaInv);
        newSD.InvMax = SaveHelper.CreateSctructInv(MaxInv);
        newSD.InvAlex = SaveHelper.CreateSctructInv(AlexInv);
        newSD.carInv = SaveHelper.CreateSctructCarInv();
        newSD.ObjToDestroy = SaveHelper.CreateM();
        saves.Add(newSD);
        if (saves.Count > 8)
        {
            DeleteFile(saves[0].TexturePath);
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
        DeleteFile(getFilePath());

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
            // Texture2D tex = new Texture2D(1920, 1080, TextureFormat.ARGB32, true);
            Texture2D tex = new Texture2D(1920, 1080);
            tex.LoadImage(bytes);
            // tex.Apply();
            return tex;
        }
        return null;
    }




    public string getTextureFilePath(string imgName)
    {


        string filePath = Path.Combine(Application.persistentDataPath, imgName);
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
    void DeleteFile(string FilePath)
    {
        File.Delete(FilePath);
    }

}
