using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Console : MonoBehaviour
{
    [SerializeField] private string password;
    [SerializeField] private string lastInput;
    [SerializeField]
    TextMeshProUGUI DisplayText;
    private string DisplayTextString;
    private Coroutine blinking;
    private Coroutine animateText;
    private bool canType = false;
    private bool focus;
    public UnityEvent completed = new();
    public UnityEvent wrongAnswer = new();

    //why do this in the code? because using is stripping out my fucking unicode characters otherwise 
    string startString = "===============================================" + System.Environment.NewLine +
                         "= CardOS v1.0 - Command Prompt =" + System.Environment.NewLine +
                         "===============================================" + System.Environment.NewLine +
                         System.Environment.NewLine +
                         "[CardOS Shell - Version 1.0.0]" + System.Environment.NewLine +
                         System.Environment.NewLine +
                         "[♠] Welcome to CardOS, where every command is a game!" + System.Environment.NewLine +
                         System.Environment.NewLine +
                         "[♣] Aces high! Let the cards guide your way." + System.Environment.NewLine +
                         System.Environment.NewLine +
                         "[♦] HINT: The Joker's secret lies in the cards he loves." + System.Environment.NewLine +
                         "Observe the suits closely, and let the symbols reveal the path." + System.Environment.NewLine +
                         "Look beyond the physical, and let the doors guide your way."+ System.Environment.NewLine +
                         "[♥] Password: $ ";
    string wrongPasswordString = "[♦] Incorrect password! The Joker's riddles are tricky. Try again." + System.Environment.NewLine +
                         "[♦] HINT: The Joker's secret lies in the cards he loves." + System.Environment.NewLine +
                         "Observe the suits closely, and let the symbols reveal the path." + System.Environment.NewLine +
                         "Look beyond the physical, and let the doors guide your way."+ System.Environment.NewLine +
                         "[♥] Password: $ ";
    string correctPasswordString = "";



    void Start()
    {
        DisplayTextString = DisplayText.text;
        animateText = StartCoroutine(AnimateText(startString));
    }

    void Update()
    {
        if (canType && focus && blinking == null)
        {
            blinking = StartCoroutine(Blinking());
        }
        else if ((!canType || !focus) && blinking != null)
        {
            StopCoroutine(blinking);
            if (animateText == null)
            {
                DisplayText.text = DisplayTextString;
            }
            blinking = null;
        }

        if (!canType || !focus) { return; }

        if (Input.inputString != "")
        {

            char[] characters = Input.inputString.ToCharArray();
            foreach (char c in characters)
            {
                if (c == '\b') //Input is backspace
                {
                    if (lastInput.Length != 0)
                    {
                        lastInput = lastInput.Remove(lastInput.Length - 1, 1);
                        DisplayTextString = DisplayTextString.Remove(DisplayTextString.Length - 1, 1);
                        DisplayText.text = DisplayTextString;
                    }
                    return;
                }
                if (c == '\r') //Input is Enter
                {
                    EvalInput();
                    return;
                }
            }
            lastInput += Input.inputString;
            DisplayTextString += Input.inputString;
            DisplayText.text = DisplayTextString;
            return;
        }
    }


    void EvalInput()
    {
        if (lastInput == password)
        {
            completed?.Invoke();
            animateText = StartCoroutine(AnimateText(correctPasswordString));
        }
        else
        {
            wrongAnswer?.Invoke();
            animateText = StartCoroutine(AnimateText(wrongPasswordString));
        }
        
        lastInput = "";
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
            DisplayText.text += "_";
            yield return new WaitForSeconds(0.5f);
            DisplayText.text = DisplayTextString;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator AnimateText(string text)
    {
        if (blinking != null)
        {
            StopCoroutine(blinking);
            blinking = null;
        }
        canType = false;
        DisplayText.text = "";
        DisplayTextString = text;
        char[] characters = text.ToCharArray();
        foreach (char c in characters)
        {
            DisplayText.text += c;
            yield return new WaitForSeconds(0.01f);
        }
        canType = true;
        animateText = null;
    }
}
