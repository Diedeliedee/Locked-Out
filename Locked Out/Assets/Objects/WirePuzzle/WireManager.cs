using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WireManager : Puzzle
{
    [SerializeField] private List<Wire> m_order = new();
    [Space]
    [SerializeField] private UnityEvent m_onCorrectWireCut;
    [SerializeField] private UnityEvent m_onWrongWireCut;
    [Space]
    [SerializeField] private Renderer m_colorScreen;
    [Space]
    [SerializeField] private Color[] m_screenColors;
    [Space]
    [SerializeField] private TextMeshProUGUI m_colorBlindText;

    [SerializeField] private string[] m_colorBlindColorString;

    private Wire m_nextWire = null;




    private void Awake()
    {
        //  Sample all wire references in a linked list, and subscribe to their 'OnCut' function.
        for (int i = 0; i < m_order.Count; i++)
        {
            m_order[i].onCut.AddListener(OnWireCut);
        }

        //  Set the first wire in the list to be the one to cut.
        PrimeWire(m_order);
    }

    /// <summary>
    /// Called whenever a wire has been cut.
    /// </summary>
    private void OnWireCut(Wire _wire)
    {
        //  Call corresponding events based on whether the correct or incorrect wire is cut.
        if (_wire == m_nextWire) { m_onCorrectWireCut?.Invoke(); Debug.Log("correct Wire"); }
        else { m_onWrongWireCut?.Invoke();  Debug.Log("Wrong Wire"); }

        //  Destroy, and remove the cut wire from the list.
        m_order.Remove(_wire);
        Destroy(_wire.gameObject);

        //  Mark the puzzle as solved if no more wires need to be cut.
        if (m_order.Count <= 0)
        {
            Debug.Log("Solved");
            m_onSolved.Invoke();
            return;
        }

        //  Prime the wire next in line.
        PrimeWire(m_order);
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void PrimeWire(List<Wire> _wires)
    {
        // m_screenColors:
        // 0 = blue
        // 1 = yellow
        // 2 = purple
        // 3 = red
        // 4 = green;
        switch (_wires.Count)
        {
            case 6:
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[0];
                        m_colorBlindText.text = m_colorBlindColorString[0];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[1];
                        m_colorBlindText.text = m_colorBlindColorString[1];
                        break;
                    case 2:
                        m_colorScreen.material.color = m_screenColors[2];
                        m_colorBlindText.text = m_colorBlindColorString[2];
                        break;
                    case 3:
                        m_colorScreen.material.color = m_screenColors[3];
                        m_colorBlindText.text = m_colorBlindColorString[3];
                        break;
                }
                //If there are no yellow wires and the screen is purple, cut the third wire.
                if (m_colorScreen.material.color == m_screenColors[2])
                {
                    if(WireCountByColor(_wires,WireColor.Yellow) == 0)
                    {
                        m_nextWire = _wires[2];
                        return;
                    }
                }
                //Otherwise, if there is exactly one yellow wire and there is more than one red wire, cut the fourth wire.
                if(WireCountByColor(_wires,WireColor.Yellow) == 1 && WireCountByColor(_wires,WireColor.Red) > 1)
                {
                    m_nextWire = _wires[3];
                    return;
                }
                //Otherwise, if there are no red wires and the screen is blue, cut the last wire.
                if(m_colorScreen.material.color == m_screenColors[0])
                {
                    if(WireCountByColor(_wires,WireColor.Red) == 0)
                    {
                        m_nextWire = _wires[5];
                        return;
                    }
                }
                //Otherwise, If there are no green wires and the screen is not yellow, cut the fifth wire.
                if (m_colorScreen.material.color != m_screenColors[1])
                {
                    if(WireCountByColor(_wires,WireColor.Green) == 0)
                    {
                        m_nextWire = _wires[4];
                        return;
                    }
                }
                //Otherwise, cut the first wire when the screen is blue.
                m_colorScreen.material.color = m_screenColors[0];
                m_colorBlindText.text = m_colorBlindColorString[0];
                m_nextWire = _wires[0];
                break;
            case 5:
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[0];
                        m_colorBlindText.text = m_colorBlindColorString[0];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[3];
                        m_colorBlindText.text = m_colorBlindColorString[3];
                        break;
                    case 2:
                        m_colorScreen.material.color = m_screenColors[2];
                        m_colorBlindText.text = m_colorBlindColorString[2];
                        break;
                    case 3:
                        m_colorScreen.material.color = m_screenColors[4];
                        m_colorBlindText.text = m_colorBlindColorString[4];
                        break;
                }
                //If the last wire is green and the screen is purple, cut the fourth wire.
                if (_wires[4] && m_colorScreen.material.color == m_screenColors[2])
                {
                    m_nextWire = _wires[3];
                    return;
                }
                //Otherwise, if there is exactly one red wire and more than one yellow wire, cut the first wire.
                if (WireCountByColor(_wires, WireColor.Red) == 1 && WireCountByColor(_wires, WireColor.Yellow) > 1)
                {
                    m_nextWire = _wires[0];
                    return;
                }
                //Otherwise, if there are no green wires, cut the second wire when the screen is purple.
                if (WireCountByColor(_wires, WireColor.Green) == 0 && m_colorScreen.material.color == m_screenColors[2])
                {
                    m_nextWire = _wires[1];
                    return;
                }
                //Otherwise, cut the first wire.
                m_nextWire = _wires[0];
                break;
            case 4:
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[0];
                        m_colorBlindText.text = m_colorBlindColorString[0];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[4];
                        m_colorBlindText.text = m_colorBlindColorString[4];
                        break;
                }
                //If there is more than one red wire and the screen is green, cut the last red wire.
                if (m_colorScreen.material.color == m_screenColors[4])
                {
                    Wire lastRedWire = null;
                    if (WireCountByColor(_wires, WireColor.Red, ref lastRedWire) > 1)
                    {
                        m_nextWire = lastRedWire;
                        return;
                    }
                }
                //Otherwise, if the first wire is yellow and the screen is blue, cut the first wire.
                if (_wires[0].color == WireColor.Yellow && m_colorScreen.material.color == m_screenColors[0])
                {
                    m_nextWire = _wires[0];
                    return;
                }
                //Otherwise, if there is exactly one blue wire, cut the first wire.
                if (WireCountByColor(_wires, WireColor.Blue) == 1)
                {
                    m_nextWire = _wires[0];
                    return;
                }
                //Otherwise, cut the second wire.
                m_nextWire = _wires[1];
                break;
            case 3:
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[0];
                        m_colorBlindText.text = m_colorBlindColorString[0];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[3];
                        m_colorBlindText.text = m_colorBlindColorString[3];
                        break;
                }
                //If there are no red wires and the screen is blue, cut the second wire.
                if (WireCountByColor(_wires, WireColor.Red) == 0 && m_colorScreen.material.color == m_screenColors[0])
                {
                    m_nextWire = _wires[1];
                    return;
                }
                //Otherwise, if the last wire is blue, cut the last wire but not if the screen is red, if it is red instead cut the first wire.
                if (_wires[2].color == WireColor.Blue)
                {
                    if (m_colorScreen.material.color == m_screenColors[3])
                    {
                        m_nextWire = _wires[0];
                        return;
                    }
                    else
                    {
                        m_nextWire = _wires[2];
                        return;
                    }
                }
                //Otherwise, if there is more than one blue wire, cut the last blue wire.
                Wire lastBlueWire = null;
                if (WireCountByColor(_wires, WireColor.Blue, ref lastBlueWire) > 1)
                {
                    m_nextWire = lastBlueWire;
                    return;
                }
                //Otherwise, cut the last wire when the screen is green and the second when it’s red.
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[4];
                        m_colorBlindText.text = m_colorBlindColorString[4];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[3];
                        m_colorBlindText.text = m_colorBlindColorString[3];
                        break;
                }
                if (m_colorScreen.material.color == m_screenColors[4])
                {
                    m_nextWire = _wires[2];
                    return;
                }
                else
                {
                    m_nextWire = _wires[1];
                }
                break;
            case 2:
                //If the last wire is green, cut the last wire.
                if (_wires[1].color == WireColor.Green)
                {
                    m_nextWire = _wires[1];
                    m_colorScreen.material.color = m_screenColors[0];
                    m_colorBlindText.text = m_colorBlindColorString[0];
                    return;
                }
                switch (UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        m_colorScreen.material.color = m_screenColors[0];
                        m_colorBlindText.text = m_colorBlindColorString[0];
                        break;
                    case 1:
                        m_colorScreen.material.color = m_screenColors[3];
                        m_colorBlindText.text = m_colorBlindColorString[3];
                        break;
                    case 2:
                        m_colorScreen.material.color = m_screenColors[2];
                        m_colorBlindText.text = m_colorBlindColorString[2];
                        break;
                    case 3:
                        m_colorScreen.material.color = m_screenColors[4];
                        m_colorBlindText.text = m_colorBlindColorString[4];
                        break;
                }
                //Otherwise, if the screen is red, cut the first wire.
                if (m_colorScreen.material.color == m_screenColors[3])
                {
                    m_nextWire = _wires[0];
                    return;
                }
                //Otherwise, If the first wire is orange, cut the second wire.
                if (_wires[1].color == WireColor.Red)
                {
                    m_nextWire = _wires[1];
                    return;
                }
                //Otherwise, if there is exactly one blue wire and the screen is not green, cut the first wire.
                if (m_colorScreen.material.color != m_screenColors[4])
                {
                    if (WireCountByColor(_wires, WireColor.Blue) == 1)
                    {
                        m_nextWire = _wires[0];
                        return;
                    }
                }
                //Otherwise, if the screen is purple, cut the second wire.
                if (m_colorScreen.material.color == m_screenColors[2])
                {
                    m_nextWire = _wires[1];
                    return;
                }
                //Otherwise, cut the first wire when the screen is not blue.
                if (m_colorScreen.material.color != m_screenColors[0])
                {
                    m_nextWire = _wires[0];
                    return;
                }
                //Otherwise, cut the last wire.
                m_nextWire = _wires[1];
                break;
            default:
                //Cut the last wire.
                m_colorScreen.material.color = Color.black;
                m_colorBlindText.text = "";
                if (_wires.Count > 0)
                {
                    m_nextWire = _wires[0];
                }
                break;
        }
    }



    protected int WireCountByColor(List<Wire> _wires, WireColor color)
    {
        int WireCount = 0;
        foreach (Wire w in _wires)
        {
            if (w.color == color)
            {
                WireCount++;
            }
        }
        return WireCount;
    }
    protected int WireCountByColor(List<Wire> _wires, WireColor color, ref Wire lastWire)
    {
        int WireCount = 0;
        foreach (Wire w in _wires)
        {
            if (w.color == color)
            {
                WireCount++;
                lastWire = w;
            }
        }
        return WireCount;
    }

}
