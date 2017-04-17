using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsController : MonoBehaviour {

	public Animator newsAnim;
	public bool isActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenNews () {

		newsAnim.SetBool ("Show", true);
		isActive = true;
	}

	public void CloseNews () {

		newsAnim.SetBool ("Show", false);
		isActive = false;

	}
}
