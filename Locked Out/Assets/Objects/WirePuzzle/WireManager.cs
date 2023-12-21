using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WireManager : Puzzle
{
    [SerializeField] private Wire[] m_order;
    [Space]
    [SerializeField] private UnityEvent m_onCorrectWireCut;
    [SerializeField] private UnityEvent m_onWrongWireCut;
    [Space]
    [SerializeField] private Material m_colorScreen;
 
    private LinkedList<Wire> m_remainingWires = new();
    private LinkedListNode<Wire> m_primedWire = null;

    private void Awake()
    {
        //  Sample all wire references in a linked list, and subscribe to their 'OnCut' function.
        for (int i = 0; i < m_order.Length; i++)
        {
            m_remainingWires.AddLast(m_order[i]);
            m_order[i].onCut.AddListener(OnWireCut);
        }

        //  Set the first wire in the list to be the one to cut.
        PrimeWire(m_remainingWires.First);
    }

    /// <summary>
    /// Called whenever a wire has been cut.
    /// </summary>
    private void OnWireCut(Wire _wire)
    {
        //  Call corresponding events based on whether the correct or incorrect wire is cut.
        if (_wire.color == m_primedWire.Value.color)    { m_onCorrectWireCut.Invoke(); }
        else                                            { m_onWrongWireCut.Invoke(); }

        //  Destroy, and remove the cut wire from the list.
        m_remainingWires.Remove(_wire);
        Destroy(_wire.gameObject);

        //  Mark the puzzle as solved if no more wires need to be cut.
        if (m_remainingWires.Count <= 0)
        {
            m_onSolved.Invoke();
            return;
        }

        //  Prime the wire next in line.
        PrimeWire(m_remainingWires.First);
    }

    /// <summary>
    /// Sets the passed in wire as the wire to be cut.
    /// </summary>
    private void PrimeWire(LinkedListNode<Wire> _wireNode)
    {
        m_primedWire = _wireNode;
        m_colorScreen.SetColor("_EmissionColor", _wireNode.Value.cosmeticColor);
    }
}
