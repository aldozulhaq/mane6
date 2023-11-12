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
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        PlayMusic("Main");
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
            sfxSource.pitch = Random.Range(0.7f, 1.4f);
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