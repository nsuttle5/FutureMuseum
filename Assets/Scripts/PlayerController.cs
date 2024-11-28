using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (optionsPanel == null)
        {
            Debug.LogWarning("Options Panel is not assigned in the Inspector");
        }
    }

    public Rigidbody playerRigidbody;
    public float speed = 5.0f;
    public float sprintSpeed = 10.0f;
    public float mouseSensitivity = 100.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public GameObject optionsPanel;

    // Update is called once per frame
    void Update()
    {
        // Handle camera rotation
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0.0f);

        // Handle options panel toggle
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // FixedUpdate is called once per physics frame
    void FixedUpdate()
    {
        // Handle player movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Ensure the movement is only on the XZ plane
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Check if the sprint key is pressed
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : speed;

        Vector3 movement = forward * moveVertical + right * moveHorizontal;
        playerRigidbody.MovePosition(playerRigidbody.position + movement * currentSpeed * Time.fixedDeltaTime);
    }

    void PauseGame()
    {
        optionsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; // Pause the game
    }

    public void ResumeGame()
    {
        optionsPanel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f; // Resume the game
    }
}
