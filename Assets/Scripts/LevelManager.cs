using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public string sceneName;
    public string startingMenu;
    public GameObject optionsPanel;
    public AudioSource audioSource; // Assign the AudioSource in the Inspector
    public float fadeDuration = 0.5f; // Duration of the fade in seconds

    // Fade audio and change scene
    public void changeScene()
    {
        StartCoroutine(FadeOutAndChangeScene());
    }

    private IEnumerator FadeOutAndChangeScene()
    {
        if (audioSource != null)
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

        SceneManager.LoadScene(sceneName);
    }

    public void returnMenu()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.FadeOutMusic(0.5f); // Adjust fadeDuration as needed.
        }

        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        Time.timeScale = 1;
        SceneManager.LoadScene(startingMenu);
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }


}
