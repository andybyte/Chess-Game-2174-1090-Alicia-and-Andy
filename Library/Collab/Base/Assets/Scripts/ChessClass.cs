using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ChessClass : NetworkBehaviour {
	//Controls#####################
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 6.0f;
	//control the state of player movement (0=select piece, 1=select square, 2=press spacebar, 3=movement and reset)
	private int state; //0 = not moving, 1 = move
	//##############################

	public ChessClass chessClass;


	public string squareID;
	public string abbr;
	public string colorStr;
	public Material bluemat;
	public Material redmat;

	private Transform child;


	// Use this for initialization
	void Start () {

		//set default state to select piece first.
		state = 0;


		// Set the color to the object
		child = this.gameObject.transform.GetChild (0);
		if (child.childCount > 0) {
			for (int i = 0; i < child.childCount; i++) {
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
		if (state == 1) {
			this.transform.position = Vector3.Lerp (this.transform.position, endPoint, 1 / (duration * (Vector3.Distance (this.transform.position, endPoint))));
		}

		if (state == 1) {
			if (this.transform.position == endPoint) {

				//## CHECK TO SEE IF ANOTHER PIECE OF THE OPPOSITE COLOR IS COLLIDING, IF SO, DESTROY IT ##



				state = 0;
			}
		}
	}

	public void Chirp() {
		Debug.Log ("<OO");
	}

	public void MovePiece(Vector3 ep) {
		endPoint = ep;
		state = 1;
	}

	void OnCollisionEnter(Collision collision) {
		GameObject hit = collision.gameObject;
		Debug.Log (hit);
//		var health = hit.GetComponent<Health> ();
//		if (health != null) {
//			health.TakeDamage (10);
//		}
//		Destroy (gameObject);
	}
}
	
	//void OnTriggerEnter (Collider piece){
		//if((piece.gameObject.GetComponent<ChessClass>().colorStr != colorStr) && (piece.gameObject.GetComponent<ChessClass>().squareID == squareID)){
			//Destroy (piece.gameObject);
		//}
	//}
//}
