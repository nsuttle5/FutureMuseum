using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public GameObject player;
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    // Update is called once per frame
    void Update()
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

        Vector3 movement = forward * moveVertical + right * moveHorizontal;
        player.transform.Translate(movement * speed * Time.deltaTime, Space.World);

        // Handle camera rotation
        rotationX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        rotationY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90.0f, 90.0f);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0.0f);
    }
}
