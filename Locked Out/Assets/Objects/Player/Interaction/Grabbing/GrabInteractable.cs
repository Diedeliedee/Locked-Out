using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class GrabInteractable : HighlightHoverable, IGrabbable
{
    [SerializeField] private float m_smoothenTime = 0.1f;
    [SerializeField] private UnityEvent m_onGrab = null;

    //  Cache:
    private Vector3 m_holdVelocity = default;

    //  Reference:
    private LayerMask m_defaultLayer = default;
    private LayerMask m_holdLayer = 7;
    private Rigidbody m_rigidBody = null;

    public bool enablePhysicsOnRelease = false;

    public UnityEvent onGrab => m_onGrab;
    public Vector3 position => transform.position;

    protected override void Awake()
    {
        base.Awake();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        m_defaultLayer = gameObject.layer;
    }

    public void OnGrab(Transform _origin)
    {
        gameObject.layer = m_holdLayer;

        enablePhysicsOnRelease = false;
        m_rigidBody.isKinematic = true;

        m_onGrab?.Invoke();
    }

    public void OnHold(Transform _origin, float _deltaTime)
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            _origin.position,
            ref m_holdVelocity,
            m_smoothenTime,
            Mathf.Infinity,
            _deltaTime);

        transform.rotation = Quaternion.Slerp(transform.rotation, _origin.rotation, _deltaTime / m_smoothenTime);
    }
    public void OnRelease()
    {
        m_rigidBody.velocity = m_holdVelocity;
        m_holdVelocity = default;

        gameObject.layer = m_defaultLayer;

        if (enablePhysicsOnRelease) return;
        m_rigidBody.isKinematic = false;
    }
}

