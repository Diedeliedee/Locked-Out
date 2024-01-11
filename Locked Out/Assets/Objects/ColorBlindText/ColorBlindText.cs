using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlindText : MonoBehaviour
{
    [SerializeField]
    GameObject TextObject;
    //  Reference:
    private PlayerInputReader m_input = null;

    private void Start()
    {
        m_input = FindObjectOfType<PlayerInputReader>();
    }

    private void Update()
    {
        var buttonPressed = m_input.toggleColorBlindModeWasPressed;
        if (buttonPressed) { 
            
            TextObject.SetActive(!TextObject.activeInHierarchy); 
        }
    }
}
