
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public Transform playerBody;
    public float sensitivity = 0.2f;
    public float playerRadius = 0.4f;
    public LayerMask obstacleMask;

    private float xRotation;
    private float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (Mouse.current == null || Keyboard.current == null)
            return;
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        CameraAxis();
    }

    private void CameraAxis()
    {
        if (Cursor.lockState != CursorLockMode.Locked)
            return;
        Vector2 mouse = Mouse.current.delta.ReadValue();
        float mouseX = mouse.x * sensitivity;
        float mouseY = mouse.y * sensitivity;
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation =
            Quaternion.Euler(xRotation, 0f, 0f);
        if (playerBody != null)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }
}
