using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerInteraction : HighlightHoverable
{
    [SerializeField] private bool correctServer;

    public void Hack()
    {
        if (correctServer)
        {
            Debug.Log("Hacked correct server", gameObject);
            // Get clue text file
        }
        else
        {
            Debug.Log("Hacked incorrect server", gameObject);
            // Get time penalty
        }
    }
}
