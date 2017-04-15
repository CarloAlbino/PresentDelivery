using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkLobbyControls : MonoBehaviour
{

    /////////////////////////////////////////////////
    // NOTE: NOT USED
    //       Tried something, didn't work
    //
    //          - Carlo (April 14, 2017)
    /////////////////////////////////////////////////


    private NetworkManager m_netManager;
    public Text m_ipAddressDisplay;
    private string m_serverIP;
    public GameObject m_networkConnectionUI;
    public GameObject m_serverConnectionUI;
    public GameObject m_clientConnectionUI;

    public InputField m_ipInput;
	void Start ()
    {
        m_netManager = GetComponent<NetworkManager>();
        m_serverConnectionUI.SetActive(false);
        m_clientConnectionUI.SetActive(false);
	}

    public void Lobby_StartServer()
    {
        if (m_netManager.StartServer())
        {
            m_networkConnectionUI.SetActive(false);
            m_serverConnectionUI.SetActive(true);
            m_serverIP = Network.player.ipAddress;
            m_ipAddressDisplay.text = "Connect to: " + m_serverIP;
        }
        else
        {
            Debug.LogWarning("Server not connected");
        }
    }

    public void Lobby_StartClient()
    {
        m_networkConnectionUI.SetActive(false);
        m_clientConnectionUI.SetActive(true);
    }

    public void Lobby_ConnectToServer()
    {
        m_serverIP = m_ipInput.text;
        m_netManager.networkAddress = m_serverIP;
        if(m_netManager.StartClient() == null)
        {
            Debug.LogWarning("Client not connected");
        }

    }

    public void Lobby_Disconnect()
    {
        m_netManager.StopClient();

        if (NetworkServer.active)
        {
            m_netManager.StopServer();
        }
    }
	
	void Update ()
    {
		
	}
}
