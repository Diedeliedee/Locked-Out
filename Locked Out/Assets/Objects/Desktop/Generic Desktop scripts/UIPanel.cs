using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UIPanel : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image image;       //the image we are looking for
    private Vector3 offset = Vector3.zero;
    private Transform panelMaster;              // the owning parent that we want to move
    public Transform parentAfterDrag;           // the parent object the master was in before draging

    void Start()
    {
        panelMaster = transform.parent;
        image = GetComponent<Image>();
    }

    /// <summary>
    /// sets all variables and unparents the Master
    /// </summary>
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDrag = panelMaster.parent;
        panelMaster.SetParent(transform.root);
        panelMaster.SetAsLastSibling();
        image.raycastTarget = false;
        offset = panelMaster.position - Input.mousePosition;
    }

    /// <summary>
    /// Moves the panel with a offset
    /// </summary>
    public void OnDrag(PointerEventData eventData)
    {
        panelMaster.position = Input.mousePosition + offset;
    }

    /// <summary>
    /// Sets the master back under the correct parent
    /// </summary>
    public void OnEndDrag(PointerEventData eventData)
    {
        panelMaster.SetParent(parentAfterDrag);
        image.raycastTarget = transform;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }



}
