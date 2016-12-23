using UnityEngine;
using System.Collections;

public class Walk : AbstractBehaviour {

    public float m_speed = 50f;
    private float m_currentSpeed;
    private bool m_canWalk = true;

    private TimeManager m_timeManager;

    void Start()
    {
        m_timeManager = FindObjectOfType<TimeManager>();
        m_currentSpeed = m_speed;
    }
	
	void FixedUpdate () {
        m_canWalk = !m_timeManager.IsGameOver();

        if (m_canWalk)
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);

            // Always up, down, left right
            bool up = m_inputState.GetButtonValue(m_inputButtons[0]);
            bool down = m_inputState.GetButtonValue(m_inputButtons[1]);
            bool left = m_inputState.GetButtonValue(m_inputButtons[2]);
            bool right = m_inputState.GetButtonValue(m_inputButtons[3]);

            //Debug.Log(up + " " + down + " " + left + " " + right);

            // If there is a second up button add it to the fifth element.  Used in multiplayer
            bool up2 = false;
            if (m_inputButtons.Length > 4)
                up2 = m_inputState.GetButtonValue(m_inputButtons[4]);

            // Make sure the player can't walk when in the air
            if (m_inputState.absVelY < 0.01f)
            {
                Vector3 newDirection = Vector3.zero;
                // Add force based on input
                if (up || up2)
                {
                    newDirection += m_currentSpeed * transform.forward;
                }

                if (down)
                {
                    newDirection += m_currentSpeed * -transform.forward;
                }

                if (left)
                {
                    newDirection += m_currentSpeed * -transform.right;
                }

                if (right)
                {
                    newDirection += m_currentSpeed * transform.right;
                }

                //Debug.Log(gameObject.name + " " + newDirection);

                m_rb.velocity = newDirection;
            }
        }
        else
        {
            m_rb.velocity = new Vector3(0, m_rb.velocity.y, 0);
        }
    }

    public void SetCanWalk(bool b)
    {
        m_canWalk = b;
    }

    public void SetSpeedByWeight(int weight)
    {
        float speedMultiplier = 1.0f;
        switch(weight)
        {
            case 10:
            default:
                speedMultiplier = 0.5f;
                break;
            case 9:
                speedMultiplier = 0.6f;
                break;
            case 8:
                speedMultiplier = 0.65f;
                break;
            case 7:
                speedMultiplier = 0.7f;
                break;
            case 6:
                speedMultiplier = 0.75f;
                break;
            case 5:
                speedMultiplier = 0.8f;
                break;
            case 4:
                speedMultiplier = 0.85f;
                break;
            case 3:
                speedMultiplier = 0.9f;
                break;
            case 2:
                speedMultiplier = 0.95f;
                break;
            case 1:
            case 0:
                speedMultiplier = 1.0f;
                break;
        }
        m_currentSpeed = m_speed * speedMultiplier;
    }
}
