using HighlightPlus;
using UnityEngine;

[RequireComponent(typeof(HighlightEffect))]
public abstract class HighlightHoverable : MonoBehaviour, IHoverable
{
    private HighlightEffect m_hightlighter = null;

    protected virtual void Awake()
    {
        m_hightlighter = GetComponent<HighlightEffect>();
    }

    public virtual void OnEnter()
    {
        if (m_hightlighter != null) 
        { 
            m_hightlighter.enabled = true;
            m_hightlighter.highlighted = true;
        }
    }

    public virtual void OnExit()
    {
        if (m_hightlighter != null)
        {
            m_hightlighter.enabled = false;
            m_hightlighter.highlighted = false;
        }
    }
}
