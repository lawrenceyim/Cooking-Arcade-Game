using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState {
    public static bool isPaused = false;

    public static void pauseGame() {
        isPaused = true;
    }

    public static void unpauseGame() {
        isPaused = false;
    }

    public static bool gameIsPaused() {
        return isPaused;
    }
}
