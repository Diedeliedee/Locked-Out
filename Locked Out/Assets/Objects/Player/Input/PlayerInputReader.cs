using UnityEngine;

public class PlayerInputReader : MonoBehaviour
{
    //  Properties:
    public float movementInput => m_input.DroneControls.Movement.ReadValue<float>();
    public Vector2 rotationInput => m_input.DroneControls.Rotation.ReadValue<Vector2>();
    public bool interactWasPressed => m_input.DroneControls.Interact.triggered;
    public bool toggleColorBlindModeWasPressed => m_input.General.ToggleColorBlindMode.triggered;

    //  References:
    private PlayerInputReceiver m_input = null;

    private void Awake()
    {
        m_input = new();
    }

    private void Start()
    {
        EnableInput();
    }

    public void EnableInput() => m_input.Enable();

    public void DisableInput() => m_input.Disable();
}
