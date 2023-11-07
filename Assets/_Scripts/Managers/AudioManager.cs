using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgmSource, sfxSource;
    [SerializeField] private Audio[] bgmClips, sfxClips;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Don't destroy this object when loading a new scene
        }
        else
            Destroy(gameObject);
    }

    public void PlayMusic(string musicName)
    {
        Audio audio = System.Array.Find(bgmClips, a => a.name == musicName);
        if (audio == null)
        {
            Debug.LogWarning("Music: " + musicName + " not found!");
            return;
        }
        else
        {
            bgmSource.clip = audio.clip;
            bgmSource.Play();
        }
    }

    public void PlaySFX(string sfxName)
    {
        Audio audio = System.Array.Find(bgmClips, a => a.name == sfxName);
        if (audio == null)
        {
            Debug.LogWarning("SFX: " + sfxName + " not found!");
            return;
        }
        else
        {
            sfxSource.PlayOneShot(audio.clip);
        }
    }
}

[System.Serializable]
public class Audio
{
    public string name;
    public AudioClip clip;
}