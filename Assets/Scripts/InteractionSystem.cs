using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class InteractionSystem : MonoBehaviour
{
    public GameObject uiPanel; // The UI Panel
    public TextMeshProUGUI uiText; // UI TextMeshProUGUI component
    private bool isUiActive = false;
    private InteractableObject currentObject;

    void Update()
    {
        if (isUiActive && Input.GetKeyDown(KeyCode.E)) // Close UI
        {
            uiPanel.SetActive(false);
            uiText.gameObject.SetActive(false);
            isUiActive = false;
        }
        else if (!isUiActive && Input.GetKeyDown(KeyCode.E)) // Open UI
        {
            CheckForInteractable();
        }
    }

    private void CheckForInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f)) // 3f is your interaction distance
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null && interactable.IsPlayerClose())
            {
                currentObject = interactable;
                uiText.text = currentObject.objectText;
                uiPanel.SetActive(true);
                uiText.gameObject.SetActive(true);
                isUiActive = true;
            }
        }
    }
}