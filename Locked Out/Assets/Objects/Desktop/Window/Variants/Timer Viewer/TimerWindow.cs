using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TimerWindow : MonoBehaviour
{
    [Header("References:")]
    [SerializeField] private TextMeshProUGUI m_textMesh = null;
    [SerializeField] private Color m_gameOverColor = Color.red;

    [Header("Enabling:")]
    [SerializeField] private float m_enableTime = 1f;
    [SerializeField] private Window m_window = null;
    [SerializeField] private UnityEvent m_onTimePassed = null;

    private bool m_activated = false;

    public void OnTimerChanged(float _seconds)
    {
        var time = System.TimeSpan.FromSeconds(_seconds);

        m_textMesh.text = time.ToString(@"mm\:ss");
        if (!m_activated && _seconds <= m_enableTime)
        {
            m_window.gameObject.SetActive(true);
            m_window.Focus();
            m_onTimePassed?.Invoke();
            m_activated = true;
        }
    }

    public void OnGameLost()
    {
        m_textMesh.color = m_gameOverColor;
    }
}
