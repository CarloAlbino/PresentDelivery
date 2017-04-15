using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NetworkGameStart : NetworkBehaviour {

    // Button to start the game is on this canvas
    public GameObject m_startGameCanvas;

    // Is the game started?
    private bool m_startGame = false;
    public bool startGame { get { return m_startGame; } }

    // Reference to the nework manager
    private NetworkManager m_netManager;

    void Start()
    {
        // Grab the reference
        m_netManager = FindObjectOfType<NetworkManager>();
    }

	void Update ()
    {
        if (!m_startGame)
        {
            if (!m_startGameCanvas.activeInHierarchy)
            {
                if (isServer)
                {
                    // Show start button when server is started
                    // NOTE: currently the server must start the game manually
                    //       and must decide when all the players are connected/ready
                    m_startGameCanvas.SetActive(true);
                }
            }
        }

        // Disconnect from the game by pressing q
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isServer)
            {
                NetworkServer.DisconnectAll();
                m_netManager.StopServer();
            }
            else if(isClient)
            { 
                Network.Disconnect();
            }
            SceneManager.LoadScene(0);
        }
	}

    // Start the game (called via a canvas button)
    // This hides the start button and starts the game
    public void Button_StartGame()
    {
        m_startGame = true;
        m_startGameCanvas.SetActive(false);
    }
}
