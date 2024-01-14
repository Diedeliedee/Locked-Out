using UnityEngine;

public interface IGrabbable
{
    public Vector3 position { get; }

    public void OnGrab(Transform _origin);

    public void OnHold(Transform _origin, float _deltaTime);

    public void OnRelease();
}
