using Joeri.Tools.Movement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 1f;
    [SerializeField] private float m_moveGrip = 1f;
    [Space]
    [SerializeField] private float m_turnSpeed = 1f;
    [SerializeField] private float m_turnGrip = 1f;

    private CharacterController m_characterController;
    private Accel.Singular m_movementAcceleration = new();
    private Accel.Singular m_rotationAcceleration = new();

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        var input = GetInput();
        var deltaTime = Time.deltaTime;

        //  Calculating the desired velocity for moving, and rotation.
        m_movementAcceleration.CalculateVelocity(input.y, m_moveSpeed, m_moveGrip, deltaTime);
        m_rotationAcceleration.CalculateVelocity(input.x, m_turnSpeed, m_turnGrip, deltaTime);

        //  Applying calculated velocity.
        m_characterController.Move(transform.forward * (m_movementAcceleration.velocity * deltaTime));
        transform.Rotate(0f, m_rotationAcceleration.velocity * deltaTime, 0f, Space.Self);
    }

    private Vector2 GetInput()
    {
        var input = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) ++input.y;
        if (Input.GetKey(KeyCode.S)) --input.y;
        if (Input.GetKey(KeyCode.LeftArrow)) --input.x;
        if (Input.GetKey(KeyCode.RightArrow)) ++input.x;
        return input;
    }
}
