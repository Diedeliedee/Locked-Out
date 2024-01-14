using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;

    private IGrabbable m_holdingItem = null;

    public void TryGrab(IGrabbable _item)
    {
        if (m_holdingItem == null) Grab(_item);
    }

    public void TryRelease()
    {
        if (m_holdingItem != null) Release();
    }

    public void TryInsert(ISlot _slot)
    {
        if (m_holdingItem == null) return;
        if (!_slot.TryPlace(m_holdingItem)) return;
        Release();
    }

    private void Grab(IGrabbable _item)
    {
        //  Play holding animation.
        _item.OnGrab(m_pivot);
        m_holdingItem = _item;
    }

    private void Release()
    {
        //  Play release animation.
        m_holdingItem.OnRelease();
        m_holdingItem = null;
    }
}
