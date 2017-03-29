using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard : MonoBehaviour {
	public GameObject whiteSquare;
	public GameObject blackSquare;
	private float x, y, z;
	private List<GameObject> boardSquares;
	private const int SCALE_FACTOR = 2;
	private const int BOARD_OFFSET = 8;
	private Square squareObj; //if get rid of broken if statement, delete this
	private int charIncrement;

	// Use this for initialization
	void Start () {
		boardSquares = new List<GameObject> ();
		charIncrement = 105;
		x = 0f;
		y = 1f;
		z = 0f;

		int row = 1;
		int column = 1;
		int switchInitColor = 0;

		for(int r=1; r<=8; r++){
			column = 1;

			for(int c=1; c<=8; c++){
				x = (float)(row * SCALE_FACTOR - BOARD_OFFSET);
				z = (float)(column * SCALE_FACTOR);

				GameObject squareGameObject;
				if ((column % 2) == switchInitColor) {
					squareGameObject = (GameObject)Instantiate (whiteSquare, new Vector3 (x, y, z), transform.rotation);
				} else {
					squareGameObject = (GameObject)Instantiate (blackSquare, new Vector3 (x, y, z), transform.rotation);
				}
					
				boardSquares.Add (squareGameObject);

				Square squareObj = squareGameObject.GetComponent<Square> ();
				squareObj.ColumnID = (char)(charIncrement - column);
				squareObj.RowID = r;
				squareObj.squareID = squareObj.ColumnID.ToString() + squareObj.RowID.ToString();

				// log IDs to see how they look: //Debug.Log(squareObj.RowID + "," +squareObj.ColumnID);

				column++;



			}
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
		if (Input.GetMouseButton (0)) {//this if statement doesnet work T.T
//			Debug.Log (squareObj.ColumnID + "" + squareObj.RowID);
//			Debug.Log ("pressed");
		}
	}


}
