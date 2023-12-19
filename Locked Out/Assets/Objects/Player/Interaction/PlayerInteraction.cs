using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float m_interactionDistance = 3f;
    [Space]
    [SerializeField] private Transform m_raycastOrigin;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (!Physics.Raycast(m_raycastOrigin.position, m_raycastOrigin.forward, out RaycastHit _hit, m_interactionDistance)) return;
        if (!_hit.transform.TryGetComponent(out IInteractable _interactable)) return;

        _interactable.Interact();
    }
}
