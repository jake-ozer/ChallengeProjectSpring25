using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public float xSens;
    public float ySens;

    private float xRot;
    private float yRot;
    private PlayerInput input;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        input = transform.parent.GetComponent<PlayerInput>();
    }

    private void Update()
    {
        Vector2 look = input.actions["Look"].ReadValue<Vector2>();

        float lookX = look.x * xSens;
        float lookY = look.y * ySens;

        xRot -= lookY;
        xRot = Mathf.Clamp(xRot, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRot, yRot, 0);
        transform.parent.Rotate(Vector3.up * lookX);
    }
}
