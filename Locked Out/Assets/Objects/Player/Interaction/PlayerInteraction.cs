using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float m_interactionDistance = 3f;
    [SerializeField] private LayerMask m_interactionLayer = default;
    [Space]
    [SerializeField] private Toolbox m_toolbox = null;
    [SerializeField] private Transform m_raycastOrigin = null;

    //  Cache:
    private IHoverable m_cachedInteractable = null;

    //  Reference:
    private PlayerInputReader m_input = null;

    private void Awake()
    {
        m_input = FindObjectOfType<PlayerInputReader>();
    }

    private void Update()
    {
        var buttonPressed = m_input.interactWasPressed;

        //  Check if our interaction raycast hits an interactable object. If it didn't clear the cached interactable.
        if (!FoundHoverable(out IHoverable _hoverable))
        {
            if (m_cachedInteractable != null)
            {
                m_cachedInteractable.OnExit();
                m_cachedInteractable = null;
            }
            if (buttonPressed) m_toolbox.TrySecondaryActions();
            return;
        }

        //  If a valid interactable is found, cache it, and call OnEnter function. 
        m_cachedInteractable?.OnExit();
        m_cachedInteractable = _hoverable;
        m_cachedInteractable.OnEnter();

        //  Have the toolbox check all possible interaction types.
        if (buttonPressed) m_toolbox.CrossReference(_hoverable);
    }

    private bool FoundHoverable(out IHoverable _hoverable)
    {
        _hoverable = null;

        if (!Physics.Raycast(m_raycastOrigin.position, m_raycastOrigin.forward, out RaycastHit _hit, m_interactionDistance, m_interactionLayer)) return false;
        if (!_hit.transform.TryGetComponent(out _hoverable)) return false;
        return true;
    }
}
