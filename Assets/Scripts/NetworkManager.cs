using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer) {
			Debug.Log ("Running as a server");
		} else {
			if (Network.isClient) {
				Debug.Log ("Running as a client");
			}
		}
	}
		
	public void OnConnectedToServer(){
		Debug.Log ("Connected");
	}
}
