using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string objectText; // Text to display in the UI
    public float interactionRange = 3f; // Distance for interaction
    private Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assume the Player has a "Player" tag
    }

    public bool IsPlayerClose()
    {
        return Vector3.Distance(player.position, transform.position) <= interactionRange;
    }
}