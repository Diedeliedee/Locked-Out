using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    public System.Action<Quaternion> onRotated = null;

    private Quaternion m_offset = default;

    public Vector3 position => transform.position;

    public void OnGrab(Transform _origin)
    {
        m_offset = Quaternion.Inverse(_origin.rotation) * transform.rotation;
    }

    public void OnHold(Transform _origin, float _deltaTime)
    {
        transform.rotation = _origin.rotation * m_offset;
        onRotated?.Invoke(transform.localRotation);
    }

    public void OnRelease()
    {
        m_offset = default;
    }
}
