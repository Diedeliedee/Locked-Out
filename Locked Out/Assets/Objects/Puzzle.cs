using UnityEngine;
using UnityEngine.Events;

public abstract class Puzzle : MonoBehaviour
{
    [SerializeField] protected UnityEvent m_onSolved = null;
}
