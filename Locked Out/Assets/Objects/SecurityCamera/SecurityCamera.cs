using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField] private float maxDegrees = 30;
    [SerializeField] private float duration;
    [SerializeField] private Transform pivot;
    [SerializeField] private CameraDetection m_detection = null;

    private Coroutine m_routine = null;

    private void Start()
    {
        m_routine = StartCoroutine(Rotation(maxDegrees));
    }

    private void OnDisable()
    {
        m_detection.gameObject.SetActive(false);
        StopCoroutine(m_routine);
    }

    private IEnumerator Rotation(float targetRotation)
    {
        var startRotation = -targetRotation;
        var timer = 0f;

        while(timer < duration)
        {
            var eulers = pivot.localEulerAngles;

            timer += Time.deltaTime;

            pivot.localEulerAngles = new Vector3(eulers.x, Mathf.Lerp(startRotation, targetRotation, timer / duration), eulers.z);

            yield return null;
        }

        yield return new WaitForSeconds(duration);

        StartCoroutine(Rotation(startRotation));
    }
}