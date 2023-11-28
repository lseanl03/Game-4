using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public class Axie
{
    public AxieName axieName;
    public float speed;
    public float rateFire;
    public int health;
    public int attackDamage;
}
public class AxieListData : BaseSave<AxieListData>
{
    public List<Axie> axieList;
    private AxieManager axieManager => AxieManager.instance;
    public override void Save()
    {
        foreach (Axie axie in axieList)
        {
            string filePath = Application.persistentDataPath + "/" + axie.axieName.ToString() + ".json";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
                Debug.Log("create file" + GetType().Name);
            }
            string jsonText = JsonUtility.ToJson(axie);
            File.WriteAllText(filePath, jsonText);
        }
    }
    public override void Load()
    {
        foreach (SelectAxie selectAxie in axieManager.axieList)
        {
            Axie axie = new Axie();
            axieList.Add(axie);
            string filePath = Application.persistentDataPath + "/" + selectAxie.axieName.ToString() + ".json";
            if (File.Exists(filePath))
            {
                string jsonText = File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(jsonText, axie);
            }
        }
    }
    public bool FileAlreadyExists(SelectAxie selectAxie)
    {
        Debug.Log(axieList.Count);
        foreach(var axie in axieList)
        {
            if(axie.axieName == selectAxie.axieName)
            {
                string filePath = Application.persistentDataPath + "/" + axie.axieName.ToString() + ".json";
                if (File.Exists(filePath))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
