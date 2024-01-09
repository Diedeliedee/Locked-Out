using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;

    private IGrabbable m_holdingItem = null;

    public void Grab(IGrabbable _item)
    {
        if (m_holdingItem != null) Release();

        //  Play holding animation.
        _item.Grab(m_pivot);
    }

    public void Release()
    {
        m_holdingItem.Release();
    }
}
