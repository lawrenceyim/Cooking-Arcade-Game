using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int money;
    public static int day;
    public static int expense;
    public static int revenue;
    public static float musicVolumeSetting;
    public static float soundEffectVolumeSetting;


    [RuntimeInitializeOnLoadMethod]
    static void RunOnGameStart()
    {
        LoadData();
    }

    public static void SaveData() {
        ES3.Save("money", money);
        ES3.Save("day", day);
    }

    public static void LoadData() {
        money = ES3.Load("money", 1000);
        day = ES3.Load("day", 0);
        musicVolumeSetting = ES3.Load("musicVolumeSetting", .1f);
        soundEffectVolumeSetting = ES3.Load("soundEffectVolumeSetting", .2f);
    }

    public static void ResetSave() {
        ES3.DeleteFile("SaveFile.es3");
    }

    public static bool HasEnoughMoney(int amount) {
        return money >= amount;
    }

    public static void IncrementDay() {
        day++;
    }

    public static void StartDay() {
        revenue = 0;
        expense = 0;
        Time.timeScale = 1f;
    }

    public static void IncreaseMoney(int amount) {
        money += amount;
        revenue += amount;
    }

    public static void DecreaseMoney(int amount) {
        money -= amount;
        expense += amount;
    }
}
