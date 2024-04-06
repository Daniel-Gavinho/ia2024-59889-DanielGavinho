using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; }
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(int index, float time) {
        audioSource.clip = musicClips[index];
        audioSource.Play();
        Invoke("StopSound", time);
    }

    public void StopSound() {
        audioSource.Stop();
    }
}
