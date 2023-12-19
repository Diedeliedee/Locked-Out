using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacteded!!!", gameObject);
    }
}
