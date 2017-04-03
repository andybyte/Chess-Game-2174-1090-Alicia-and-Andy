using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MoveHistory : NetworkBehaviour {
	public Camera hillaryCamera;
	public static List<string> moveHistory;
	private Transform originalCamPos;
	public Text moveHistoryText;
	public Text history;
	public Text moveHistoryTextBox;
	public Text PlayerFeed;

	[SyncVar]
	public string playerFeedText;
	[SyncVar]
	public int TurnCount;
	[SyncVar]
	public string SharedHistory;

	// Use this for initialization
	void Start () {
		moveHistoryTextBox = GameObject.Find ("MHText").GetComponent<Text> ();
		moveHistory = new List<string> ();

		PlayerFeed = GameObject.Find ("PlayerFeed").GetComponent<Text> ();

		history = moveHistoryText.GetComponent<Text> ();

		hillaryCamera = GameObject.Find ("HillaryCamera").GetComponent<Camera> ();
		hillaryCamera.enabled = true;

		if (!isServer) {//camera for client (trump)
			hillaryCamera.transform.position = new Vector3 (12, 10, 9);
			Camera.main.transform.rotation = Quaternion.Euler(45,270,0);

		} 

		if(isServer) {//camera for server (hillary)
			hillaryCamera.transform.position = new Vector3(-10,10,9);
			Camera.main.transform.rotation  = Quaternion.Euler(45,90,0);
		}
		
	}

	// Update is called once per frame
	void Update () {
		moveHistoryTextBox.text = SharedHistory;

		// Control player's turn.
		if (TurnCount % 2 == 0) {
			playerFeedText = "Go Player 1!";
		} else {
			playerFeedText = "Go Player 2!";
		}

		PlayerFeed.text = playerFeedText;
			
	}

	public List<string> MoveHistoryList{
		get{
			return moveHistory;
		}
		set{

		}
	}
}
