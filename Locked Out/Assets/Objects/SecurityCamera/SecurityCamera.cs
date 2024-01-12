using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private UnityEvent m_onPlayerDetected = null;
    [Space]
    [SerializeField] private float maxDegrees = 30;
    [SerializeField] private float detectionCooldown = 2f;
    [SerializeField] private float duration;
    [SerializeField] private Transform pivot;
    [SerializeField] private CameraDetection m_detection = null;

    private Coroutine m_routine = null;
    private float m_cooldownTimer = 0f;
    private bool m_disabled = false;

    private void Start()
    {
        m_routine = StartCoroutine(Rotation(maxDegrees));
    }

    private void Update()
    {
        if (m_disabled) return;
        if (m_cooldownTimer > 0f)
        {
            m_cooldownTimer -= Time.deltaTime;
            if (m_cooldownTimer < 0f)
            {
                m_cooldownTimer = 0f;
            }
        }
        else if (m_detection.PlayerDetected())
        {
            m_onPlayerDetected.Invoke();
            m_cooldownTimer = detectionCooldown;
        }

        //  Joeri remooove.
        if (Input.GetKeyDown(KeyCode.K)) Disable();
    }

    public void Disable()
    {
        m_disabled = true;
        m_detection.gameObject.SetActive(false);
        StopCoroutine(m_routine);
    }

    private IEnumerator Rotation(float targetRotation)
    {
        var startRotation = -targetRotation;
        var timer = 0f;

        while (timer < duration)
        {
            var eulers = pivot.localEulerAngles;

            timer += Time.deltaTime;

            pivot.localEulerAngles = new Vector3(eulers.x, Mathf.Lerp(startRotation, targetRotation, timer / duration), eulers.z);

            yield return null;
        }

        yield return new WaitForSeconds(duration);

        m_routine = StartCoroutine(Rotation(startRotation));
    }
}