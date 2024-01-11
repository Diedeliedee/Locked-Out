using UnityEngine;
using UnityEngine.EventSystems;

public class WindowDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    //  Cache:
    private Vector2 m_offset = Vector2.zero;

    //  Reference:
    [SerializeField] private Window m_window = null;

    //  Utility Property:
    private Vector2 m_mousePosition => new Vector2(Input.mousePosition.x, Input.mousePosition.y);

    /// <summary>
    /// Registers offset, and moves move the window up front.
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        m_offset = m_window.rectTransform.anchoredPosition - m_mousePosition;
        m_window.transform.SetAsLastSibling();
    }

    /// <summary>
    /// Moves the panel with an offset.
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        m_window.MoveTo(m_mousePosition + m_offset);
    }

    /// <summary>
    /// Sets the master back under the correct parent
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        m_offset = Vector2.zero;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
