using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ServerInteraction : HighlightHoverable, IInteractable
{
    [SerializeField] private UnityEvent m_onInteract = null;

    public void Interact()
    {
        m_onInteract.Invoke();
    }
}
