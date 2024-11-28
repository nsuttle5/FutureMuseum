using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class InteractionSystem : MonoBehaviour
{
    public GameObject uiPanel; // The UI Panel
    public ScrollingText scrollingText;
    public TextMeshProUGUI uiText; // UI TextMeshProUGUI component
    public float interactionDistance = 3f; // Interaction distance
    private bool isUiActive = false;
    private InteractableObject currentObject;

    void Update()
    {
        if (isUiActive && Input.GetKeyDown(KeyCode.E)) // Handle UI interaction
        {
            if (currentObject != null && currentObject.currentTextIndex < currentObject.objectTexts.Length - 1)
            {
                // Show next text
                currentObject.currentTextIndex++;
                uiText.text = currentObject.objectTexts[currentObject.currentTextIndex];
                scrollingText.StartScrolling(currentObject.objectTexts[currentObject.currentTextIndex]);
            }
            else
            {
                // Close UI
                uiPanel.SetActive(false);
                uiText.gameObject.SetActive(false);
                isUiActive = false;
            }
        }
        else if (!isUiActive && Input.GetKeyDown(KeyCode.E)) // Open UI
        {
            CheckForInteractable();
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionDistance)) // Use interactionDistance
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null && interactable.IsPlayerClose())
            {
                currentObject = interactable;
                currentObject.currentTextIndex = 0; // Reset text index
                uiText.text = currentObject.objectTexts[currentObject.currentTextIndex];
                uiPanel.SetActive(true);
                uiText.gameObject.SetActive(true);
                isUiActive = true;

                scrollingText.StartScrolling(currentObject.objectTexts[currentObject.currentTextIndex]);
            }
        }
    }
}