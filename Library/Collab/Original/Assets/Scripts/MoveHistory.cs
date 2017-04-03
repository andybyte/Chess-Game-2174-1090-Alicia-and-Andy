using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MoveHistory : NetworkBehaviour {

	public int turn;
	public Camera hillaryCamera;
	public List<string> moveHistory;
	public Button undoButton;
	private Transform originalCamPos;
	public Text moveHistoryText;
	public Text history;

	// Use this for initialization
	void Start () {
		moveHistory = new List<string> ();
		turn = 1;

		history = moveHistoryText.GetComponent<Text> ();

		Button undo = undoButton.GetComponent<Button> ();
		undo.onClick.AddListener (UndoOnClick);

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

		if (Input.GetKeyDown ("space")) {//switch to space
			//StartCoroutine(TurnRotation());
			//coroutine isn't needed unless switching camera views again
//			history.text += moveHistory [turn-1] + "\n";
		}
			
	}
		

	IEnumerator TurnRotation(){
		yield return new WaitForSeconds(4);
		turn += 1;
	}

	void UndoOnClick(){
		Debug.Log ("UNDO");
		if (turn > 0) {
			turn -= 1;
			moveHistory.RemoveAt(turn-1);
			history.text = "";

			for (int i = 0; i < moveHistory.Count; i++) {
				history.text += moveHistory [turn-1] + "\n";
			}
		}
		//needs put piece back based on last move
		//needs to remove/cross out last move made in moveHistory
	}

	public List<string> MoveHistoryList{
		get{
			return moveHistory;
		}
		set{

		}
	}

	public void AddToList(string moveInfo){//adds the move info to the moveHistory list for text access
		string move = "No. " + turn + " " + moveInfo;
		moveHistory.Add(move);
	}
}
