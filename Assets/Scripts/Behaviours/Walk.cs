using UnityEngine;
using System.Collections;

public class Walk : AbstractBehaviour {

    public float m_speed = 50f;
	
	void FixedUpdate () {
        m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);

        // Always up, down, left right
        bool up = m_inputState.GetButtonValue(m_inputButtons[0]);
        bool down = m_inputState.GetButtonValue(m_inputButtons[1]);
        bool left = m_inputState.GetButtonValue(m_inputButtons[2]);
        bool right = m_inputState.GetButtonValue(m_inputButtons[3]);

        //Debug.Log(up + " " + down + " " + left + " " + right);

        // If there is a second up button add it to the fifth element.  Used in multiplayer
        bool up2 = false;
        if(m_inputButtons.Length > 4)
            up2 = m_inputState.GetButtonValue(m_inputButtons[4]);

        // Make sure the player can't walk when in the air
        if (m_inputState.absVelY < 0.01f)
        {
            Vector3 newDirection = Vector3.zero;
            // Add force based on input
            if (up || up2)
            {
                newDirection += m_speed * transform.forward;
            }

            if (down)
            {
                newDirection += m_speed * -transform.forward;
            }

            if (left)
            {
                newDirection += m_speed * -transform.right;
            }

            if (right)
            {
                newDirection += m_speed * transform.right;
            }

            Debug.Log(gameObject.name + " " + newDirection);

            m_rb.velocity = newDirection;
        }

    }
}
