using UnityEngine;
using System.Collections;
using Fungus;

public class Player : MonoBehaviour {

	[Header("Movement")]
	public float forwardspeed;
	public Vector3 speed;
	public float sideSpeed;
	public float movementSpeed = 5.0f;
	public float mouseSens = 5.0f;
	public float horizontalRot = 0.0f;
	public float verticalRot = 0.0f;
	public float rotRange = 60.0f;

	[Header("Fixed Camera")]
	public GameObject fixedCamNode;
	public FixedCam fixedCamScript;
	public GameObject fixedCamGO;
	public Camera fixedCamera;

	[Header("References")]
	public Flowchart flowchart;
	public GameObject journalGO;
	public JournalController journalScript;

	[Header("Variables")]
	public float numNewsObj;
	public bool newsChosen = false;

	public bool isInteract;
	public bool playerActive;


	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		//Cursor.lockState = CursorLockMode.Locked;

		//Finding the Interactive Objects + Cameras
		fixedCamScript = fixedCamNode.GetComponent<FixedCam> (); //Getting Fixed Cam Script
		fixedCamera = fixedCamGO.GetComponent<Camera>(); //Getting Fixed Cam Camera
		journalScript = journalGO.GetComponent<JournalController>(); //Getting Journal Controller Script

		isInteract = false;
		playerActive = true;
		Camera.main.enabled = true;
		fixedCamera.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

		if (flowchart.GetBooleanVariable ("diagActive") || isInteract == true) {

			//Debug.Log (flowchart.GetBooleanVariable ("diagActive"));
			playerActive = false;

		} else if (!flowchart.GetBooleanVariable ("diagActive") || isInteract == false) {

			//Debug.Log (flowchart.GetBooleanVariable ("diagActive"));
			playerActive = true;
			Cursor.lockState = CursorLockMode.Locked;

		}


		//Mouse Position on Fixed Camera
		ray = fixedCamera.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast (ray, out hit)) {
			//print (hit.collider.name);
			if (hit.collider.name == "obj_News1") {

				numNewsObj = 1.0f;

			} else if (hit.collider.name == "obj_News2") {
				
				numNewsObj = 2.0f;

			} else if (hit.collider.name == "obj_News3") {

				numNewsObj = 3.0f;

			} else if (hit.collider.name == "Floor") { //Not touching any of the newspapers
				
				numNewsObj = 0.0f;

			}
				
		}

		//If Main Camera is enabled
		if (Camera.main.enabled == true) {

			if (playerActive) {
				
				//Rotation
				horizontalRot = Input.GetAxis ("Mouse X") * mouseSens;
				transform.Rotate (0, horizontalRot, 0);

				//Mouse Look
				verticalRot -= Input.GetAxis ("Mouse Y") * mouseSens;
				verticalRot = Mathf.Clamp (verticalRot, -rotRange, rotRange);
				Camera.main.transform.localRotation = Quaternion.Euler (verticalRot, 0, 0);

				//Simple Movement
				forwardspeed = Input.GetAxis ("Vertical") * movementSpeed;
				sideSpeed = Input.GetAxis ("Horizontal") * movementSpeed;

				speed = new Vector3 (sideSpeed, 0, forwardspeed);

				speed = transform.rotation * speed;

				CharacterController cc = GetComponent<CharacterController> ();
				cc.SimpleMove (speed);
			}

			//Interact Controls
			if (Input.GetMouseButtonDown(0)) { //LEFT CLICK to interact

				if (fixedCamScript.newsUIScript.isActive) { //Closing the UI

					Debug.Log ("CLOSE NEWSPAPER");
					fixedCamScript.newsUIScript.CloseNews ();

				}

				//If Interacting with Still Cam Object - Fixed Camera on an object
				if (fixedCamScript.interacting == true && playerActive) {

					isInteract = true; //If player is currently interacting with something.
					Cursor.lockState = CursorLockMode.None;
					Cursor.visible = true;

					fixedCamScript.FixCam (); //THE CAMERA IS NOW FIXED ON THE NEWSPAPERS
				
					if (flowchart.GetFloatVariable ("newsNum") > 0) { //If a Newspaper has already been chosen

						flowchart.ExecuteBlock ("FixedCam_Start"); //****Play a Block that supports chosen newspaper, or skip to a certain block

					} else {

						flowchart.ExecuteBlock ("FixedCam_Start"); //Otherwise, choose Newspaper

					}

				}

				if (isInteract && !flowchart.GetBooleanVariable ("diagActive") && !newsChosen) { //Clicking during fixed Camera

					if (numNewsObj == 1.0f) { 

						Debug.Log ("NEWSPAPER 1");
						flowchart.ExecuteBlock ("FixedCam_News1"); //I have to have everything work out here.

					} else if (numNewsObj == 2.0f) {

						Debug.Log ("NEWSPAPER 2");
						flowchart.ExecuteBlock ("FixedCam_News2");

					} else if (numNewsObj == 3.0f) {

						Debug.Log ("NEWSPAPER 3");
						flowchart.ExecuteBlock ("FixedCam_News3");

					}

				}


				//If Interacting with Dialogue Object - Dialogue over screen


				//If Interacting with CamDialogue Object - Fixed Camera on object, Dialogue over screen

			}



			if (Input.GetMouseButtonDown (1)) { //RIGHT CLICK to stop interacting


				if (fixedCamScript.interacting == true && !playerActive) {
						
					Debug.Log ("Finishing fixed cam");
					isInteract = false;
					fixedCamScript.UnfixCam ();

				}
					

				//Cancel out of Dialogue Box

				//Cancel out of CamDialogue

			}


			if (Input.GetKeyDown (KeyCode.Space)) { //Bringing up Journal

				if (!flowchart.GetBooleanVariable ("diagActive")) { //Does not open Journal during Dialogue

					Debug.Log("Pressed Space");
					if (journalScript.isActive == false) {

						journalScript.OpenJournal ();

					} else if (journalScript.isActive == true) {

						journalScript.CloseJournal ();

					}

				}
					
			}
					
		}
			
	}

}
	