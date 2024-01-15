using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Desktop : MonoBehaviour
{
    private List<Window> m_windows = null;
    private Window m_focussedWindow = null;

    public Window focussedWindow => m_focussedWindow;

    private void Start()
    {
        m_windows = GetComponentsInChildren<Window>(true).ToList();
        foreach (Window w in m_windows)
        {
            w.onRequestFocus += SetFocusWindow;
            w.onRequestDestroy += RemoveWindow;
        }
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AddWindow(Window window)
    {
        m_windows.Add(window);
        window.onRequestFocus += SetFocusWindow;
        window.onRequestDestroy += RemoveWindow;

        //  Set the focus window to be the new window
        SetFocusWindow(window);
    }

    public void RemoveWindow(Window window)
    {
        m_windows.Remove(window);
        window.onRequestFocus -= SetFocusWindow;
        window.onRequestDestroy -= RemoveWindow;
        window.OnObliterate();
        Destroy(window.gameObject);
    }

    public void SetFocusWindow(Window window)
    {
        if (m_focussedWindow != null) m_focussedWindow.OnDefocus();
        m_focussedWindow = window;
        m_focussedWindow.OnFocus();
    }

    /// <summary>
    /// Removes null pointers, shouldn't be used when things are cleared correctly
    /// </summary>
    private void RevalidateList()
    {
        m_windows = m_windows.Where(x => x != null).ToList();
    }

}
