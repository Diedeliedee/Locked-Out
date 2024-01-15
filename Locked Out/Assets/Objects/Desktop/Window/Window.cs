using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour
{
    //  Events:
    [SerializeField] private UnityEvent<Window> m_onFocus = new();
    [SerializeField] private UnityEvent m_onDefocus = new();
    [SerializeField] private UnityEvent<Window> m_onDestroy = new();

    //  Desktop events.
    public System.Action<Window> onRequestFocus;
    public System.Action<Window> onRequestDestroy;

    //  Reference:
    private RectTransform m_RectTransform = null;
    private WindowFocusReceiver m_focusReceiver = null;

    //  Properties:
    public RectTransform rectTransform => m_RectTransform;

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_focusReceiver = GetComponentInChildren<WindowFocusReceiver>();
        if(m_focusReceiver != null)
        m_focusReceiver.onWindowClick += OnWindowClick;
    }

    #region Interface
    /// <summary>
    /// The window hides it self
    /// </summary>
    public void Hide()
    {
        m_onFocus.Invoke(null);
        gameObject.SetActive(false);
    }

    public void Focus()
    {
        onRequestFocus?.Invoke(this);
    }

    /// <summary>
    /// Closes the window
    /// </summary>
    public void DestroySelf()
    {
        onRequestDestroy?.Invoke(this);
        //Destroy call moved to Desktop after removing it from the list because unity keeps complaining about it "disapearing"
    }

    /// <summary>
    /// Moves the window's anchored position to the passed in Vector2 parameter.
    /// </summary>
    public void MoveTo(Vector2 _screenPosition)
    {
        m_RectTransform.anchoredPosition = _screenPosition;
    }
    #endregion

    #region Events
    public void OnWindowClick(PointerEventData _eventData)
    {
        Focus();
    }

    public void OnFocus()
    {
        transform.SetAsLastSibling();
        m_onFocus.Invoke(this);
    }

    public void OnDefocus()
    {
        m_onDefocus.Invoke();
    }

    public void OnObliterate()
    {
        m_onDefocus.Invoke();
        m_onDestroy.Invoke(this);
    }
    #endregion


}
