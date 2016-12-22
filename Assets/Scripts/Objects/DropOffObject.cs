using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SpriteRenderer))]
public class DropOffObject : MonoBehaviour {

    [Range(1, 4)]
    public int m_sentByPlayer = 1;
    private InventoryObject m_item;
    private Rigidbody m_rb;
    private SpriteRenderer m_renderer;

    // This class should be based off of the smae class that an obstacle should be based off of.
    public float m_speed = 100.0f;
    public float m_maxSpeed = 100.0f;
    public Vector3 m_direction = Vector3.right;

    void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_renderer = GetComponent<SpriteRenderer>();
	}

	void FixedUpdate ()
    {
        if (m_rb.velocity.magnitude < m_maxSpeed)
            m_rb.AddForce(m_direction * m_speed, ForceMode.Force);
    }

    void OnTriggerEnter(Collider other)
    {
        // When colliding with the destination the points should be calculated and destroyed

        // For now they will destroy themselves
        if(other.CompareTag("Destroy"))
            Destroy(this.gameObject);
    }

    public void SetItem(InventoryObject item)
    {
        m_item = item;
        m_renderer.sprite = m_item.m_sprite;
    }
}
