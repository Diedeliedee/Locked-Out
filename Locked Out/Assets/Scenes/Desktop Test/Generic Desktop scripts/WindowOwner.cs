using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowOwner : MonoBehaviour
{
    [SerializeField] private GameObject windowPrefab;
    [SerializeField] private GameObject windowInstance;
    [SerializeField] private Transform canvasReferance;
    [SerializeField] private bool closeOnclick = true;


    /// <summary>
    /// Toggels a window on or off and spawn it in if it doesn't exist
    /// </summary>
    public void onClick()
    {
        if(windowInstance != null)
        {
            if (!closeOnclick) return;
            windowInstance.SetActive(!windowInstance.activeInHierarchy);
        }
        else
        {
            windowInstance = Instantiate(windowPrefab, canvasReferance);
        }
    }



}
