using UnityEngine;

public class KeyboardRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 5f;

    void Update()
    {
        RotateWithKeyboard();
    }

    private void RotateWithKeyboard()
    {
        var horizontalInput = 0;
        var verticalInput = 0;

        if (Input.GetKey(KeyCode.J)) --horizontalInput;
        if (Input.GetKey(KeyCode.L)) ++horizontalInput;
        if (Input.GetKey(KeyCode.I)) ++verticalInput;
        if (Input.GetKey(KeyCode.K)) --verticalInput;
        if (Input.GetKeyDown(KeyCode.R)) transform.rotation = Quaternion.identity;

        var yaw = horizontalInput * rotationSpeed * Time.deltaTime;
        var pitch = verticalInput * rotationSpeed * Time.deltaTime;

        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.right, pitch);
    }
}