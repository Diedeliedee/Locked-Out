using Highlighters;
using UnityEngine;

[RequireComponent(typeof(Highlighter))]
public abstract class HighlightHoverable : MonoBehaviour, IHoverable
{
    private Highlighter m_hightlighter = null;

    protected virtual void Awake()
    {
        m_hightlighter = GetComponent<Highlighter>();
    }

    public virtual void OnEnter()
    {
       if (m_hightlighter != null) m_hightlighter.enabled = true;
    }

    public virtual void OnExit()
    {
        if (m_hightlighter != null) m_hightlighter.enabled = false;
    }
}
