using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;


public class Window : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent<Window> OnSetFocusEvent = new();
    public UnityEvent OnUnSetFocusEvent = new();
    public UnityEvent<Window> OnDestory = new();

    /// <summary>
    /// the windows hides it self
    /// </summary>
    public void Hide()
    {
        OnSetFocusEvent?.Invoke(null);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// closes the window
    /// </summary>
    public void DestroySelf()
    {
        OnUnSetFocusEvent?.Invoke();
        OnDestory?.Invoke(this);
        //Destroy call moved to Desktop after removing it from the list because unity keeps complaining about it "disapearing"
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        OnSetFocusEvent?.Invoke(this);
    }


}
