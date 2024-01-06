using Highlighters;
using UnityEngine;

[RequireComponent(typeof(Highlighter))]
public abstract class HighlightInteractable : MonoBehaviour, IInteractable
{
    private Highlighter m_hightlighter = null;

    private void Awake()
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

    public abstract void OnInteract();
}
