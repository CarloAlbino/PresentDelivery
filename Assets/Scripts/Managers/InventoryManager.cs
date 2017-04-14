using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class InventoryManager : NetworkBehaviour {

    private Queue<InventoryObject> m_heldItems = new Queue<InventoryObject>();
    public int m_maxHeldItems = 5;
    public DropOffObject m_dropOffObject;
    public Transform m_dropOffPosition;
    private int m_currentWeight = 0;
    public Text m_label;
    private string m_defaultLabel;
    //[SerializeField]
    private Walk m_playerWalk;
    public GameObject m_droppedItems;
    private bool m_taken = false;
    private NetworkPlayer m_netPlayer;

	void Start ()
    {
        m_netPlayer = GetComponent<NetworkPlayer>();
        m_playerWalk = GetComponent<Walk>();
        m_defaultLabel = m_label.text;
	}

    void Update()
    {
        if(m_heldItems.Count == m_maxHeldItems)
        {
            m_label.text = "FULL\nv";
        }
        else
        {
            m_label.text = m_defaultLabel;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Pick up items
        if (!m_taken)   // Can the local player pickup items, if a player is not the host, there is a delay, this fixes that delay
        {
            if (m_heldItems.Count < m_maxHeldItems)
            {
                if (other.GetComponent<PickupObject>())
                {
                    PickupObject pickup = other.GetComponent<PickupObject>();
                    if (pickup != null)
                    {
                        if (pickup.CanPickUp())
                        {
                            m_taken = true;
                            m_currentWeight += pickup.GetItem().m_weight;
                            m_heldItems.Enqueue(pickup.GetItem());
                            pickup.CmdSwapItem();
                            m_playerWalk.SetSpeedByWeight(m_currentWeight);
                            transform.localScale = Vector3.one * (1.0f + (float)m_currentWeight / 25.0f);
                            //Debug.Log("Current Weight: " + m_currentWeight +  " " + Vector3.one * (1.0f + (float)m_currentWeight / 25.0f));
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    { 
        // Drop off items
        if(m_heldItems.Count > 0)
        {
            if (other.CompareTag("DropOff"))
            {
                m_playerWalk.SetCanWalk(false);
                StartCoroutine(DropOff());
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PickupObject>())
        {
            // Allow the player to pickup the items again.
            m_taken = false;
        }
    }

    private IEnumerator DropOff()
    {
        // Stop player movement here
        m_playerWalk.SetCanWalk(false);
        yield return new WaitForSeconds(0.7f);
        DropOffObject dropOff = Instantiate(m_dropOffObject, m_dropOffPosition.position, Quaternion.identity) as DropOffObject;
        dropOff.m_localPlayerNum = m_netPlayer.m_playerNum; // Set the player number that dropped off the item
        m_currentWeight -= m_heldItems.Peek().m_weight;
        if(m_currentWeight < 0)
        {
            m_currentWeight = 0;
        }
        dropOff.SetItem(m_heldItems.Peek());
        m_heldItems.Dequeue();

        if(m_heldItems.Count > 0)
        {
            StartCoroutine(DropOff());
        }
        else
        {
            // Re-enable player movement here
            m_playerWalk.SetCanWalk(true);
        }
        m_playerWalk.SetSpeedByWeight(m_currentWeight);
        transform.localScale = Vector3.one * (1.0f + (float)m_currentWeight / 25.0f);
    }

    public void EmptyInventory()
    {
        if (m_heldItems.Count > 0)
            Instantiate(m_droppedItems, transform.position, Quaternion.identity);
        m_heldItems.Clear();
        m_currentWeight = 0;
        m_playerWalk.SetSpeedByWeight(m_currentWeight);
        transform.localScale = Vector3.one * (1.0f + (float)m_currentWeight / 25.0f);
    }
}
