using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public void OnEnter() { }

    public void OnExit() { }

    public void OnInteract()
    {
        Debug.Log("Interacteded!!!", gameObject);
    }
}
