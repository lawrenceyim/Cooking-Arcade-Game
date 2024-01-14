using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData {
    public static int money;
    public static int day;
    public static int expense;
    public static int revenue;
    public static float musicVolumeSetting;
    public static float soundEffectVolumeSetting;
    public static int defaultMoney = 1000;
    public static int xResolution;
    public static int yResolution;

    [RuntimeInitializeOnLoadMethod]
    static void RunOnGameStart() {
        LoadData();
        Screen.SetResolution(xResolution, yResolution, false);
    }

    public static void SaveData() {
        ES3.Save("money", money);
        ES3.Save("day", day);
        ES3.Save("musicVolumeSetting", musicVolumeSetting);
        ES3.Save("soundEffectVolumeSetting", soundEffectVolumeSetting);
        ES3.Save("xResolution", xResolution);
        ES3.Save("yResolution", yResolution);
    }

    public static void LoadData() {
        money = ES3.Load("money", defaultMoney);
        day = ES3.Load("day", 1);
        musicVolumeSetting = ES3.Load("musicVolumeSetting", .1f);
        soundEffectVolumeSetting = ES3.Load("soundEffectVolumeSetting", .2f);
        xResolution = ES3.Load("xResolution", 1280);
        yResolution = ES3.Load("yResolution", 720);
    }

    public static void ResetSave() {
        // ES3.DeleteFile("SaveFile.es3");
        ES3.Save("money", defaultMoney);
        ES3.Save("day", 1);
        ES3.Save("pizzaLetterIntroPlayed", false);
        ES3.Save("saladLetterIntroPlayed", false);
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
