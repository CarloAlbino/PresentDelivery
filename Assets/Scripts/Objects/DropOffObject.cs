using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DropOffObject : MonoBehaviour {

    [Range(1, 4)]
    public int m_sentByPlayer = 1;
    [SerializeField]
    private InventoryObject m_item;
    private Rigidbody m_rb;
    private SpriteRenderer m_renderer;

    // This class should be based off of the smae class that an obstacle should be based off of.
    public float m_speed = 100.0f;
    public float m_maxSpeed = 100.0f;
    public Vector3 m_direction = Vector3.right;

    private int m_points = 0;
    private string m_name = " ";
    private GameManager m_gameManager;

    void Start ()
    {
        m_rb = GetComponent<Rigidbody>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        if(!m_renderer)
        {
            Debug.LogError("NO SPRITE RENDERER ATTACHED!");
        }
        m_gameManager = FindObjectOfType<GameManager>();
	}

	void FixedUpdate ()
    {
        if (m_rb.velocity.magnitude < m_maxSpeed)
            m_rb.AddForce(m_direction * m_speed, ForceMode.Force);
    }

    void OnTriggerEnter(Collider other)
    {
        // When colliding with the destination the points should be calculated and destroyed
        if(other.CompareTag("Destination"))
        {
            m_gameManager.AddPoints(m_sentByPlayer, m_points, m_name);
            Destroy(this.gameObject);
        }
    }

    public void SetItem(InventoryObject item)
    {
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        if (!m_renderer)
        {
            Debug.LogError("NO SPRITE RENDERER ATTACHED!");
        }
        InventoryObject tempObject = Instantiate(item, Vector3.zero, Quaternion.identity) as InventoryObject;
        //Debug.Log(tempObject);
        m_item = tempObject;
        m_name = m_item.name;
        m_renderer.sprite = m_item.m_sprite;
        m_points = m_item.m_points;
        Destroy(tempObject.gameObject);
    }
}
