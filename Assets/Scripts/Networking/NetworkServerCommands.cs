using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkServerCommands : NetworkBehaviour {

    private NetworkScoreManager m_netScoreManager;

    public override void OnStartServer()
    {
        m_netScoreManager = FindObjectOfType<NetworkScoreManager>();
    }

    [Command]
    public void CmdAddPoints(int localPlayerID, int points, string itemName)
    {
        Debug.Log("Adding points for " + localPlayerID);
        m_netScoreManager.AddPoints(localPlayerID, points, itemName);
    }

    [Command]
    public void CmdAddPlayer()//int num)
    {
        Debug.Log("New Player Added");
        //sv_numOfPlayers = num;
        m_netScoreManager.sv_numOfPlayers++;
        m_netScoreManager.sv_scores.Add(0);
        m_netScoreManager.sv_currentMultiplier.Add(1);
    }
}
