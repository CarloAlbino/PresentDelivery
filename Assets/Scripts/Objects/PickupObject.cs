using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {

    // Make sure the 2 arrays match
    // Also make sure the rarest item is last
    // Calculate percentages between 0 and 1. The total of all percentages must be 1.  In the future find a way to automatically calculate this when moving sliders in the editor.
    public InventoryObject[] items;
    public GameObject m_visualComponent;
    private SpriteRenderer m_spriteRenderer;
    private InventoryObject m_currentItem;
    //private BoxCollider m_collider;

    public Transform m_stopPosition;
    public Transform m_startPosition;
    public float m_moveSpeed = 1.0f;

    private bool m_canPickup = false;
    private AudioSource m_audioSource;

	void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //m_collider = GetComponent<BoxCollider>();
        SwapItem();
        m_audioSource.Stop();
    }

	void Update ()
    {
        if (m_visualComponent.transform.position.z < m_stopPosition.position.z)
        {
            m_visualComponent.transform.Translate(Vector3.forward * m_moveSpeed * Time.deltaTime);
        }
        else
        {
            m_canPickup = true;
        }
	}

    public void SwapItem()
    {
        float rand = Random.Range(0.0f, 1.0f);

        for(int i = items.Length - 1; i > -1; i--)
        {
            if(rand > items[i].m_spawnPercentage)
            {
                m_canPickup = false;
                m_visualComponent.transform.position = m_startPosition.position;
                m_spriteRenderer.sprite = items[i].m_sprite;
                m_currentItem = items[i];
                break;
            }
        }
        m_audioSource.Play();
    }

    public InventoryObject GetItem()
    {
        return m_currentItem;
    }

    public bool CanPickUp()
    {
        return m_canPickup;
    }
}
