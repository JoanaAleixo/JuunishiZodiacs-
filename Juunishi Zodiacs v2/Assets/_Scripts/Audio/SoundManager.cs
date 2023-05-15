using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] public static AudioClip slashSound, jumpSound, bomSound, starSound, posionSound, bigStarSound, spikesSound, foot1Sound, foot2Sound, waterSound;
    static AudioSource audioSrc;

    void Start()
    {
        slashSound = Resources.Load<AudioClip>("Slash");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(AudioClip clip)
    {
        audioSrc.PlayOneShot(clip);
    }
}
