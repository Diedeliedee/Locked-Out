using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void OnEnter();

    public void OnExit();

    public void OnInteract();
}
