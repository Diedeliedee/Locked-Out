using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    public System.Action<Quaternion> onRotated = null;
    private bool m_grabbed = false;

    private PlayerInputReader m_input = null;


    protected override void Awake()
    {
        base.Awake();
        m_input = FindObjectOfType<PlayerInputReader>();
    }

    private void Update()
    {
        if (!m_grabbed) return;

        var rotation = m_input.gyroOrientationInput;

        transform.localRotation = rotation;
        onRotated?.Invoke(rotation);
    }

    public void OnGrab(Transform _origin)
    {
        m_grabbed = true;
    }

    public void OnRelease()
    {
        m_grabbed = false;
    }
}
