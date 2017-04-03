using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class GameOver : NetworkBehaviour {
	

	public GameObject gameOverUI;

	// Use this for initialization
	void Start () {
		gameOverUI.GetComponent<GameObject>();
		gameOverUI.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	}

	[ClientRpc]
	public void RpcRestart(){ //restarts the scene for clients connected to the server
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
		
	public void Quit(){ //closes individual's application
		Debug.Log ("APPLICATION QUIT!");
		Application.Quit ();
	}
		

	[ClientRpc]
	public void RpcEndGame(){ //sets gameOverUI to appear for clients
		gameOverUI.SetActive(true);
	}
}
