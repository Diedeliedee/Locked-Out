using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float m_interactionDistance = 3f;
    [Space]
    [SerializeField] private Transform m_raycastOrigin;

    private IInteractable m_cachedInteractable = null;

    private void Update()
    {
        //  Check if our interaction raycast hits an interactable object. If it didn't clear the cached interactable.
        if (!FoundInteractable(out IInteractable _interactable))
        {
            if (m_cachedInteractable != null)
            {
                m_cachedInteractable.OnExit();
                m_cachedInteractable = null;
            }
            return;
        }

        //  If a valid interactable is found, cache it, and call OnEnter function. 
        m_cachedInteractable?.OnExit();
        m_cachedInteractable = _interactable;
        m_cachedInteractable.OnEnter();

        //  If the interaction button has been pressed
        if (Input.GetKeyDown(KeyCode.Space)) _interactable.OnInteract();
    }

    private bool FoundInteractable(out IInteractable _interactable)
    {
        _interactable = null;

        if (!Physics.Raycast(m_raycastOrigin.position, m_raycastOrigin.forward, out RaycastHit _hit, m_interactionDistance)) return false;
        if (!_hit.transform.TryGetComponent(out _interactable)) return false;
        return true;
    }
}
