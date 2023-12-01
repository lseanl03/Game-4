using System.Collections;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using NUnit.Framework;

public abstract class BaseSave<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Load();
    }
    public virtual void OnApplicationQuit()
    {
        Save();
    }
    public virtual void Save()
    {
        string filePath = Application.persistentDataPath + "/" + GetType().Name + ".json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Dispose();
        }
        string jsonText = JsonUtility.ToJson(this);
        File.WriteAllText(filePath, jsonText);
    }
    public virtual void Load()
    {
        string filePath = Application.persistentDataPath + "/" + GetType().Name + ".json";
        if (File.Exists(filePath))
        {
            string jsonText = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonText, this);
        }
    }
}
