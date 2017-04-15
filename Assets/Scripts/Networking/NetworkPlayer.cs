using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour {

    // Possible player colours
    public Material[] m_PlayerColors;

    // The player object's player number (player 1, player 2, etc..) 
    // NOTE: When the variable changes on the object with authority,
    //       the ChangeColor function is called and that updates the
    //       colour of the player object on all connected users (replicas and master)
    [SyncVar (hook = "ChangeColor")]
    public int m_playerNum = 0;

    // Reference to the score manager
    private NetworkScoreManager m_netScoreManager;

    // When the object gets spawned because a client connects
    // Only runs for the client
    public override void OnStartClient()
    {
        // NOTE:
        // All scoring will be done the server side.
        // All that the clients are doing is moving the player object
        // to control their server replica.  The game is simple enough that
        // with a good connection everything should be fine.

        // Get the reference
        m_netScoreManager = FindObjectOfType<NetworkScoreManager>();

        // If the object is the on the server add a player.
        if (!isLocalPlayer && isServer)
        {
            // This is a replica on the server
            // Set the SyncVars
            m_netScoreManager.sv_numOfPlayers = NetworkServer.connections.Count;
            m_netScoreManager.sv_scores.Add(0);
            m_netScoreManager.sv_currentMultiplier.Add(1);

            // Save the Unique Network ID
            m_netScoreManager.sv_playerNetIDs.Add(netId.Value);
            for (int i = 0; i < m_netScoreManager.sv_playerNetIDs.Count; i++)
            {
                // Compares the saved UNetID to the this player's UNetID
                // If they are the same the index of that ID on the server
                // Becomes the player's number (player 1, player 2, etc.)
                if (m_netScoreManager.sv_playerNetIDs[i] == netId.Value)
                {
                    m_playerNum = i; // NOTE: Changing this changes the colour of the player on all connected users
                    break;
                }
            }

            //Debug.Log("New Player spawned");
        }

        // Change the local version's colour
        GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];
    }

    // If the new player is the local player
    public override void OnStartLocalPlayer()
    {
        // Grab the reference if it doesn't exist
        if (m_netScoreManager == null)
        {
            m_netScoreManager = FindObjectOfType<NetworkScoreManager>();
        }

        // Allow the user to control their player
        GetComponent<Walk>().isLocal = true;
        // Add a text bubble to show which player the user is controlling
        GetComponentInChildren<Text>().text = "You\nv";
    }

    // Change the colour of the player
    public void ChangeColor(int playerNum)
    {
        GetComponent<MeshRenderer>().material = m_PlayerColors[m_playerNum];
    }

}
