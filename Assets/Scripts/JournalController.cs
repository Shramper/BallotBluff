using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;

public class JournalController : MonoBehaviour {

	public Animator journalAnim;
	public bool isActive;

	public Text txtNews;
	public Text txtNPC;
	public Text txtTV;

	public Flowchart flowchart;

	[Header("References")]
	public GameObject player;
	public Player playerScript;
	//public GameObject fixedCamGO;
	//public FixedCam fixedCamScript;
	//public GameObject npcGO;
	//public NPC npcScript;
	public GameObject evtGO;
	public EventController evtScript;
	public GameObject tvGO;
	public TV tvScript;

	// Use this for initialization
	void Start () {

		journalAnim.SetBool ("Show", false);
		isActive = false;

		tvScript = tvGO.GetComponent<TV> ();
		evtScript = evtGO.GetComponent<EventController> ();
		playerScript = player.GetComponent<Player> ();

	}
	
	// Update is called once per frame
	void Update () {

		//News Info in Journal
		if (flowchart.GetFloatVariable ("newsNum") == 0.0f) {

			txtNews.text = "I have yet to read the news.";

		} else if (flowchart.GetFloatVariable ("newsNum") == 1.0f) {

			txtNews.text = "This person tends to ramble and go off topic.";

		} else if (flowchart.GetFloatVariable ("newsNum") == 2.0f) {

			txtNews.text = "This person may get mistaken for someone else.";

		} else if (flowchart.GetFloatVariable ("newsNum") == 3.0f) {

			txtNews.text = "This person usually wears dark clothing.";

		}

		//NPC Info in Journal
		if (flowchart.GetFloatVariable ("npcNum") == 0) {

			txtNPC.text = "I have yet to talk to the NPC.";

		} else if (flowchart.GetFloatVariable ("npcNum") == 1) {

			txtNPC.text = "This person wears fancy clothes, and glasses.";

		} else if (flowchart.GetFloatVariable ("npcNum") == 2) {

			txtNPC.text = "This person is male.";

		} else if (flowchart.GetFloatVariable ("npcNum") == 3) {

			txtNPC.text = "This person dabbles in the game dev industry, but also teaches.";

		} else if (flowchart.GetFloatVariable ("npcNum") == 4) {

			txtNPC.text = "This person isn't originally from Toronto.";

		}
			
		//TV Info in Journal
		if (tvScript.tvNum == 0) {

			txtTV.text = "I have yet to watch the news.";

		} else if (tvScript.tvNum == 1) {

			txtTV.text = "This person has gone to Level Up Showcase, and spoken with big people there.";

		} else if (tvScript.tvNum == 2) {

			txtTV.text = "This person is an irregular visitor, except for one particular day.";

		}  else if (tvScript.tvNum == 3) {

			txtTV.text = "This person was seen recently leaving the building with a student and fellow teacher from last semester";

		} else if (tvScript.tvNum >= 4) {

			txtTV.text = "I unfortunately missed the latest news stories.";

		}

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
