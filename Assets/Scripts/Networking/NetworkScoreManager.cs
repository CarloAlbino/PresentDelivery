using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkScoreManager : NetworkBehaviour {

    // Saves the score of the, up to 4, players
    [SyncVar]
    public SyncListInt sv_scores = new SyncListInt();
    // Stores the number of players there are with the purpose to synching up colours and score labels
    [SyncVar]
    public int sv_numOfPlayers = 0;
    // Stores the current score multiplier for each connected player
    [SyncVar]
    public SyncListInt sv_currentMultiplier = new SyncListInt();
    // Stores the Unique Network IDs for each connected player in order of their connection
    [SyncVar]
    public SyncListUInt sv_playerNetIDs = new SyncListUInt();

    // Used for determining score multipliers
    private string m_lastItem = "";
    public int m_maxMultiplier = 5;
    // Displays current score multiplier
    public Text[] m_multiplierText = new Text[4];

    // Reference to the audio source
    private AudioSource m_audioSource;
    
    void Start ()
    {
        // Grab the audio source
        m_audioSource = GetComponent<AudioSource>();
    }

    void Update ()
    {
        // Display the score multipliers
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

        //Debug.Log("Num of players connected " + NetworkServer.connections.Count);
        //foreach (NetworkConnection n in NetworkServer.connections)
        //{
        //    Debug.Log(n.connectionId);
        //}
    }

    /// <summary>
    /// Adds points to the player's score after multiplying it if needed
    /// </summary>
    /// <param name="localPlayerID">Whether it's player 1, player 2, etc</param>
    /// <param name="points">How many points</param>
    /// <param name="itemName">The item's name</param>
    public void AddPoints(int localPlayerID, int points, string itemName)
    {
        //Debug.Log("Adding points for " + localPlayerID);

        // Local player ID is used to check that player's current multiplier
        // All scoring is done on server side
        if (itemName == m_lastItem && sv_currentMultiplier[localPlayerID] < m_maxMultiplier)
        {
            sv_currentMultiplier[localPlayerID]++;
        }
        else
        {
            sv_currentMultiplier[localPlayerID] = 1;
        }

        m_lastItem = itemName;

        // Add points to the passed in player's score
        sv_scores[localPlayerID] += points * sv_currentMultiplier[localPlayerID];

        m_audioSource.Play();
    }
}
