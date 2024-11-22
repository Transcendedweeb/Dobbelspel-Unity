using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasinoMusicPlayer : MonoBehaviour
{
    public AudioClip[] audioClips;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRandomClip()
    {

        int randomIndex = Random.Range(0, audioClips.Length);
        AudioClip randomClip = audioClips[randomIndex];

        audioSource.clip = randomClip;
        audioSource.Play();

    }
}
