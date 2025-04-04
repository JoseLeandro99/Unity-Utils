using System;
using System.IO;
using UnityEngine;

#region EXAMPLES
[Serializable]
public class SaveDataExampleClass
{
    public string GameInfo;
    public string DateInfo;
}
#endregion

public static class JsonSaver<T>
{
    private static string savePath = string.Format("{0}/savedata.json", Application.persistentDataPath);

    public static void Save(T data)
    {
        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

        Debug.Log("savedata successfully saved to " + savePath);
    }

    public static T Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            T data = JsonUtility.FromJson<T>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("savedata not found in " + savePath);
            return default;
        }
    }

    public static void Delete()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("savedata successfully deleted");
        }
    }
}

