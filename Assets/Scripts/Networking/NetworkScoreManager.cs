using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkScoreManager : NetworkBehaviour {

    [SyncVar]
    public SyncListInt sv_scores = new SyncListInt();
    [SyncVar]
    public int sv_numOfPlayers = 0;
    [SyncVar]
    public SyncListInt sv_currentMultiplier = new SyncListInt();
    [SyncVar]
    public SyncListUInt sv_playerNetIDs = new SyncListUInt();

    private string m_lastItem = "";
    public int m_maxMultiplier = 5;
    public Text[] m_multiplierText = new Text[4];
    private AudioSource m_audioSource;
    
    void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
        for (int i = 0; i < sv_numOfPlayers; i++)
        {
            if (sv_currentMultiplier[i] > 1)
            {
                m_multiplierText[i].text = "x" + sv_currentMultiplier[i];
            }
            else
            {
                m_multiplierText[i].text = " ";
            }
        }

        Debug.Log("Num of players connecteed " + NetworkServer.connections.Count);
        foreach (NetworkConnection n in NetworkServer.connections)
        {
            Debug.Log(n.connectionId);
        }
    }

    public void AddPoints(int localPlayerID, int points, string itemName)
    {
        Debug.Log("Adding points for " + localPlayerID);
        if (itemName == m_lastItem && sv_currentMultiplier[localPlayerID] < m_maxMultiplier)
        {
            sv_currentMultiplier[localPlayerID]++;
        }
        else
        {
            sv_currentMultiplier[localPlayerID] = 1;
        }

        m_lastItem = itemName;
        sv_scores[localPlayerID] += points * sv_currentMultiplier[localPlayerID];

        m_audioSource.Play();
    }

    [Command]
    public void CmdNewPlayer(uint newId)
    {
        sv_playerNetIDs.Add(newId);
    }
}
