using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesktopLoader : MonoBehaviour
{
    void Start()
    {
        Desktop d = FindObjectOfType<Desktop>();
        if(d == null)
        {
            SceneManager.LoadScene(0, LoadSceneMode.Additive);
        }
    }
}
