using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkCleanUp : MonoBehaviour {

	void Start ()
    {
        // Destroy network manager if it is in the scene.  
        // We don't want to be connected in the menu.
        NetworkManager netManager = FindObjectOfType<NetworkManager>();
        if (netManager != null)
        {
            Destroy(netManager.gameObject);
        }
	}
	
}
