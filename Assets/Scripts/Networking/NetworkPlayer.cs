using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkPlayer : NetworkBehaviour {

    public Material m_localPlayerColor;

	void Start ()
    {
		
	}

    void Update ()
    {
		
	}

    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material = m_localPlayerColor;
        GetComponent<Walk>().isLocal = true;
        GetComponentInChildren<Text>().text = "You\nv";
    }

    public uint GetPlayerNetID()
    {
        return netId.Value;
    }
}
