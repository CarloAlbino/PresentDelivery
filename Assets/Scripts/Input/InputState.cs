using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ButtonState
{
    public bool value;
    public float holdTime = 0;
}

public enum Directions
{
    Up = 1, 
    Down = -1,
    Right = 1,
    Left = -1
}

public class InputState : MonoBehaviour {

    public Directions direction = Directions.Right;
    public float absVelX = 0.0f;
    public float absVelY = 0.0f;
    public float absVelZ = 0.0f;

    private Rigidbody m_rb;
    private Dictionary<Buttons, ButtonState> buttonStates = new Dictionary<Buttons, ButtonState>();

    void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        absVelX = Mathf.Abs(m_rb.velocity.x);
        absVelY = Mathf.Abs(m_rb.velocity.y);
        absVelZ = Mathf.Abs(m_rb.velocity.z);
    }

    public void SetButtonValue(Buttons key, bool value)
    {
        if (!buttonStates.ContainsKey(key))
            buttonStates.Add(key, new ButtonState());

        var state = buttonStates[key];

        if(state.value && !value)
        {
            //Debug.Log("Button " + key + " released " + state.holdTime);
            state.holdTime = 0;
        }else if(state.value && value)
        {
            state.holdTime += Time.deltaTime;
            //Debug.Log("Button " + key + " down " + state.holdTime);
        }

        state.value = value;
    }

    public bool GetButtonValue(Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].value;
        else
            return false;
    }

    public float GetButtonHoldTime(Buttons key)
    {
        if (buttonStates.ContainsKey(key))
            return buttonStates[key].holdTime;
        else
            return 0;
    }
}
