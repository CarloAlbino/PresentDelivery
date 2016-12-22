using UnityEngine;
using System.Collections;

public abstract class AbstractBehaviour : MonoBehaviour {

    public Buttons[] m_inputButtons;

    protected InputState m_inputState;
    protected Rigidbody m_rb;

    protected virtual void Awake()
    {
        m_inputState = GetComponent<InputState>();
        m_rb = GetComponent<Rigidbody>();
    }
}
