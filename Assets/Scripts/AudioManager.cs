using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private AudioSource src;
    public AudioClip slashStartSound;
    public AudioClip slashingSound;
    public AudioClip slashEndSound;
    public AudioClip fishSliceSound;
    public AudioClip bombExplosionSound; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            src = GetComponent<AudioSource>();
            if (src == null)
            {
                src = gameObject.AddComponent<AudioSource>();
                src.playOnAwake = false;
                src.spatialBlend = 0f;
                src.volume = 1f;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySlashStart()
    {
        if (slashStartSound != null && src != null)
        {
            src.clip = slashStartSound;
            src.Play();
        }
    }

    public void PlaySlashing()
    {
        if (slashingSound != null && src != null && !src.isPlaying)
        {
            src.clip = slashingSound;
            src.Play();
        }
    }

    public void PlaySlashEnd()
    {
        if (slashEndSound != null && src != null)
        {
            src.clip = slashEndSound;
            src.Play();
        }
    }

    public void PlayFishSlice()
    {
        if (fishSliceSound != null && src != null)
        {
            src.PlayOneShot(fishSliceSound, 1f);
        }
    }

        public void PlayBombExplosion()
    {
        if (bombExplosionSound != null && src != null)
        {
            src.PlayOneShot(bombExplosionSound, 1f);
        }
    }

}