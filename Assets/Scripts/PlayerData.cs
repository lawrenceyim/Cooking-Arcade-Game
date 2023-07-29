using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int money;


    [RuntimeInitializeOnLoadMethod]
    static void RunOnGameStart()
    {
        if (File.Exists(Application.dataPath + "/save.txt")) {
            LoadData();
        } else {
            ResetSave();
        }
    }

    public static void SaveData() {
        SaveObject saveObject = new SaveObject();
        saveObject.money = money;
        string json = JsonUtility.ToJson(saveObject);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    public static void LoadData() {
        string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
        money = saveObject.money;
    }

    public static void ResetSave() {
        money = 0;
        SaveData();
    }

    private class SaveObject {
        public int money;
    }
}
