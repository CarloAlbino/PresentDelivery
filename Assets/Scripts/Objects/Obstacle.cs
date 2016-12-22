using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Obstacle : MonoBehaviour {

    public float m_speed = 100.0f;
    public float m_maxSpeed = 100.0f;
    public Vector3 m_direction = Vector3.right;
    private Rigidbody m_rb;

	void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate ()
    {
        if(m_rb.velocity.magnitude < m_maxSpeed)
            m_rb.AddForce(m_direction * m_speed, ForceMode.Force);
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Destroy"))
        {
            Destroy(this.gameObject);
        }
    }
}
