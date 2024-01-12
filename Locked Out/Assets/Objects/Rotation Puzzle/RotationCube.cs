using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    [SerializeField] private JoyconDemo joyconGyro;
    public System.Action<Quaternion> onRotated = null;

    private bool m_grabbed = false;

    private void Update()
    {
        if (!m_grabbed) return;

        onRotated.Invoke(joyconGyro.orientation);
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
