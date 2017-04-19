using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class NewsController : MonoBehaviour {

	public Animator newsAnim;
	public bool isActive;

	public Text txtNews;

	public Flowchart flowchart;

	[Header("References")]
	public GameObject player;
	public Player playerScript;

	// Use this for initialization
	void Start () {

		newsAnim.SetBool ("Show", false);
		isActive = false;

		playerScript = player.GetComponent<Player> ();

	}
	
	// Update is called once per frame
	void Update () {

		//News Info in Newspaper
		if (flowchart.GetFloatVariable ("newsNum") == 1.0f) {

			txtNews.text = "This person tends to ramble and go off topic.";

		} else if (flowchart.GetFloatVariable ("newsNum") == 2.0f) {

			txtNews.text = "This person may get mistaken for someone else.";

		} else if (flowchart.GetFloatVariable ("newsNum") == 3.0f) {

			txtNews.text = "This person usually wears dark clothing.";

		}

	}

	public void OpenNews () {

		//Debug.Log("OpenNews() Activated");
		newsAnim.SetBool ("Show", true);
		isActive = true;
	}

	public void CloseNews () {

		//Debug.Log("CloseNews() Activated");
		newsAnim.SetBool ("Show", false);
		isActive = false;

	}
}
