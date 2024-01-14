using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class GrabInteractable : HighlightHoverable, IGrabbable
{
    public UnityEvent onGrab = null;

    private Transform m_defaultParent = null;
    private LayerMask m_defaultLayer = default;

    private Rigidbody m_rigidBody = null;
    public bool enablePhysicsOnRelease = false;

    protected override void Awake()
    {
        base.Awake();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_defaultParent = transform.parent;
        m_defaultLayer = gameObject.layer;
    }

    public void OnGrab(Transform _origin)
    {
        transform.parent = _origin;
        transform.localPosition = Vector3.zero;

        gameObject.layer = 0;

        enablePhysicsOnRelease = false;
        m_rigidBody.isKinematic = true;

        onGrab?.Invoke();
    }

    public void OnRelease()
    {
        transform.parent = m_defaultParent;

        gameObject.layer = m_defaultLayer;

        if (enablePhysicsOnRelease) return;
        m_rigidBody.isKinematic = false;
    }
}
