using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FixedCam : MonoBehaviour {

	private Renderer rend;
	public Material interactMAT;
	public Material tableMAT;

	public GameObject tableObject;

	[Header("Newspaper1")]
	public GameObject news1Obj;
	public Material news1ObjMAT;
	private Renderer news1ObjRend;
	[Header("Newspaper2")]
	public GameObject news2Obj;
	public Material news2ObjMAT;
	private Renderer news2ObjRend;
	[Header("Newspaper3")]
	public GameObject news3Obj;
	public Material news3ObjMAT;
	private Renderer news3ObjRend;

	[Header("")]
	public GameObject fixedCamGO;
	private Camera fixedCamera;

	public GameObject player;
	public Player playerScript;

	public bool interacting;

	public Flowchart flowchart;

	// Use this for initialization
	void Start () {


		//Finding Player to enable
		playerScript = player.GetComponent<Player> ();

		//Finding Renderer to highlight object
		rend = tableObject.GetComponent<Renderer>();

		//Finding Camera to disable
		fixedCamera = fixedCamGO.GetComponent<Camera>();

		//Getting Renderers of Newspapers
		news1ObjRend = news1Obj.GetComponent<Renderer>();
		news2ObjRend = news2Obj.GetComponent<Renderer>();
		news3ObjRend = news3Obj.GetComponent<Renderer>();


	}

	// Update is called once per frame
	void Update () {

		if (fixedCamera.enabled) {

			//If Player is interacting with newspapers
			if (playerScript.numNewsObj == 0.0f && playerScript.isInteract && !playerScript.newsChosen) {

				news1ObjRend.material = news1ObjMAT;
				news2ObjRend.material = news2ObjMAT;
				news3ObjRend.material = news3ObjMAT;

			} else if (playerScript.numNewsObj == 1.0f && playerScript.isInteract && !playerScript.newsChosen) {

				news1ObjRend.material = interactMAT; //Moused Over
				news2ObjRend.material = news2ObjMAT;
				news3ObjRend.material = news3ObjMAT;

			} else if (playerScript.numNewsObj == 2.0f && playerScript.isInteract && !playerScript.newsChosen) {

				news1ObjRend.material = news1ObjMAT;
				news2ObjRend.material = interactMAT; //Moused Over
				news3ObjRend.material = news3ObjMAT;

			} else if (playerScript.numNewsObj == 3.0f && playerScript.isInteract && !playerScript.newsChosen) {

				news1ObjRend.material = news1ObjMAT;
				news2ObjRend.material = news2ObjMAT;
				news3ObjRend.material = interactMAT; //Moused Over

			}


		} else if (!fixedCamera.enabled && !playerScript.newsChosen) {

			news1ObjRend.material = news1ObjMAT;
			news2ObjRend.material = news2ObjMAT;
			news3ObjRend.material = news3ObjMAT;

		}
			
	}

	void OnTriggerEnter (Collider other) { //Highlight Object, possibly show text

		if (other.tag == "Player") {

			//Debug.Log ("HI");
			rend.material = interactMAT;
			interacting = true; //Check true for interaction. Cannot check every tick in an OnTrigger Enter, so a boolean must be set.

		}

	}

	void OnTriggerExit (Collider other) { //De-highlight Object, remove text

		if (other.tag == "Player") {

			//Debug.Log ("BYE");
			rend.material = tableMAT;
			interacting = false;

		}

	}

	public void FixCam () {

		Debug.Log ("STOP MOVING PLAYER");
		playerScript.playerActive = false;
		fixedCamera.enabled = true;
		rend.material = tableMAT;
		tableObject.GetComponent<Collider> ().enabled = false;
	}

	public void UnfixCam () {

		playerScript.playerActive = true;
		fixedCamera.enabled = false;
		rend.material = interactMAT;
		tableObject.GetComponent<Collider> ().enabled = true;
	}

	public void News1 () {
		
			playerScript.newsChosen = true;
			Debug.Log ("READING NEWSPAPER 1 HELLO");
			Destroy (news2Obj);
			Destroy (news3Obj);

	}

	public void News2 () {

		playerScript.newsChosen = true;
		Debug.Log ("READING NEWSPAPER 2 HELLO");
		Destroy (news1Obj);
		Destroy (news3Obj);

	}

	public void News3 () {

		playerScript.newsChosen = true;
		Debug.Log ("READING NEWSPAPER 3 HELLO");
		Destroy (news1Obj);
		Destroy (news2Obj);

	}
}
