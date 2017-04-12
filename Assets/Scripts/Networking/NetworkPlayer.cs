using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayer : NetworkBehaviour {

    public Color m_localPlayerColor;

	void Start ()
    {
		
	}

    void Update ()
    {
		
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = m_localPlayerColor;
    }
}
