using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColloseumIntro : MonoBehaviour
{
    public AudioClip[] colosseumMusic;
    public AudioClip[] colosseumIntro;
    
    private AudioSource musicSource;
    private AudioSource introSource;

    void PlayRandomColosseumMusic()
    {
        if (colosseumMusic.Length > 0)
        {
            int randomIndex = Random.Range(0, colosseumMusic.Length);
            AudioClip randomTrack = colosseumMusic[randomIndex];

            musicSource.clip = randomTrack;
            musicSource.Play();
        }
        else
        {
            Debug.LogError("colosseumMusic array is empty!");
        }
    }

    void StartColosseumIntro()
    {
        if (colosseumIntro.Length > 0)
        {
            int randomIndex = Random.Range(0, colosseumIntro.Length);
            AudioClip randomTrack = colosseumIntro[randomIndex];

            introSource.clip = randomTrack;
            introSource.Play();
        }
        else
        {
            Debug.LogError("colosseumIntro array is empty!");
        }
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
