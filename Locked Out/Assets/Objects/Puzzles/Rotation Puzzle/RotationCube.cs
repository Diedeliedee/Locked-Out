using UnityEngine;

public class RotationCube : HighlightHoverable, IGrabbable
{
    public System.Action<Quaternion> onRotated = null;

    public Vector3 position => transform.position;

    public void OnGrab(Transform _origin) { }

    public void OnHold(Transform _origin, float _deltaTime)
    {
        transform.rotation = _origin.rotation;
        onRotated?.Invoke(transform.localRotation);
    }

    public void OnRelease() { }
}
