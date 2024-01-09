using UnityEngine;

public interface IGrabbable
{
    public void Grab(Transform _origin);

    public void Release();
}
