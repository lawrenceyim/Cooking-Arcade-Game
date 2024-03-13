using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState {
    public static bool isPaused = false;

    public static void PauseGame() {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public static void UnpauseGame() {
        isPaused = false;
        Time.timeScale = 1f;
    }

    public static bool IsGameIsPaused() {
        return isPaused;
    }
}
