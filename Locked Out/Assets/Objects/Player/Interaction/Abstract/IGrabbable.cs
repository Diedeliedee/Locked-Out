using UnityEngine;

public interface IGrabbable
{
    public void OnGrab(Transform _origin);

    public void OnRelease();
}
