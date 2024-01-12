using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.Events;

public class TimerManager : MonoBehaviour
{
    [SerializeField] private float m_time = 100f;
    [SerializeField] private bool m_instaKill = false;
    [Space]
    [SerializeField] private UnityEvent m_onTimeLost = null;
    [SerializeField] private UnityEvent m_onGameLost = null;
    [SerializeField] private UnityEvent m_onGameWon = null;
    [SerializeField] private UnityEvent<float> m_onTimeUpdated = null;

    private State m_state = State.Running;
    private float m_secondsLeft = 0f;

    private void Start()
    {
        m_secondsLeft = m_time;
    }

    private void Update()
    {
        if (m_state != State.Running) return;

        m_secondsLeft -= Time.deltaTime;
        m_onTimeUpdated.Invoke(m_secondsLeft);
        if (m_secondsLeft <= 0f)
        {
            m_secondsLeft = 0f;
            m_state = State.Lost;
            m_onGameLost.Invoke();
        }
    }

    public void RemoveTime(float _seconds)
    {
        if (m_instaKill) _seconds = Mathf.Infinity;
        m_secondsLeft -= _seconds;
        m_onTimeLost.Invoke();
    }

    public enum State
    {
        Running,
        Won,
        Lost,
    }
}
