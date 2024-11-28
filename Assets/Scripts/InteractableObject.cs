using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public float interactionRange = 3f; // Distance for interaction
    private Transform player;

    public string[] objectTexts; // Array of texts to display in the UI
    public int currentTextIndex = 0;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assume the Player has a "Player" tag
    }

    public bool IsPlayerClose()
    {
        return Vector3.Distance(player.position, transform.position) <= interactionRange;
    }

    public void Interact()
    {
        if (objectTexts.Length == 0) return;

        // Display the current text
        Debug.Log(objectTexts[currentTextIndex]);

        // Move to the next text
        currentTextIndex = (currentTextIndex + 1) % objectTexts.Length;
    }
}