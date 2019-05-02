using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace UnityStandardAssets._2D
{

	public class PlayerControl : MonoBehaviour {

		public Text timerJB;
		public Text itemsMessage;
		public GameObject portalOut;

		GameObject jumpBoost;
		GameObject greenDoor;
		GameObject yellowDoor;
		GameObject redDoor;
		PlatformerCharacter2D character;
		Transform lastCheckPoint;
		GameManager gameManager;
	
		bool greenKey;
		bool yellowKey;
		bool redKey;
		bool portal;
		bool passFase;

		float timer = 5.0f;
		float messageTimer = 0.0f;
		float startJumpForce = 0;
		string message;

		// Use this for initialization
		void Start () {

			character = GetComponent<PlatformerCharacter2D>();
			gameManager =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager> ();
			startJumpForce = character.GetJumpForce ();
			timerJB.enabled = false;
			message = "";
			greenKey = false;
			yellowKey = false;
			redKey = false;
			portal = false;
			passFase = false;
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void FixedUpdate() {

			if (jumpBoost && Input.GetKeyDown ("p")) {
				timerJB.enabled = true;
				character.SetJumpForce(startJumpForce + 600f);
				Destroy (jumpBoost);
			}

			if (Input.GetKeyDown("i")) {
				if (greenDoor)
					Destroy (greenDoor);
				if (yellowDoor)
					Destroy (yellowDoor);
				if (redDoor)
					Destroy (redDoor);
			}

			if (Input.GetKeyDown ("t")) {
				if (portal)
					transform.position = portalOut.transform.position;
			}

			if (timerJB.enabled) {

				timer -= Time.deltaTime;
				timerJB.text = "JumpBoost Timer: " + timer.ToString ("F");

				if (timer <= 0) {
					character.SetJumpForce(startJumpForce);
					timer = 5f;
					timerJB.enabled = false;
				}
			}

			if (message != "") {
				itemsMessage.text = message;
				itemsMessage.color = Color.yellow;
				messageTimer += Time.deltaTime;
				if (messageTimer >= 3.0f) {
					message = "";
					itemsMessage.text = "";
					messageTimer = 0.0f;
				}
			}
		}

		void OnCollisionEnter2D(Collision2D coll) {

			// Mantain the player on the platform (update movement automatically)
			if (coll.gameObject.tag == "JumpBoost") {
				jumpBoost = coll.gameObject;
				message = "Press P to pick up JumpBoost";
			}
			if (coll.gameObject.tag == "Enemy" || 
				coll.gameObject.tag == "Spikes") {
				transform.position = lastCheckPoint.position;
			}
			if (coll.gameObject.tag == "GreenKey") {
				greenKey = true;
				gameManager.SetPlatformActive ("ExtentsLeft25");
				gameManager.SetKeyActive ("GreenKey");
				Destroy (coll.gameObject);
			}
			if (coll.gameObject.tag == "YellowKey") {
				yellowKey = true;
				Destroy (coll.gameObject);
				gameManager.SetPortalsActive ();
				gameManager.SetPlatformActive ("ExtentsLeft28");
				gameManager.SetKeyActive ("YellowKey");
			}
			if (coll.gameObject.tag == "RedKey") {
				redKey = true;
				Destroy (coll.gameObject);
				gameManager.SetPlatformNotActive ("Platform04x06");
				gameManager.SetPlatformNotActive ("Platform04x07");
				gameManager.SetKeyActive ("RedKey");
			}
			if (coll.gameObject.tag == "GreenDoor") {
				if (greenKey) {
					greenDoor = coll.gameObject;
					message = "Press I to open the door";
				}
				else
					message = "You need the green key";
			}
			if (coll.gameObject.tag == "YellowDoor") {
				if (yellowKey) {
					yellowDoor = coll.gameObject;
					message = "Press I to open the door";
				}
				else
					message = "You need the yellow key";
			}
			if (coll.gameObject.tag == "RedDoor") {
				if (redKey) {
					redDoor = coll.gameObject;
					message = "Press I to open the door";
				}
				else
					message = "You need the red key";
			}
			if (coll.gameObject.tag == "PlatformSlider02") {
				gameManager.SetPlatformActive ("Platform04x06");
			}
		}

		void OnTriggerEnter2D(Collider2D coll) {
			if (coll.gameObject.tag == "CheckPoint") {
				if (!coll.gameObject.GetComponent<CheckPoint> ().WasTrriged ()) {
					coll.gameObject.GetComponent<CheckPoint> ().SetTrigged ();
					lastCheckPoint = coll.gameObject.transform;
					message = "CheckPoint!";
					if (coll.gameObject.name == "Point06") {
						gameManager.SetPlatformActive ("Platform04x16");
						gameManager.SetPlatformActive ("Platform04x18");
					}
				}
			}
			if (coll.gameObject.tag == "PortalInCollider") {
				if (HasYellowKey()) {
					portal = true;
					message = "Press T to entry in the portal";

				}
			}
			if (coll.gameObject.tag == "PassFase") {
				gameManager.RestartSetActive ();
				passFase = true;
				gameManager.SetPlatformActive ("Platform04x19");
			}
		}

		public bool HasGreenKey() {
			return greenKey;
		}

		public bool HasYellowKey() {
			return yellowKey;
		}

		public bool HasRedKey() {
			return redKey;
		}
	}
}
