using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; //{ get; private set; }
    AudioSource source;
    public bool isMuted = false;

    private void Awake()
    {
        instance = this;

        source = GetComponent<AudioSource>();
    }

    private void Start()
    {
        isMuted = source.mute;
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }

    public void SoundOn()
    {
        isMuted = !isMuted;
        source.mute = isMuted;
    }

    public void AgainButtonFunction()
    {
        SceneManager.LoadScene(1);
    }
}
