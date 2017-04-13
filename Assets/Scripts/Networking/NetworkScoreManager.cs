using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkScoreManager : NetworkBehaviour {

    [SyncVar]
    public int[] sv_scores = { 0, 0, 0, 0 };
    [SyncVar]
    public int sv_numOfPlayers = 0;
    [SyncVar]
    public int[] sv_currentMultiplier = { 1, 1, 1, 1 };

    private int m_localPlayerNetID = 0;
    private int m_localPlayerID = 0;

    private string m_lastItem = "";
    public int m_maxMultiplier = 5;
    public Text[] m_multiplierText = new Text[4];
    private AudioSource m_audioSource;
    
    void Start ()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public override void OnStartServer()
    {
        m_localPlayerID = sv_numOfPlayers;
        sv_numOfPlayers++;
        Debug.Log(gameObject.name + " is player " + sv_numOfPlayers + " server");
    }

    public override void OnStartClient()
    {
        m_localPlayerID = sv_numOfPlayers;
        sv_numOfPlayers++;
        Debug.Log(gameObject.name + " is player " + sv_numOfPlayers + " client");
    }

    public void SetLocalPlayer(int num)
    {
        m_localPlayerNetID = num;
    }

    void Update ()
    {
        for (int i = 0; i < m_multiplierText.Length; i++)
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
    }

    public void AddPoints(int playerNetID, int points, string itemName)
    {
        if(playerNetID == m_localPlayerNetID)
        {
            if (itemName == m_lastItem && sv_currentMultiplier[m_localPlayerID] < m_maxMultiplier)
            {
                sv_currentMultiplier[m_localPlayerID]++;
            }
            else
            {
                sv_currentMultiplier[m_localPlayerID] = 1;
            }

            m_lastItem = itemName;
            sv_scores[m_localPlayerID] += points * sv_currentMultiplier[m_localPlayerID];
        }

        m_audioSource.Play();
    }
}
