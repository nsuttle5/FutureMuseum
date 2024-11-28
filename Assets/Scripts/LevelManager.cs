using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string sceneName;

    public string startingMenu;

    public GameObject optionsPanel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void returnMenu()
    {
        // Unfreeze all frozen game objects
        foreach (GameObject obj in GameObject.FindObjectsOfType<GameObject>())
        {
            if (obj.GetComponent<Rigidbody>() != null)
            {
                obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
        }

        // Unpause the game
        Time.timeScale = 1;

        // Load the starting menu scene
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
