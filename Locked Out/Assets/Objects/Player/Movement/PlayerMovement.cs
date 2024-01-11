using Joeri.Tools.Movement;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("General Movement:")]
    [SerializeField] private float m_moveSpeed = 1f;
    [SerializeField] private float m_moveGrip = 1f;
    [Space]
    [SerializeField] private float m_turnSpeed = 1f;
    [SerializeField] private float m_turnGrip = 1f;

    [Header("Verticality:")]
    [SerializeField] private float m_airGripMultiplier = 0.25f;
    [SerializeField] private float m_hardFallTreshold = 3f;
    [SerializeField] private UnityEvent m_onLand;

    private PlayerInputReader m_input = null;
    private CharacterController m_characterController;
    private Accel.Singular m_movementAcceleration = new();
    private Accel.Singular m_rotationAcceleration = new();
    private Accel.Uncontrolled m_verticalAcceleration = null;
    private GroundCheck m_groundCheck = null;

    public bool isGrounded { get; private set; }

    private void Awake()
    {
        m_input = FindObjectOfType<PlayerInputReader>();
        m_characterController = GetComponent<CharacterController>();
        m_verticalAcceleration = new(-9.81f, 0f, 0f);
        m_groundCheck = GetComponent<GroundCheck>();
    }

    private void Update()
    {
        var input = m_input.movementInput;
        var velocity = Vector3.zero;
        var deltaTime = Time.deltaTime;
        var onGround = m_groundCheck.IsOnGround();

        //  Calculating the desired velocity for moving, and rotating.
        m_movementAcceleration.CalculateVelocity(input.y, m_moveSpeed, isGrounded ? m_moveGrip : m_moveGrip * m_airGripMultiplier, deltaTime);
        m_rotationAcceleration.CalculateVelocity(input.x, m_turnSpeed, isGrounded ? m_turnGrip : m_turnGrip * m_airGripMultiplier, deltaTime);

        //  Apply calculated velocity to variable.
        velocity += transform.forward * m_movementAcceleration.velocity;

        //  Gravity calculations
        if (onGround && m_verticalAcceleration.velocity <= 0)
        {
            if (m_verticalAcceleration.velocity < -m_hardFallTreshold) m_onLand.Invoke();
            m_verticalAcceleration.velocity = -m_verticalAcceleration.velocity * 0.2f;
        }
        else
        {
            velocity += Vector3.up * m_verticalAcceleration.CalculateVelocity(deltaTime);
        }
        isGrounded = onGround;

        //  Applying variable.
        m_characterController.Move(velocity * deltaTime);
        transform.Rotate(0f, m_rotationAcceleration.velocity * deltaTime, 0f, Space.Self);
    }
}
