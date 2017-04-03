using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
	private static GameObject chessTarget;
	private static GameObject squareTarget;
	private static int state; // 0 = choose piece, 1 = choose square, 2 = send command.
	private static Vector3 endPoint;
	private static string newSquareID;
	private GameObject previousSquare;
	public float duration = 6.0f;
	private AudioSource slide;
	private AudioSource losePiece;
	private AudioSource stop;

	public int TurnCount = 0;

	private string squareToSquareMove;

	public int PlayerNum;

	public MoveHistory moveHistoryClass;
	public GameOver gameover;

	// Use this for initialization
	void Start () {

		if(isServer){
			PlayerNum = 1;
		}

		if(!isServer){
			PlayerNum = 2;
		}

		state = 0;
		gameover = GameObject.Find ("TheGame").GetComponent<GameOver> ();
		moveHistoryClass = GameObject.Find ("TheGame").GetComponent<MoveHistory> ();
		slide = GameObject.Find("Slide").GetComponent<AudioSource> ();
		losePiece = GameObject.Find("Lose Piece").GetComponent<AudioSource> ();
		stop = GameObject.Find("Stop").GetComponent<AudioSource> ();

	}

	// Update is called once per frame
	void Update () {
		
		if (!isLocalPlayer) {
			return;
		}
			
		// Control player's turn.
		if (PlayerNum == 1 && moveHistoryClass.TurnCount % 2 != 0) {
			return;
		} else if (PlayerNum == 2 && moveHistoryClass.TurnCount % 2 == 0) {
			return;
		}

		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray = new Ray();

			ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			// Detect hitting a square.
			if (state == 1) { // was 1
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag == "square") {
						//creates the target square as its new squareID
						squareTarget = hit.collider.gameObject;
						newSquareID = hit.transform.GetComponent<Square> ().squareID.ToUpper ();
						Debug.Log ("Selected: " + newSquareID);

						//Check to see if a friendly chesspiece is already occupying that square.
						if (squareTarget.GetComponent<Square>().occupant) {
							if (PlayerNum == 1 && squareTarget.GetComponent<Square>().occupant.GetComponent<ChessClass>().colorStr == "Blue") {
								//There is already a blue piece and you are trying to move a blue piece here.
								return;
							} else if (PlayerNum == 2 && squareTarget.GetComponent<Square>().occupant.GetComponent<ChessClass>().colorStr == "Red"){
								//There is already a red piece and you are trying to move a red piece here.
								return;
							}
						}

						squareTarget.GetComponent<Square> ().MakeActive ();

						//save the click / tap position
						endPoint = hit.transform.position;
						//as we do not want to change the y axis value based on touch position, reset it to original y axis value
						endPoint.y = chessTarget.transform.position.y;

						state = 2; // was 2
					}
				}
			}


			// Detect hitting a piece.
			if (state == 0) {
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag != "square") {
						if (PlayerNum == 1 && hit.transform.GetComponent<ChessClass>().colorStr == "Red") {
							return;
						} else if (PlayerNum == 2 && hit.transform.GetComponent<ChessClass>().colorStr == "Blue"){
							return;
						}
						Debug.Log ("Selected: " + hit.transform.tag);
						chessTarget = hit.collider.gameObject;
						chessTarget.GetComponent<ChessClass> ().MakeActive ();
						state = 1;
					}
				}
			}
		}

		// Move a piece (chessTarget) to a target square (endPoint)
		if (state == 3) {
			if (chessTarget) {
				if (chessTarget.transform.position != endPoint) {
					//slide.Play ();
					CmdMovePiece (chessTarget, endPoint);

				} else {
					state = 0;
					TurnCount = moveHistoryClass.TurnCount;
					TurnCount++;
					string history = moveHistoryClass.SharedHistory;

					//adds the information about the color, piece, start-end squares to the AddToList function to add to the history list
					history += "No. " + TurnCount + " " + chessTarget.GetComponent<ChessClass> ().colorStr + " " + chessTarget.GetComponent<ChessClass> ().abbr + ": " + chessTarget.GetComponent<ChessClass> ().squareID + " -> " + newSquareID + "\n";
					chessTarget.GetComponent<ChessClass> ().SquareID = newSquareID;
					chessTarget.GetComponent<ChessClass> ().MakeInActive ();
					squareTarget.GetComponent<Square> ().MakeInActive ();
					// Set square as occupied by this piece.
					CmdExchangeSquareOccupant(squareTarget, chessTarget);
					CmdUpdateTurnAndHistory (TurnCount, history);
					CmdCheckAttack (chessTarget);
				}
			}
		}

		// Currently set to use the spacebar for confirming move but we could tie this to changing cameras too. #TODO
		if (Input.GetKeyDown ("space")) {
			
			// Are we ready to send? 2 IF YES.
			if (state == 2) {
				state = 3;
				slide.Play();
			}
		}

		if (Input.GetKeyDown ("backspace")) {//allows player to reselect piece and target square
			Debug.Log ("Reset Turn");
			if (chessTarget) {
				chessTarget.GetComponent<ChessClass> ().MakeInActive ();	
			}
			if (squareTarget) {
				squareTarget.GetComponent<Square> ().MakeInActive ();
			}
			state = 0;
		}
	}
	
	// Move Piece once per frame until it reaches its endpoint.
	[Command]
	void CmdMovePiece(GameObject chessTarget, Vector3 endPoint) {//moves the piece to target square
		if (chessTarget) {
			chessTarget.GetComponent<ChessClass> ().Attacking = true;
			chessTarget.transform.position = Vector3.Lerp (chessTarget.transform.position, endPoint, 1 / (duration * (Vector3.Distance (chessTarget.transform.position, endPoint))));
		}
	}

	// Check if the piece is attacking and if so, destroy opponent.
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
					losePiece.Play ();
					Destroy (chessTarget.GetComponent<ChessClass>().hitObject);
					//attacking piece destroys the stationary piece
				}	
			}
		}
		chessTarget.GetComponent<ChessClass>().Attacking = false;
		state = 0;
	}

	// Update turn and history on the server.
	[Command]
	void CmdUpdateTurnAndHistory(int turnNum, string moveInfo){
		moveHistoryClass.TurnCount = turnNum;
		moveHistoryClass.SharedHistory = moveInfo;
	}

	// Move a piece off a square and onto the next to check for movement blocking.
	[Command]
	void CmdExchangeSquareOccupant(GameObject newSquare, GameObject occupant){
		if (chessTarget.GetComponent<ChessClass>().SquareOccupancy) {
			previousSquare = chessTarget.GetComponent<ChessClass>().SquareOccupancy;
			previousSquare.GetComponent<Square> ().occupant = null;
		}
		newSquare.GetComponent<Square> ().occupant = occupant;
		occupant.GetComponent<ChessClass> ().SquareOccupancy = newSquare;
	}
}
