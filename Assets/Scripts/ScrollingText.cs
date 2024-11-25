using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using System.Collections;

public class ScrollingText : MonoBehaviour
{
    public TMP_Text uiText; // Reference to the TMP_Text component
    private string fullText; // Full text to display
    private Coroutine scrollingCoroutine; // Reference to the coroutine

    public float scrollSpeed = 0.05f; // Time between each character reveal

    private void Awake()
    {
        if (uiText == null)
            uiText = GetComponent<TMP_Text>();
    }

    public void StartScrolling(string newText)
    {
        fullText = newText;
        uiText.text = ""; // Clear the text initially

        if (scrollingCoroutine != null)
        {
            StopCoroutine(scrollingCoroutine); // Stop any ongoing scrolling
        }

        scrollingCoroutine = StartCoroutine(ScrollText());
    }

    private IEnumerator ScrollText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            uiText.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(scrollSpeed);
        }

        scrollingCoroutine = null; // Reset the coroutine reference after completion
    }
}