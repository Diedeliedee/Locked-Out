using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Desktop : MonoBehaviour
{
    Window focusWindow;

    [SerializeField] private List<Window> windows;

    private void Start()
    {
        foreach(Window w in windows)
        {
            w.OnSetFocusEvent.AddListener(SetFocusWindow);
            w.OnDestory.AddListener(RemoveWindow);
        }
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AddWindow(Window window)
    {
        windows.Add(window);
        window.OnSetFocusEvent.AddListener(SetFocusWindow);
        window.OnDestory.AddListener(RemoveWindow);
        //set the focus window to be the new window
        window.OnSetFocusEvent?.Invoke(window);
    }

    public void RemoveWindow(Window window)
    {
        windows.Remove(window);
        window.OnSetFocusEvent.RemoveListener(SetFocusWindow);
        window.OnDestory.RemoveListener(RemoveWindow);
        Destroy(window.gameObject);
    }

    /// <summary>
    /// Removes null pointers, shouldn't be used when things are cleared correctly
    /// </summary>
    private void RevalidateList()
    {
        windows = windows.Where(x => x != null).ToList();
    }


    public void SetFocusWindow(Window window)
    {
        if(focusWindow != null) focusWindow.OnUnSetFocusEvent?.Invoke();
        focusWindow = window;
    }
}
