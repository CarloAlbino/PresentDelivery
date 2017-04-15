using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkGameStart : NetworkBehaviour {

    public GameObject m_startGameCanvas;
    public Text m_ipDisplay;

    private bool m_startGame = false;
    public bool startGame { get { return m_startGame; } }

    private NetworkManager m_netManager;

    void Start()
    {
        m_netManager = FindObjectOfType<NetworkManager>();
    }

	void Update ()
    {
        if (!m_startGameCanvas.activeInHierarchy)
        {
            if (isServer)
            {
                m_startGameCanvas.SetActive(true);
                m_ipDisplay.text = Network.player.ipAddress;
            }
        }
	}

    public void Button_StartGame()
    {
        m_startGame = true;
        m_startGameCanvas.SetActive(false);
    }
}
