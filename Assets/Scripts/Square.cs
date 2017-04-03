using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Square : NetworkBehaviour {
	private string color;
	private int rowID;
	private char columnID;
	[SyncVar]
	public string squareID;
	[SyncVar]
	public GameObject occupant = null;

	public Material active;
	public Material inactive;


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

	public void MakeActive() {
		// Make active
		this.GetComponent<Renderer>().material = active;
	}

	public void MakeInActive() {
		// Make inactive
		this.GetComponent<Renderer>().material = inactive;
	}
}
