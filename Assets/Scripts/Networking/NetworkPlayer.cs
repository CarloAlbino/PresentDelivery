using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour {

    public Material m_localPlayerColor;

    public Material[] m_PlayerColors;

    [SyncVar (hook = "ChangeColor")]
    public int m_playerNum = 0;

    private NetworkScoreManager m_netScoreManager;

	void Start ()
    {

	}

    void Update ()
    {

    }

    public override void OnStartClient()
    {
        // If the object is the on the server add a player.
        // All scoring will be done the server side.
        // All that the clients are doing is moving the player object
        // to control their server replica.  The game is simple enough that
        // with a good connection everything should be fine.
        m_netScoreManager = FindObjectOfType<NetworkScoreManager>();

        if (!isLocalPlayer && isServer)
        {
            // This is a replica on the server
            // Set the SyncVars
            m_netScoreManager.sv_numOfPlayers = NetworkServer.connections.Count;
            m_netScoreManager.sv_scores.Add(0);
            m_netScoreManager.sv_currentMultiplier.Add(1);

            m_netScoreManager.sv_playerNetIDs.Add(netId.Value);
            for (int i = 0; i < m_netScoreManager.sv_playerNetIDs.Count; i++)
            {
                if (m_netScoreManager.sv_playerNetIDs[i] == netId.Value)
                {
                    m_playerNum = i;
                    break;
                }
            }

            //GetComponent<MeshRenderer>().material = m_PlayerColors[m_netScoreManager.sv_numOfPlayers - 1];
            Debug.Log("New PLayer spawned");
        }
        //else
        //{
        //    // If a replica on the client side
        //    GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];
        //}

        GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];
    }

    public override void OnStartLocalPlayer()
    {
        if (m_netScoreManager == null)
        {
            m_netScoreManager = FindObjectOfType<NetworkScoreManager>();
        }

        //GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];// m_localPlayerColor;
        GetComponent<Walk>().isLocal = true;
        GetComponentInChildren<Text>().text = "You\nv";
    }

    public void ChangeColor(int playerNum)
    {
        GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];
    }

    public uint GetPlayerNetID()
    {
        return netId.Value;
    }
}
