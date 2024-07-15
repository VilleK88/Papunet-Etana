using UnityEngine;
public class SoundManager : MonoBehaviour
{
    #region Singleton
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        source = GetComponent<AudioSource>();
        isMuted = PlayerPrefs.GetInt("isMuted", 0) == 1;
        source.mute = isMuted;
    }
    #endregion
    [HideInInspector] public AudioSource source;
    public bool isMuted = false;
    public void PlaySound(AudioClip sound)
    {
        if (!isMuted)
            source.PlayOneShot(sound);
    }
}