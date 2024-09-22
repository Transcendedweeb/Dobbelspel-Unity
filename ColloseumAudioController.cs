using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColloseumAudioController : MonoBehaviour
{
    public AudioClip[] colosseumMusic;
    public AudioClip[] colosseumIntro;
    
    AudioSource musicSource;
    AudioSource introSource;

    void PlayRandomColosseumMusic()
    {
        int randomIndex = Random.Range(0, colosseumMusic.Length);
        AudioClip randomTrack = colosseumMusic[randomIndex];

        musicSource.clip = randomTrack;
        musicSource.Play();
    }

    void StartColosseumIntro()
    {
        int randomIndex = Random.Range(0, colosseumIntro.Length);
        AudioClip randomTrack = colosseumIntro[randomIndex];

        introSource.clip = randomTrack;
        introSource.Play();
    }

    IEnumerator StartSounds()
    {
        PlayRandomColosseumMusic();
        yield return new WaitForSeconds(1f);
        StartColosseumIntro();
    }

    void OnEnable()
    {
        AudioSource[] audioSources = this.GetComponents<AudioSource>();

        musicSource = audioSources[0];
        introSource = audioSources[1];

        StartCoroutine(StartSounds());
    }
}
