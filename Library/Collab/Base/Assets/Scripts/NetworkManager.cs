using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkManager : NetworkBehaviour {

	public Text bug;
	// Use this for initialization
	void Start () {
		bug = GameObject.Find ("bug").GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isServer) {
			Debug.Log ("Running as a server");
		} else {
			if (Network.isClient) {
				Debug.Log (bug.text = "Running as a client");
			}
		}
	}
		
	public void OnConnectedToServer(){
		Debug.Log ("Connected");
	}
}
