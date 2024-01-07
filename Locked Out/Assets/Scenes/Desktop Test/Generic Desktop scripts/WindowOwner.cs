using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOwner : MonoBehaviour
{
    [SerializeField] private Window windowPrefab;
    [SerializeField] private Window windowInstance;
    [SerializeField] private Transform canvasReferance;
    [SerializeField] private bool closeOnclick = true;

    private Desktop desktop;

    private void Start()
    {
        desktop = FindObjectOfType<Desktop>();
    }


    /// <summary>
    /// Toggels a window on or off and spawn it in if it doesn't exist
    /// </summary>
    public void onClick()
    {
        if(windowInstance != null)
        {
            if (!closeOnclick) return;
            windowInstance.gameObject.SetActive(!windowInstance.gameObject.activeInHierarchy);
            if (windowInstance.gameObject.activeInHierarchy)
            {
                windowInstance.OnSetFocusEvent?.Invoke(windowInstance);
            }
        }
        else
        {
            windowInstance = Instantiate(windowPrefab, canvasReferance);
            desktop.AddWindow(windowInstance);
        }
    }



}
