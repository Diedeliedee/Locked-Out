using UnityEngine;

public class PlayerLookRotation : MonoBehaviour
{
    [SerializeField] private float m_range = 70f;
    [SerializeField] private float m_speed = 180f;

    private float m_pitch = 0f;
    private float m_maxOffset = 0f;

    private PlayerInputReader m_input = null;

    private void Awake()
    {
        m_input = FindObjectOfType<PlayerInputReader>();
    }

    private void Start()
    {
        m_maxOffset = m_range / 2f;
    }

    private void Update()
    {
        m_pitch += m_input.rotationInput.y * m_speed * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_maxOffset, m_maxOffset);
        transform.localEulerAngles = new Vector3(-m_pitch, 0f, 0f);
    }
}
