using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	public float xMax;
	public float yMax;
	public float xMin;
	public float yMin;

	Transform target;

	// Use this for initialization
	void Start () {


	}

	// Update is called once per frame
	void Update () {
		if (!target) {
			
			target = GameObject.FindGameObjectWithTag ("Player").transform;
		}

		transform.position = new Vector3 (Mathf.Clamp (target.position.x, xMin, xMax), Mathf.Clamp (target.position.y, yMin, yMax),
			transform.position.z);
	}
}
