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
	public float duration = 6.0f;
	public int PlayerTurn;
	public int PlayerNum;

	public MoveHistory moveHistoryClass;
	public GameOver gameover;
	public Text bug;

	// Use this for initialization
	void Start () {
		PlayerTurn = 1;

		// Helper text for debugging.
		bug = GameObject.Find ("bug").GetComponent<Text> ();

		if(isServer){
			PlayerNum = 1;
		}

		if(!isServer){
			PlayerNum = 2;
		}

		state = 0;
		gameover = GameObject.Find ("TheGame").GetComponent<GameOver> ();
		moveHistoryClass = GameObject.Find ("TheGame").GetComponent<MoveHistory> ();
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}


		bug.text = PlayerTurn.ToString();


//		if (PlayerNum != PlayerTurn) {
//			return;
//		}

		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray = new Ray();
			//for unity editor
//			#if UNITY_EDITOR
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			//for touch device
//			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
//			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//			#endif

			// Detect hitting a square.
			if (state == 1) { // was 1
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag == "square") {
						//save the click / tap position
						endPoint = hit.transform.position;
						squareTarget = hit.collider.gameObject;
						//as we do not want to change the y axis value based on touch position, reset it to original y axis value
						endPoint.y = chessTarget.transform.position.y;


						newSquareID = hit.transform.GetComponent<Square> ().squareID.ToUpper ();
						//creates the target square as its new squareID
						Debug.Log ("Selected: " + newSquareID);

						string squareToSquareMove = chessTarget.GetComponent<ChessClass> ().colorStr + " " + chessTarget.GetComponent<ChessClass> ().abbr + ": " + chessTarget.GetComponent<ChessClass> ().squareID + " -> " + newSquareID;

						//@Temporarily disabling movehistory add while I fix movement.
						moveHistoryClass.AddToList(squareToSquareMove);
						//adds the information about the color, piece, start-end squares to the AddToList function to add to the history list

						hit.transform.GetComponent<Square> ().squareID.ToUpper ();
//						CmdTest (squareTarget);
						state = 2; // was 2
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

		// Move a piece (chessTarget) to a target square (endPoint)
		if (state == 3) {
			if (chessTarget) {
				if (chessTarget.transform.position != endPoint) {
					CmdMovePiece (chessTarget, endPoint);
				} else {
					state = 0;
					PlayerTurn++;
					CmdCheckAttack (chessTarget);
				}
			}
		}

		// Currently set to use the spacebar for confirming move but we could tie this to changing cameras too. #TODO
		if (Input.GetKeyDown ("space")) {
			// Are we ready to send? 2 IF YES.
			if (state == 2) {
				state = 3;
				chessTarget.GetComponent<ChessClass> ().squareID = newSquareID;
				//changes the squareID to newSquareID once piece moves
			}
		}

		if (Input.GetKeyDown ("backspace")) {//allows player to reselect piece and target square
			Debug.Log ("Reset Turn");
			state = 0;
		}
	}

	[Command]
	void CmdMovePiece(GameObject chessTarget, Vector3 endPoint) {//moves the piece to target square
//		if (!isLocalPlayer) {
//			return;
//		}
		if (chessTarget) {
			chessTarget.GetComponent<ChessClass> ().Attacking = true;
			chessTarget.transform.position = Vector3.Lerp (chessTarget.transform.position, endPoint, 1 / (duration * (Vector3.Distance (chessTarget.transform.position, endPoint))));
		}
	}

	[Command]
	void CmdCheckAttack(GameObject chessTarget){
		if (chessTarget.GetComponent<ChessClass>().Attacking) {
			if (chessTarget.GetComponent<ChessClass>().hitObject) {
				if (chessTarget.GetComponent<ChessClass>().hitObject.GetComponent<ChessClass>().colorStr != chessTarget.GetComponent<ChessClass>().colorStr) {
					Debug.Log ("Destroy: " + chessTarget.GetComponent<ChessClass>().hitObject.name);
			
				if((chessTarget.GetComponent<ChessClass>().hitObject.tag == "HillaryKing") || (chessTarget.GetComponent<ChessClass>().hitObject.tag == "TrumpKing")) {
					gameover.RpcEndGame();
					Debug.Log ("gameover");
					//gameover call to set UI as active
				}

					Destroy (chessTarget.GetComponent<ChessClass>().hitObject);
					//attacking piece destroys the stationary piece
				}	
			}
		}
		chessTarget.GetComponent<ChessClass>().Attacking = false;
		state = 0;
	}

}
