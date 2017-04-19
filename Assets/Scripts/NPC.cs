using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class NPC : MonoBehaviour {

	private Renderer rend;
	public Material interactMAT;
	public Material defaultMAT;

	public GameObject npcGO;
	public GameObject player;
	public Player playerScript;

	public bool interacting;
	public bool done;

	public Flowchart flowchart;

	// Use this for initialization
	void Start () {

		//Finding Player to enable
		playerScript = player.GetComponent<Player> ();

		//Finding Renderer to highlight object
		rend = npcGO.GetComponent<Renderer>();

	}
	
	// Update is called once per frame
	void Update () {

		if (done) {

			rend.material = defaultMAT;

		}

	}

	void OnTriggerEnter (Collider other) { //Highlight Object, possibly show text

		if (other.tag == "Player" && !done) {

			//Debug.Log ("HI");
			rend.material = interactMAT;
			interacting = true; //Check true for interaction. Cannot check every tick in an OnTrigger Enter, so a boolean must be set.

		}

	}

	void OnTriggerExit (Collider other) { //De-highlight Object, remove text

		if (other.tag == "Player") {

			//Debug.Log ("BYE");
			rend.material = defaultMAT;
			interacting = false;

		}

	}

	public void NPCInteract () {

		playerScript.evtScript.eventNum++;

		Debug.Log ("STOP MOVING PLAYER");
		playerScript.playerActive = false;
		//fixedCamera.enabled = true;
		rend.material = defaultMAT;
		npcGO.GetComponent<Collider> ().enabled = false;
		done = true;

	}

	public void NPCQuit () {

		playerScript.isInteract = false;
		playerScript.playerActive = true;
		//fixedCamera.enabled = false;
		rend.material = interactMAT;
		npcGO.GetComponent<Collider> ().enabled = true;
	}


}
