using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {
	private string color;
	private int rowID;
	private char columnID;
	public string squareID;
	//GamePiece currentGamePiece;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public char ColumnID{
		get{
			return columnID;
		}
		set{
			columnID = value;
		}
	}

	public int RowID{
		get{
			return rowID;
		}
		set{
			rowID = value;
		}
	}

	// Test mouse click

	void OnMouseDown() {
//		Debug.Log((columnID + "" + rowID).ToUpper());
//		Debug.Log (gameObject.transform.position);
	}

	public void Chirp() {
		Debug.Log ("<OO");
	}
}
