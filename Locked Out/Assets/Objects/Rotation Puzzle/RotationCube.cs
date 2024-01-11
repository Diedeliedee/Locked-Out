using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    public System.Action<Quaternion> onRotated = null;

    private bool m_grabbed = false;

    private void Update()
    {
        if (!m_grabbed) return;

        //  Get joycon rotation, and invoke on rotated.
    }

    public void Grab(Transform _origin)
    {
        m_grabbed = true;
    }

    public void Release()
    {
        m_grabbed = false;
    }
}
