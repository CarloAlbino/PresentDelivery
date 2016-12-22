using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObject : MonoBehaviour {

    // Make sure the 2 arrays match
    // Also make sure the rarest item is last
    public InventoryObject[] items;
    // Calculate percentages between 0 and 1. The total of all percentages must be 1.  In the future find a way to automatically calculate this when moving sliders in the editor.
    public float[] itemPercentage;
    private SpriteRenderer m_spriteRenderer;
    private InventoryObject m_currentItem;

    public Transform m_stopPosition;
    public Transform m_startPosition;
    public float m_moveSpeed = 1.0f;

    private bool m_canPickup = false;

	void Start ()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        SwapItem();
    }

	void Update ()
    {
        if (transform.position.z < m_stopPosition.position.z)
        {
            transform.Translate(Vector3.forward * m_moveSpeed * Time.deltaTime);
        }
        else
        {
            m_canPickup = true;
        }
	}

    public void SwapItem()
    {
        float rand = Random.Range(0.0f, 1.0f);

        for(int i = itemPercentage.Length; i > -1; i--)
        {
            if(rand > itemPercentage[i])
            {
                m_canPickup = false;
                transform.position = m_startPosition.position;
                m_spriteRenderer.sprite = items[i].m_sprite;
                m_currentItem = items[i];
                break;
            }
        }
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
