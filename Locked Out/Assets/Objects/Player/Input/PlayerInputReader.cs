using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    //  Properties:
    public float movementInput => m_input.DroneControls.Movement.ReadValue<float>();
    public Vector2 rotationInput => m_input.DroneControls.Rotation.ReadValue<Vector2>();
    public Quaternion gyroOrientationInput => m_gyroInput != null ? m_gyroInput.GetOrientation() : Quaternion.identity;
    public bool interactWasPressed => m_input.DroneControls.Interact.triggered;
    public bool toggleColorBlindModeWasPressed => m_input.General.ToggleColorBlindMode.triggered;

    //  References:
    private PlayerInputReceiver m_input = null;
    private IGyroTransmitter m_gyroInput = null;

    private void Awake()
    {
        m_input = new();
        SetupGyroTransmitter();
    }

    private void Start()
    {
        EnableInput();
    }

    public void EnableInput() => m_input.Enable();

    public void DisableInput() => m_input.Disable();

    private void SetupGyroTransmitter()
    {
        m_gyroInput = new JoyconRotationTransmitter();

        if (m_gyroInput.Setup()) return;

        Debug.LogWarning("No joycons found! Switching to controller gyro controls!");
        m_gyroInput = new DSRotationTransmitter();

        if (m_gyroInput.Setup()) return;

        Debug.LogWarning("No controller found. Gyro input is not working!");
    }
}
