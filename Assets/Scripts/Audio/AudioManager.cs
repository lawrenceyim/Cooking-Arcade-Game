using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioClip addIngredient;
    [SerializeField] AudioClip coinSound;
    [SerializeField] AudioClip trashSound;
    [SerializeField] AudioSource audioSource;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }    
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (addIngredient == null) {
            Debug.LogError("addIngredient audioclip is null");
        }
        if (coinSound == null) {
            Debug.LogError("coinSound audioclip is null");
        }
        if (trashSound == null) {
            Debug.LogError("trashSound audioclip is null");
        }
        if (audioSource == null) {
            Debug.LogError("audiosource is null");
        }
    }

    public void PlayAddIngredientSound() {
        audioSource.clip = addIngredient;
        audioSource.Play();
    }

    public void PlayCoinSound() {
        audioSource.clip = coinSound;
        audioSource.Play();
    }

    public void PlayTrashSound() {
        audioSource.clip = trashSound;
        audioSource.Play();
    }
}
