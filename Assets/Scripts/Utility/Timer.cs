using System;
using UnityEngine;

public class Timer : MonoBehaviour {
    private Action timerCallback;
    private float timer;

    public void SetTimer(float timer, Action timerCallback) {
        this.timer = timer;
        this.timerCallback = timerCallback;
    }

    private void Update() {
        if (timer > 0f) {
            timer -= Time.deltaTime;
            if (IsTimerComplete()) {
                timerCallback();
            }
        }
    }

    public bool IsTimerComplete() {
        return timer <= 0f;
    }
}