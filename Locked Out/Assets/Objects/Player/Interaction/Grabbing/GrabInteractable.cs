using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class GrabInteractable : HighlightHoverable, IGrabbable
{
    private Transform m_defaultParent = null;
    private Rigidbody m_rigidBody = null;

    protected override void Awake()
    {
        base.Awake();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_defaultParent = transform.parent;
    }

    public void Grab(Transform _origin)
    {
        transform.parent = _origin;
        transform.localPosition = Vector3.zero;

        m_rigidBody.isKinematic = true;
    }

    public void Release()
    {
        transform.parent = m_defaultParent;

        m_rigidBody.isKinematic = false;
    }
}
