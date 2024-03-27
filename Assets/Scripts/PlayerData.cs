using ES3Types;
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
    public static bool saveFileExists;
    public static bool[] stars;

    [RuntimeInitializeOnLoadMethod]
    static void RunOnGameStart() {
        LoadData();
        Screen.SetResolution(xResolution, yResolution, false);
    }

    public static void SaveData() {
        ES3.Save(Es3Values.SAVE_FILE_EXISTS, saveFileExists);
        ES3.Save(Es3Values.MONEY, money);
        ES3.Save(Es3Values.DAY, day);
        ES3.Save(Es3Values.MUSIC_VOLUME, musicVolumeSetting);
        ES3.Save(Es3Values.SOUND_EFFECT_VOLUME, soundEffectVolumeSetting);
        ES3.Save(Es3Values.X_RESOLUTION, xResolution);
        ES3.Save(Es3Values.Y_RESOLUTION, yResolution);
        ES3.Save(Es3Values.STARS, stars);
    }

    public static void LoadData() {
        saveFileExists = ES3.Load(Es3Values.SAVE_FILE_EXISTS, false);
        money = ES3.Load(Es3Values.MONEY, defaultMoney);
        day = ES3.Load(Es3Values.DAY, 1);
        musicVolumeSetting = ES3.Load(Es3Values.MUSIC_VOLUME, .1f);
        soundEffectVolumeSetting = ES3.Load(Es3Values.SOUND_EFFECT_VOLUME, .2f);
        xResolution = ES3.Load(Es3Values.X_RESOLUTION, 1280);
        yResolution = ES3.Load(Es3Values.Y_RESOLUTION, 720);
        stars = ES3.Load(Es3Values.STARS, new bool[21]);
    }

    public static void ResetSave() {
        // ES3.DeleteFile("SaveFile.es3");
        ES3.Save(Es3Values.SAVE_FILE_EXISTS, false);
        ES3.Save(Es3Values.MONEY, defaultMoney);
        ES3.Save(Es3Values.DAY, 1);
        ES3.Save(Es3Values.PIZZA_LETTER_PLAYED, false);
        ES3.Save(Es3Values.SALAD_LETTER_PLAYED, false);
        LoadData();
    }

    public static bool HasEnoughMoney(int amount) {
        return money >= amount;
    }

    public static void IncrementDay() {
        day++;
    }

    public static void DecrementDay() {
        day--;
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
