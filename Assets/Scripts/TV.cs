using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class TV : MonoBehaviour {

	private Renderer rend;
	public Material interactMAT;
	public Material defaultMAT;

	public GameObject tvGO;
	public GameObject player;
	public Player playerScript;

	public GameObject evtGO;
	public EventController evtScript;

	public bool interacting;
	public bool done;
	public float tvNum;

	public Flowchart flowchart;

	// Use this for initialization
	void Start () {

		//Finding Player to enable
		playerScript = player.GetComponent<Player> ();

		//Finding Renderer to highlight object
		rend = tvGO.GetComponent<Renderer>();

		evtScript = evtGO.GetComponent<EventController> ();

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

	public void TVInteract () {

		//playerScript.evtScript.eventNum++;
		Debug.Log ("We're at event " + evtScript.eventNum);
		playerScript.playerActive = false;
		rend.material = defaultMAT;
		tvGO.GetComponent<Collider> ().enabled = false;

		if (evtScript.eventNum == 0) {

			flowchart.ExecuteBlock ("TV_Tick1");
			tvNum = 1;

		} else if (evtScript.eventNum == 1) {

			flowchart.ExecuteBlock ("TV_Tick2");
			tvNum = 2;

		} else if (evtScript.eventNum == 2) {

			flowchart.ExecuteBlock ("TV_Tick3");
			tvNum = 3;

		}  else if (evtScript.eventNum >= 3) {

			tvNum = 4;
			Debug.Log ("No More TV!");
			flowchart.ExecuteBlock ("TV_Tick4");

		}

		done = true;

	}

	public void TVQuit () {

		playerScript.isInteract = false;
		playerScript.playerActive = true;
		//fixedCamera.enabled = false;
		rend.material = interactMAT;
		tvGO.GetComponent<Collider> ().enabled = true;

	}

}
