using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public float timeLeft;
    [SerializeField] private TextMeshProUGUI m_textMesh;
    [SerializeField] private UnityEvent onTimerFinished;

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        m_textMesh.text = timeLeft.ToString("#.00");

        if (timeLeft <= 0) onTimerFinished.Invoke();
    }
}
