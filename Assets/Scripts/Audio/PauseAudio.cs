using UnityEngine;

public class PauseAudio : MonoBehaviour, Observer
{
    [SerializeField] AudioSource[] audioSources;
    bool isPaused;

    public void Start() {
        isPaused = GameState.IsGameIsPaused();
    }

    public void PauseAudioSource() {
        foreach (AudioSource audioSource in audioSources) {
            audioSource.Pause();
        }
    }

    public void UnpauseAudioSource() {
        foreach (AudioSource audioSource in audioSources) {
            audioSource.UnPause();
        }
    }

    public void ReceiveUpdate()
    {
        if (GameState.IsGameIsPaused()) {
            PauseAudioSource();
        } else {
            UnpauseAudioSource();
        }
    }
}
