using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;      // Singleton instance.
    public AudioClip defaultClip;             // Audio to play on Awake.
    private AudioSource audioSource;
    private bool isFading = false;

    private void Awake()
    {
        // Singleton pattern to ensure one AudioManager instance exists.
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persist across scenes.
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Add an AudioSource component and configure it.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0.5f;

        // Play the default clip (Play-on-Awake audio) if it exists.
        if (defaultClip != null)
        {
            audioSource.clip = defaultClip;
            audioSource.Play();
        }
    }

    public void PlayMusic(AudioClip newClip, float fadeDuration = 0.5f)
    {
        if (isFading) return;  // Prevent overlapping fades.

        // Avoid restarting if the same clip is already playing.
        if (audioSource.clip == newClip && audioSource.isPlaying) return;

        StartCoroutine(SmoothTransition(newClip, fadeDuration));
    }

    private IEnumerator SmoothTransition(AudioClip newClip, float fadeDuration)
    {
        // Preload the new clip to avoid stuttering.
        if (newClip.loadState != AudioDataLoadState.Loaded)
        {
            newClip.LoadAudioData();
            while (newClip.loadState != AudioDataLoadState.Loaded)
            {
                yield return null;
            }
        }

        isFading = true;
        float startVolume = audioSource.volume;

        // Fade out the current music.
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Switch to the new clip.
        audioSource.Stop();
        audioSource.clip = newClip;
        if (newClip != null) audioSource.Play();
        audioSource.volume = 0;

        // Fade in the new music.
        while (audioSource.volume < startVolume)
        {
            audioSource.volume += startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        isFading = false;
    }

    public void FadeOutMusic(float fadeDuration = 0.5f)
    {
        if (isFading) return;  // Prevent overlapping fades.

        StartCoroutine(FadeOutCoroutine(fadeDuration));
    }

    private IEnumerator FadeOutCoroutine(float fadeDuration)
    {
        isFading = true;
        float startVolume = audioSource.volume;

        // Fade out the music.
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume for reuse.
        isFading = false;
    }
}
