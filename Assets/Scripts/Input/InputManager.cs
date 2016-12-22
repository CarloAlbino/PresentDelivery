using UnityEngine;
using System.Collections;

public enum Buttons
{
    // First Player
    IE_1_Up_W,
    IE_1_Down_S,
    IE_1_Left_A,
    IE_1_Right_D,
    // Second Player
    IE_2_Up_Arrow,
    IE_2_Down_Arrow,
    IE_2_Left_Arrow,
    IE_2_Right_Arrow,
    // Third Player
    IE_3_Up_1_G,
    IE_3_Up_2_H,
    IE_3_Down_B,
    IE_3_Left_V,
    IE_3_Right_N,
    // Fourth Player
    IE_4_Up_O,
    IE_4_Down_L,
    IE_4_Left_K,
    IE_4_Right_SemiColon,
    // Other
    IE_Esc,
    IE_Space,
    IE_Enter
}

public enum Condition
{
    E_GreaterThan,
    E_LessThan
}

[System.Serializable]
public class InputAxisState
{
    public string axisName;
    public float offValue;
    public Buttons button;
    public Condition condition;

    public bool value
    {
        get
        {
            var val = Input.GetAxis(axisName);
            
            switch(condition)
            {
                case Condition.E_GreaterThan:
                    return val > offValue;
                case Condition.E_LessThan:
                    return val < offValue;
            }

            return false;
        }
    }
}

public class InputManager : MonoBehaviour {

    public InputAxisState[] m_inputs;
    public InputState m_inputState;
	
	// Update is called once per frame
	void Update () {
	    foreach(var input in m_inputs)
        {
            m_inputState.SetButtonValue(input.button, input.value);
        }
	}
}
