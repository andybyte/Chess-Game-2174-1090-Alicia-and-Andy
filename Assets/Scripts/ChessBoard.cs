using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChessBoard : NetworkBehaviour {
	public GameObject whiteSquare;
	public GameObject blackSquare;
	private float x, y, z;
	private List<GameObject> boardSquares;
	private const int SCALE_FACTOR = 2;
	private const int BOARD_OFFSET = 8;
	private Square squareObj; 
	private int charIncrement;

	// Use this for initialization
	void Start () {

		if (!NetworkServer.active) {
			return;
		}

		boardSquares = new List<GameObject> ();
		charIncrement = 105;
		x = 0f;
		y = 1f;
		z = 0f;

		int row = 1;
		int column = 1;
		int switchInitColor = 0;

		// Loop to create chessboard with individual squares.
		for(int r=1; r<=8; r++){
			column = 1;

			for(int c=1; c<=8; c++){
				x = (float)(row * SCALE_FACTOR - BOARD_OFFSET); //determines where to place square on x axis
				z = (float)(column * SCALE_FACTOR);//determines where to place square on z axis

				GameObject squareGameObject;
				if ((column % 2) == switchInitColor) {//creates white squares
					squareGameObject = (GameObject)Instantiate (whiteSquare, new Vector3 (x, y, z), transform.rotation);
					NetworkServer.Spawn (squareGameObject);
				} else {//creates black squares
					squareGameObject = (GameObject)Instantiate (blackSquare, new Vector3 (x, y, z), transform.rotation);
					NetworkServer.Spawn (squareGameObject);
				}
					
				boardSquares.Add (squareGameObject);//adds square to the list of Square objects

				Square squareObj = squareGameObject.GetComponent<Square> ();
				squareObj.ColumnID = (char)(charIncrement - column);
				//assigns square a column id (char)
				squareObj.RowID = r;
				//assigns square row id (num)
				squareObj.squareID = squareObj.ColumnID.ToString() + squareObj.RowID.ToString();
				//assigns square the square ID for game use

				column++;

			}

			// Set initial color.
			if(switchInitColor==0){
				switchInitColor = 1;
			}else{
				switchInitColor = 0;
			}

			row++;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
