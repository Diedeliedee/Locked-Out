using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackComputer : MonoBehaviour
{
    private bool insideCollider;

    private void Update()
    {
        if (insideCollider && Input.GetKeyDown(KeyCode.Space))
        {
            print("Engage le hacking");
            // Logic here
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player")) insideCollider = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player")) insideCollider = false;
    }
}
