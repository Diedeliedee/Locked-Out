using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;

    private IGrabbable m_holdingItem = null;

    public void TryGrab(IGrabbable _item)
    {
        if (m_holdingItem != null) return;

        //  Play holding animation.
        _item.Grab(m_pivot);
        m_holdingItem = _item;
    }

    public void TryRelease()
    {
        if (m_holdingItem == null) return;

        //  Play release animation.
        m_holdingItem.Release();
        m_holdingItem = null;
    }
}
