using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float m_interactionDistance = 3f;
    [Space]
    [SerializeField] private Toolbox m_toolbox = null;
    [SerializeField] private Transform m_raycastOrigin = null;

    private IHoverable m_cachedInteractable = null;

    private void Update()
    {
        //  Check if our interaction raycast hits an interactable object. If it didn't clear the cached interactable.
        if (!FoundHoverable(out IHoverable _hoverable))
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
        m_cachedInteractable = _hoverable;
        m_cachedInteractable.OnEnter();

        //  Have the toolbox check all possible interaction types.
        m_toolbox.CrossReference(_hoverable);
    }

    private bool FoundHoverable(out IHoverable _hoverable)
    {
        _hoverable = null;

        if (!Physics.Raycast(m_raycastOrigin.position, m_raycastOrigin.forward, out RaycastHit _hit, m_interactionDistance)) return false;
        if (!_hit.transform.TryGetComponent(out _hoverable)) return false;
        return true;
    }
}
