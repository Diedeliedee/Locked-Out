using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour
{
    //  Events:
    [SerializeField] private UnityEvent<Window> m_onFocus = new();
    [SerializeField] private UnityEvent m_onDefocus = new();
    [SerializeField] private UnityEvent<Window> m_onDestroy = new();

    //  Reference:
    private RectTransform m_RectTransform = null;
    private WindowFocusReceiver m_focusReceiver = null;

    #region Properties
    //  Events moved to properties to enable reference tracking.
    public UnityEvent<Window> onSetFocusEvent => m_onFocus;
    public UnityEvent onDefocus => m_onDefocus;
    public UnityEvent<Window> onDestroy => m_onDestroy;

    public RectTransform rectTransform => m_RectTransform;
    #endregion

    private void Awake()
    {
        m_RectTransform = GetComponent<RectTransform>();
        m_focusReceiver = GetComponentInChildren<WindowFocusReceiver>();

        m_focusReceiver.onWindowClick += OnWindowClick;
    }

    /// <summary>
    /// the m_windows hides it self
    /// </summary>
    public void Hide()
    {
        m_onFocus.Invoke(null);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// closes the window
    /// </summary>
    public void DestroySelf()
    {
        m_onDefocus.Invoke();
        m_onDestroy.Invoke(this);
        //Destroy call moved to Desktop after removing it from the list because unity keeps complaining about it "disapearing"
    }

    public void OnWindowClick(PointerEventData _eventData)
    {
        transform.SetAsLastSibling();
        m_onFocus.Invoke(this);
    }

    /// <summary>
    /// Moves the window's anchored position to the passed in Vector2 parameter.
    /// </summary>
    public void MoveTo(Vector2 _screenPosition)
    {
        m_RectTransform.anchoredPosition = _screenPosition;
    }
}
