using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSourcePre;
    public static SoundManager soundInstance;
    [SerializeField] GameObject currentMusicObj;
    [SerializeField] AudioClip titleMenuMusic;

    void Start()
    {
        if(soundInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            soundInstance = this;
        }

        PlayMusic(titleMenuMusic);
    }

    void Update()
    {

    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource insta = Instantiate(audioSourcePre);
        insta.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        if(currentMusicObj != null)
        {
            Debug.Log("destroy?");
            Destroy(currentMusicObj);
        }
        AudioSource insta = Instantiate(audioSourcePre);
        currentMusicObj = insta.gameObject;
        insta.PlayOneShot(musicClip);
        insta.loop = true;
    }
}
