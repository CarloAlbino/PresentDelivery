using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour {

    public Material m_localPlayerColor;
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

        if (!hasAuthority)
        {
            m_playerNum = NetworkServer.connections.Count - 1;
            // Set the SyncVars
            m_netScoreManager.sv_numOfPlayers = NetworkServer.connections.Count;
            m_netScoreManager.sv_scores.Add(0);
            m_netScoreManager.sv_currentMultiplier.Add(1);
        }
    }

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material = m_localPlayerColor;
        GetComponent<Walk>().isLocal = true;
        GetComponentInChildren<Text>().text = "You\nv";

        if(m_netScoreManager == null)
        {
            m_netScoreManager = FindObjectOfType<NetworkScoreManager>();
        }
    }

    public uint GetPlayerNetID()
    {
        return netId.Value;
    }
}
