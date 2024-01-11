using UnityEngine;
using UnityEngine.EventSystems;

public class WindowFocusReceiver : MonoBehaviour, IPointerClickHandler
{
    public System.Action<PointerEventData> onWindowClick = null;

    public void OnPointerClick(PointerEventData _eventData)
    {
        onWindowClick?.Invoke(_eventData);
    }
}
