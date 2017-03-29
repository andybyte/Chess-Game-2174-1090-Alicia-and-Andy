﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour {
	private const int SCALE_FACTOR = 2;
	public GameObject hillaryBishop;
	public GameObject hillaryKing;
	public GameObject hillaryKnight;
	public GameObject hillaryPawn;
	public GameObject hillaryQueen;
	public GameObject hillaryRook;
	public GameObject trumpBishop;
	public GameObject trumpKing;
	public GameObject trumpKnight;
	public GameObject trumpPawn;
	public GameObject trumpQueen;
	public GameObject trumpRook;
	private int column;
	private int row;
	private float y,x3,x4, z3,z4;

	// Use this for initialization
	void Start () {
		row = 1;

		for (int r = 1; r <= 8; r++) {
			column = 1;
		
			for (int c = 1; c <= 8; c++) {
//				x = (float)(row * SCALE_FACTOR - 6.2);
//				x2 = (float) (x - 3.5);
				x3 = (float)(row * SCALE_FACTOR - 8.2f);
				x4 = (float)(row * SCALE_FACTOR - 7.8f);

//				z = (float)(column * SCALE_FACTOR - 1.8);
//				z2 = (float) (z + 3.5);
				z3 = (column * SCALE_FACTOR);
				z4 = (column * SCALE_FACTOR);

				y = 1.1f;

				if (row == 1) {
					switch (column) {
					case 8:
						GameObject hillaryRook2GameObject = (GameObject)Instantiate (hillaryRook, new Vector3 (x3, y, z3), hillaryRook.transform.rotation);
						hillaryRook2GameObject.GetComponent<ChessClass> ().squareID = "A1";
						break;
					case 7:
						GameObject hillaryKnight2GameObject = (GameObject)Instantiate (hillaryKnight, new Vector3 (x3, y, z3), hillaryKnight.transform.rotation);
						hillaryKnight2GameObject.GetComponent<ChessClass> ().squareID = "B1";
						break;
					case 6:
						GameObject hillaryBishop2GameObject = (GameObject)Instantiate (hillaryBishop, new Vector3 (x3, y, z3), hillaryBishop.transform.rotation);
						hillaryBishop2GameObject.GetComponent<ChessClass> ().squareID = "C1";
						break;
					case 5:
						GameObject hillaryQueenGameObject = (GameObject)Instantiate (hillaryQueen, new Vector3 (x3, y, z3), hillaryQueen.transform.rotation);
						hillaryQueenGameObject.GetComponent<ChessClass> ().squareID = "D1";
						break;
					case 4:
						GameObject hillaryKingGameObject = (GameObject)Instantiate (hillaryKing, new Vector3 (x3, y, z3), hillaryKing.transform.rotation);
						hillaryKingGameObject.GetComponent<ChessClass> ().squareID = "E1";
						break;
					case 3:
						GameObject hillaryBishopGameObject = (GameObject)Instantiate (hillaryBishop, new Vector3 (x3, y, z3), hillaryBishop.transform.rotation);
						hillaryBishopGameObject.GetComponent<ChessClass> ().squareID = "F1";
						break;
					case 2: 
						GameObject hillaryKnightGameObject = (GameObject)Instantiate (hillaryKnight, new Vector3 (x3, y, z3), hillaryKnight.transform.rotation);
						hillaryKnightGameObject.GetComponent<ChessClass> ().squareID = "G1";
						break;
					case 1:
						GameObject hillaryRookGameObject = (GameObject)Instantiate (hillaryRook, new Vector3 (x3, y, z3), hillaryRook.transform.rotation);
						hillaryRookGameObject.GetComponent<ChessClass> ().squareID = "H1";
						break;
					}
				} else if (row == 2) {
					char pawnColumn = (char)(105 - column);
					string pawnID = "" + pawnColumn + 2;
					GameObject hillaryPawnGameObject = (GameObject)Instantiate (hillaryPawn, new Vector3 (x3, y, z3), hillaryPawn.transform.rotation);
					hillaryPawnGameObject.GetComponent<ChessClass> ().squareID = pawnID.ToUpper ();;

				} else if (row == 7) {
					char pawnColumn = (char)(105 - column);
					string pawnID = "" + pawnColumn + 7;
					GameObject trumpPawnGameObject = (GameObject)Instantiate (trumpPawn, new Vector3 ((float)(x4 - 0.2), y, (float)(z4)), trumpPawn.transform.rotation);
					trumpPawnGameObject.GetComponent<ChessClass> ().squareID = pawnID.ToUpper();
				} else if (row == 8) {
					switch (column) {
					case 8:
						GameObject trumpRook2GameObject = (GameObject)Instantiate (trumpRook, new Vector3 (x4, y, z4), trumpRook.transform.rotation);
			
						trumpRook2GameObject.GetComponent<ChessClass> ().squareID = "A8";
						break;
					case 7:
						GameObject trumpKnight2GameObject = (GameObject)Instantiate (trumpKnight, new Vector3 (x4, y, z4), trumpKnight.transform.rotation);
						trumpKnight2GameObject.GetComponent<ChessClass> ().squareID = "B8";
						break;
					case 6:
						GameObject trumpBishop2GameObject = (GameObject)Instantiate (trumpBishop, new Vector3 (x4, y, z4), trumpBishop.transform.rotation);
						trumpBishop2GameObject.GetComponent<ChessClass> ().squareID = "C8";
						break;
					case 5:
						GameObject trumpQueenGameObject = (GameObject)Instantiate (trumpQueen, new Vector3 (x4, y, z4), trumpQueen.transform.rotation);
						trumpQueenGameObject.GetComponent<ChessClass> ().squareID = "D8";
						break;
					case 4:
						GameObject trumpKingGameObject = (GameObject)Instantiate (trumpKing, new Vector3 (x4, y, z4), trumpKing.transform.rotation);
						trumpKingGameObject.GetComponent<ChessClass> ().squareID = "E8";
						break;
					case 3:
						GameObject trumpBishopGameObject = (GameObject)Instantiate (trumpBishop, new Vector3 (x4, y, z4), trumpBishop.transform.rotation);
						trumpBishopGameObject.GetComponent<ChessClass> ().squareID = "F8";
						break;
					case 2: 
						GameObject trumpKnightGameObject = (GameObject)Instantiate (trumpKnight, new Vector3 (x4, y, z4), trumpKnight.transform.rotation);
						trumpKnightGameObject.GetComponent<ChessClass> ().squareID = "G8";
						break;
					case 1:
						GameObject trumpRookGameObject = (GameObject)Instantiate (trumpRook, new Vector3 (x4, y, z4), trumpRook.transform.rotation);
						trumpRookGameObject.GetComponent<ChessClass> ().squareID = "H8";
						break;
					}
				}
				column++;
			}
			row++;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Test mouse click

	void OnMouseDown() {
		Debug.Log(gameObject.ToString());
	}
}