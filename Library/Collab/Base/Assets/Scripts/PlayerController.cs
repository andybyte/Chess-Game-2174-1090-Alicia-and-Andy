using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
	private GameObject chessTarget;
	private GameObject squareTarget;
	private int state; // 0 = choose piece, 1 = choose square, 2 = send command.
	private Vector3 endPoint;
	private string newSquareID;

	public MoveHistory moveHistoryClass;

	public Text bug;

	// Use this for initialization
	void Start () {
		state = 0;
		moveHistoryClass = GameObject.Find ("TheGame").GetComponent<MoveHistory> ();

		bug = GameObject.Find ("bug").GetComponent<Text> ();

		bug.text = "hello, human";

	}

	// Update is called once per frame
	void Update () {
		//Player Check
		if (!isLocalPlayer) {
			return;
		}

		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray = new Ray();
			//for unity editor
//			#if UNITY_EDITOR
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)){
				bug.text = hit.transform.position.ToString();
			}

			//for touch device
//			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
//			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//			#endif

			// Detect hitting a square.
			if (state == 1) { 
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag == "square") {
						//save the click / tap position
						endPoint = hit.transform.position;
						squareTarget = hit.collider.gameObject;
						//as we do not want to change the y axis value based on touch position, reset it to original y axis value
						endPoint.y = chessTarget.transform.position.y;


						newSquareID = hit.transform.GetComponent<Square> ().squareID.ToUpper ();
						Debug.Log ("Selected: " + newSquareID);
						string squareToSquareMove = chessTarget.GetComponent<ChessClass> ().colorStr + " " + chessTarget.GetComponent<ChessClass> ().abbr + ": " + chessTarget.GetComponent<ChessClass> ().squareID + " -> " + newSquareID;
						moveHistoryClass.AddToList(squareToSquareMove);

						hit.transform.GetComponent<Square> ().squareID.ToUpper ();

						state = 2;
					}
				}
			}


			// Detect hitting a piece.
			if (state == 0) {
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag != "square") {
						Debug.Log ("Selected: " + hit.transform.tag);
						chessTarget = hit.collider.gameObject;
						state = 1;
					}
				}
			}

		}

		// Currently set to use the spacebar for confirming move but we could tie this to changing cameras too. #TODO
		if (Input.GetKeyDown ("space")) {
			// Are we ready to send? 2 IF YES.

			if (!isLocalPlayer) {
				return;
			}
			if (state == 2) {
				chessTarget.GetComponent<ChessClass>().MovePiece(endPoint);
				chessTarget.GetComponent<ChessClass> ().squareID = newSquareID;
				state = 0;
			}
		}

		if (Input.GetKeyDown ("backspace")) {
			Debug.Log ("Reset Turn");
			state = 0;
		}
	}

	public override void OnStartLocalPlayer(){
		Debug.Log ("Local player started");
	}
}
