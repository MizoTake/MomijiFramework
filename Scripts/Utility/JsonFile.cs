using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFile
{

    private const string FOLDER = "MomijiFramework";

    public static void Write<T>(string fileName, T data) where T : class
    {
        var path = Application.persistentDataPath + "/" + FOLDER;
        Debug.Log(path);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path + "/" + fileName + ".json", json, System.Text.Encoding.UTF8);
        Debug.Log(path + "/" + fileName + ".json");
    }

    public static T Read<T>(string fileName) where T : class
    {
        var path = Application.persistentDataPath + "/" + FOLDER + "/" + fileName + ".json";
        Debug.Log(path);

        if (!File.Exists(path))
        {
            return null;
        }
        string json = File.ReadAllText(path, System.Text.Encoding.UTF8);
        return JsonUtility.FromJson<T>(json);
    }

    public static bool CheckFile(string fileName)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/" + FOLDER))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/" + FOLDER);
        }
        return File.Exists(Application.persistentDataPath + "/" + FOLDER + "/" + fileName + ".json");
    }
}