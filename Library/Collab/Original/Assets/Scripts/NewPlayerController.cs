using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerController : MonoBehaviour {
	//flag to check if the user has tapped / clicked. 
	//Set to true on click. Reset to false on reaching destination
	private bool flag = false;
	//destination point
	private Vector3 endPoint;
	//alter this to change the speed of the movement of player / gameobject
	public float duration = 6.0f;
	//vertical position of the gameobject
	private float yAxis;

	//control the state of player movement (0=select piece, 1=select square, 2=press spacebar, 3=movement and reset)
	private int state;

	private GameObject chessTarget;
	private GameObject squareTarget;

//	public LayerMask MaskChessPieceLayer;
//	public LayerMask MaskSquareLayer;

	void Start(){
		
		//set default state to select piece first.
		state = 0;
		Debug.Log ("Player 1's Turn");

	}

	// Update is called once per frame
	void Update () {

		//check if the screen is touched / clicked   
		if ((Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) || (Input.GetMouseButtonDown (0))) {
			//declare a variable of RaycastHit struct
			RaycastHit hit;
			//Create a Ray on the tapped / clicked position
			Ray ray = new Ray();
			//for unity editor
			#if UNITY_EDITOR
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			//for touch device
			#elif (UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8)
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			#endif

			// Detect hitting a square.
			if (state == 1) { 
				if (Physics.Raycast (ray, out hit)){
					if (hit.transform.tag == "square") {
						Debug.Log (state);
						//set a flag to indicate to move the gameobject
						flag = true;

						//save the click / tap position
						endPoint = hit.transform.position;
						squareTarget = hit.collider.gameObject;

						//as we do not want to change the y axis value based on touch position, reset it to original y axis value
						endPoint.y = chessTarget.transform.position.y;

						Debug.Log (chessTarget.GetComponent<ChessClass>().squareID + " -> " + hit.transform.GetComponent<Square> ().squareID);
						state = 2;
					}
				}
			}


			// Detect hitting a piece.
			if (state == 0) {
				if (Physics.Raycast (ray, out hit)){
					Debug.Log (state);
					Debug.Log ("hit");
					if (hit.transform.tag != "square") {
						Debug.Log ("clicked on " + hit.transform.tag);
						chessTarget = hit.collider.gameObject;
						state = 1;
					}
				}
			}

		}
		//check if the flag for movement is true and the current gameobject position is not same as the clicked / tapped position
		if (flag) {
			if (state == 3) {	
				//&& !Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)){ //&& !(V3Equal(transform.position, endPoint))){
				//move the gameobject to the desired position
				chessTarget.transform.position = Vector3.Lerp (chessTarget.transform.position, endPoint, 1 / (duration * (Vector3.Distance (chessTarget.transform.position, endPoint))));
			}
		}
		//set the movement indicator flag to false if the endPoint and current gameobject position are equal
		//		else if(flag && Mathf.Approximately(gameObject.transform.position.magnitude, endPoint.magnitude)) {
		if (flag) {
			if (state == 3) {
				if (chessTarget.transform.position == endPoint) {
					flag = false;
					state = 0;
				}
			}
		}

		// Currently set to use the spacebar for confirming move but we could tie this to changing cameras too. #TODO
		if (Input.GetKeyDown ("space")) {
			if (state == 2) {
				state = 3;
				Debug.Log (state);
			}
		}
	}
}