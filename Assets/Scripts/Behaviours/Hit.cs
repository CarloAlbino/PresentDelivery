using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : AbstractBehaviour {

    public float m_hitSpeed = 50.0f;
    public float m_invincibilityTime = 1.0f;
    private float m_currentInvincibilityTime = 2.0f;

    void Update()
    {
        m_currentInvincibilityTime += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(m_inputState.absVelZ < 0.1f)
        {
            if(other.CompareTag("Obstacle"))
            {
                m_rb.AddForce(Vector3.up * m_hitSpeed);
            }
        }
    }
}
