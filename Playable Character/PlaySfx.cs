using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    public static PlaySfx Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySFX(AudioClip clip, AudioSource source = null, float volume = 1f, float pitch = 1f, bool loop = false)
    {
        if (Instance == null || clip == null) return;

        if (source != null)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            // 2D sound
            var src = Instance.gameObject.AddComponent<AudioSource>();
            src.clip = clip;
            src.volume = volume;
            src.pitch = pitch;
            src.spatialBlend = 0f;
            src.loop = loop;
            src.Play();
            Destroy(src, clip.length / pitch);
        }
    }

    public static void StopSFX(AudioSource source = null, AudioClip clip = null)
    {
        if (Instance == null) return;

        if (source != null)
        {
            if (clip == null || source.clip == clip)
                source.Stop();
            return;
        }

        var allSources = Instance.GetComponents<AudioSource>();
        foreach (var s in allSources)
        {
            if (s.loop && (clip == null || s.clip == clip))
            {
                s.Stop();
                Destroy(s);
            }
        }
    }
}
