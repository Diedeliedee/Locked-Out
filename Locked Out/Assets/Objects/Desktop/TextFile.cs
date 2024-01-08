using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFile : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI DisplayText;
    private string DisplayTextString;
    private Coroutine blinking;
    private bool focus;

    // Start is called before the first frame update
    void Start()
    {
        DisplayTextString = DisplayText.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (focus && blinking == null)
        {
            blinking = StartCoroutine(Blinking());
        }
        else if (!focus&& blinking != null)
        {
            StopCoroutine(blinking);
                DisplayText.text = DisplayTextString;
            blinking = null;
        }

        if (!focus) { return; }

        if (Input.inputString != "")
        {

            char[] characters = Input.inputString.ToCharArray();
            foreach (char c in characters)
            {
                if (c == '\b') //Input is backspace
                {
                    if (DisplayTextString.Length != 0)
                    {
                        DisplayTextString = DisplayTextString.Remove(DisplayTextString.Length - 1, 1);
                        DisplayText.text = DisplayTextString;
                    }
                    return;
                }
                if (c == '\r') //Input is Enter
                {
                    DisplayTextString += System.Environment.NewLine;
                    DisplayText.text = DisplayTextString;
                    return;
                }
            }
            DisplayTextString += Input.inputString;
            DisplayText.text = DisplayTextString;
            return;
        }
    }

    public void OnSetFocus(Window w)
    {
        focus = true;
    }

    public void OnUnSetFocus()
    {
        focus = false;
    }

    IEnumerator Blinking()
    {
        while (true)
        {
            DisplayText.text += "|";
            yield return new WaitForSeconds(0.5f);
            DisplayText.text = DisplayTextString;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
