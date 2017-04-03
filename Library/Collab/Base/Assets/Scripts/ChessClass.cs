using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;

public class ChessClass : NetworkBehaviour {
	public UnityEvent OnCollision;
	//##############################

	public bool Attacking;

	public GameObject hitObject;
	[SyncVar]
	public string squareID;
	[SyncVar]
	public GameObject SquareOccupancy = null;
	public string abbr;
	public string colorStr;
	public Material bluemat;
	public Material redmat;
	public Material activeBlueMat;
	public Material activeRedMat;

	private Transform child;




	// Use this for initialization
	void Start () {

		//set default state to select piece first.

		// Is this piece the target being moved?
		Attacking = false;

		// Set the color to the object
		child = this.gameObject.transform.GetChild (0);
		if (child.childCount > 0) {
			for (int i = 0; i < child.childCount; i++) {//creates red and blue pieces
				if (colorStr == "Blue") {
					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = bluemat;	
				} else {
					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = redmat;
				}

			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other) {//determines which piece in the collision attacks/gets destroyed
		if ((other.gameObject.GetComponent ("ChessClass") as ChessClass) != null) {
			if (Attacking) {
				hitObject = other.gameObject;
			} else {
				hitObject = null;
			}
		}
	}

	public string SquareID{
		get{
			return squareID;
		}
		set{
			squareID = value;
		}
	}

	public void MakeActive() {
		// Set the color to the object
//		child = this.gameObject.transform.GetChild (0);
//		if (child.childCount > 0) {
//			for (int i = 0; i < child.childCount; i++) {//creates red and blue pieces
//				if (colorStr == "Blue") {
//					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = activeBlueMat;	
//				} else {
//					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = activeRedMat;
//				}
//
//			}
//		}

		this.transform.localScale = new Vector3 (4.0f, 4.0f, 4.0f);
	}

	public void MakeInActive() {
		// Set the color to the object
//		child = this.gameObject.transform.GetChild (0);
//		if (child.childCount > 0) {
//			for (int i = 0; i < child.childCount; i++) {//creates red and blue pieces
//				if (colorStr == "Blue") {
//					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = bluemat;	
//				} else {
//					child.gameObject.transform.GetChild (i).GetComponent<Renderer>().material = redmat;
//				}
//
//			}
//		}
		this.transform.localScale = new Vector3 (3.0f, 3.0f, 3.0f);
	}
}