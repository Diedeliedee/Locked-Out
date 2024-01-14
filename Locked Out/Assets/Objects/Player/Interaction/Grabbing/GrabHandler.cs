using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [SerializeField] private Transform m_pivot;
    [SerializeField] private float m_gripLossDistance = 1f;

    //  Cache:
    private IGrabbable m_holdingItem = null;

    //  Reference:
    private PlayerInputReader m_input = null;

    private void Awake()
    {
        m_input = FindObjectOfType<PlayerInputReader>();
    }

    public void Tick(float _deltaTime)
    {
        m_pivot.localRotation = m_input.gyroOrientationInput;

        if (m_holdingItem == null) return;
        if ((m_holdingItem.position - m_pivot.position).sqrMagnitude > m_gripLossDistance * m_gripLossDistance)
        {
            Release();
            return;
        }
        m_holdingItem.OnHold(m_pivot, _deltaTime);
    }

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
