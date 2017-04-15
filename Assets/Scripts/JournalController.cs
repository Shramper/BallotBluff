using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalController : MonoBehaviour {

	public Animator journalAnim;
	public bool isActive;

	// Use this for initialization
	void Start () {

		journalAnim.SetBool ("Show", false);
		isActive = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenJournal () {

		journalAnim.SetBool ("Show", true);
		isActive = true;
	}

	public void CloseJournal () {

		journalAnim.SetBool ("Show", false);
		isActive = false;

	}
}
