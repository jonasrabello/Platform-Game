using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace UnityStandardAssets._2D
{
		public class GameManager : MonoBehaviour {

		public Button restartButton;
		public GameObject restart;
		public GameObject final;

		GameObject[] platforms;
		GameObject[] enemies;
		GameObject[] keys;


		GameObject portalIn;
		GameObject portalOut;

		// Use this for initialization
		void Start () {
			restartButton.onClick.AddListener(Restart);
			restart.SetActive (false);
			final.SetActive (false);
			platforms = GameObject.FindGameObjectsWithTag ("Platform");
			enemies = GameObject.FindGameObjectsWithTag("EnemyPortal");
			keys = GameObject.FindGameObjectsWithTag ("KeyHUD");
			portalIn = GameObject.FindGameObjectWithTag ("PortalIn");
			portalOut = GameObject.FindGameObjectWithTag("PortalOut");
			HidePlatform ();
			HideKeys ();
			HidePortals ();
		}
		
		// Update is called once per frame
		void Update () {

			if (Input.GetKey("escape"))	{
				Application.Quit();
			}
		}

		GameObject GetPlatform(string name) {
			foreach(GameObject p in platforms) {
				if (p.name == name)
					return p;
			}
			return null;
		}

		GameObject GetKey(string name) {
			foreach(GameObject k in keys) {
				if (k.name == name)
					return k;
			}
			return null;
		}

		public void SetPlatformActive(string name) {
			GameObject platform = GetPlatform (name);
			if(platform) {
				platform.SetActive (true);
			}
		}

		public void SetPlatformNotActive(string name) {
			GameObject platform = GetPlatform (name);
			if(platform) {
				platform.SetActive (false);
			}
		}

		public void SetPortalsActive() {
			portalIn.SetActive (true);
			portalOut.SetActive (true);
			foreach (GameObject e in enemies) {
				e.SetActive (false);
			}
		}

		public void SetKeyActive(string name) {
			GameObject key = GetKey (name);
			if (key)
				key.SetActive (true);
		}

		void HidePlatform() {
			foreach(GameObject p in platforms) {
				if (p.name == "Platform04x06")
					p.SetActive (false);
				if (p.name == "Platform04x16")
					p.SetActive (false);
				if (p.name == "Platform04x18")
					p.SetActive (false);
				if (p.name == "Platform04x19")
					p.SetActive (false);
				if (p.name == "ExtentsLeft25")
					p.SetActive (false);
				if (p.name == "ExtentsLeft28")
					p.SetActive (false);
			}
		}

		void HideKeys() {
			foreach (GameObject k in keys) {
				k.SetActive (false);
			}
		}

		void HidePortals() {
			portalIn.SetActive (false);
			portalOut.SetActive (false);
		}

		GameObject GetCheckPoint(string name) {
			foreach(GameObject c in platforms) {
				if (c.name == name)
					return c;
			}
			return null;
		}

		public void Restart() {
			SceneManager.LoadScene("Scene01");
		}

		public void RestartSetActive() {
			restart.SetActive (true);
			final.SetActive (true);
		}
	}
}
