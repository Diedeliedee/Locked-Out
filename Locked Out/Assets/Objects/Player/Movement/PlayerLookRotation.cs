using UnityEngine;

public class PlayerLookRotation : MonoBehaviour
{
    [SerializeField] private float m_range = 70f;
    [SerializeField] private float m_speed = 180f;

    private float m_pitch = 0f;
    private float m_maxOffset = 0f;

    private void Start()
    {
        m_maxOffset = m_range / 2f;
    }

    private void Update()
    {
        var input = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) ++input;
        if (Input.GetKey(KeyCode.DownArrow)) --input;

        m_pitch += input * m_speed * Time.deltaTime;
        m_pitch = Mathf.Clamp(m_pitch, -m_maxOffset, m_maxOffset);
        transform.localEulerAngles = new Vector3(-m_pitch, 0f, 0f);
    }
}
