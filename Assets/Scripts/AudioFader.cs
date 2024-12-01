using UnityEngine;
using System.Collections;

public class AudioFader : MonoBehaviour
{
    public AudioSource audioSource; // Assign your AudioSource here
    public float fadeDuration = 0.5f; // Duration of the fade in seconds

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void FadeOutAndStop()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    private IEnumerator FadeOutCoroutine()
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset for potential reuse
    }
}
