using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : AbstractBehaviour {

    public float m_hitSpeed = 50.0f;
    public float m_invincibilityTime = 1.0f;
    private float m_currentInvincibilityTime = 2.0f;
    public int m_defaultLayer;
    public int m_hitLayer;

    void Update()
    {
        m_currentInvincibilityTime += Time.deltaTime;
    }

    void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collision");
        if(m_inputState.absVelY < 0.1f)
        {
            if(other.collider.CompareTag("Obstacle"))
            {
                //Debug.Log(other.collider.name);
                gameObject.layer = m_hitLayer;
                m_rb.velocity = Vector3.zero;
                m_rb.AddForce(Vector3.up * m_hitSpeed, ForceMode.Impulse);
                // Empty inventory
                // Add some items being thrown out for visual confirmation.
                GetComponent<InventoryManager>().EmptyInventory();
            }
        }

        if(other.collider.CompareTag("Ground"))
        {
            gameObject.layer = m_defaultLayer;
        }
    }
}
