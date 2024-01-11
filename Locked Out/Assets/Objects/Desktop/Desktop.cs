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
        m_windows = GetComponentsInChildren<Window>().ToList();
        foreach (Window w in m_windows)
        {
            w.onSetFocusEvent.AddListener(SetFocusWindow);
            w.onDestroy.AddListener(RemoveWindow);
        }
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void AddWindow(Window window)
    {
        m_windows.Add(window);
        window.onSetFocusEvent.AddListener(SetFocusWindow);
        window.onDestroy.AddListener(RemoveWindow);
        //set the focus window to be the new window
        window.onSetFocusEvent?.Invoke(window);
    }

    public void RemoveWindow(Window window)
    {
        m_windows.Remove(window);
        window.onSetFocusEvent.RemoveListener(SetFocusWindow);
        window.onDestroy.RemoveListener(RemoveWindow);
        Destroy(window.gameObject);
    }

    public void SetFocusWindow(Window window)
    {
        if (m_focussedWindow != null) m_focussedWindow.onDefocus?.Invoke();
        m_focussedWindow = window;
    }

    /// <summary>
    /// Removes null pointers, shouldn't be used when things are cleared correctly
    /// </summary>
    private void RevalidateList()
    {
        m_windows = m_windows.Where(x => x != null).ToList();
    }

}
