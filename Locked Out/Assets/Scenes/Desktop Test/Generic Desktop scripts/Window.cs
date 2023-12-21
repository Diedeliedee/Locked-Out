using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    /// <summary>
    /// the windows hides it self
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// closes the window
    /// </summary>
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
