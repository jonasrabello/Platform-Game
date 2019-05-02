using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

	private Transform currentMoveTransform = null;

	public Transform start;
	public Transform finish;

	public float speed;

	SpriteRenderer sprRend;

	// Use this for initialization
	void Start () {
		sprRend = GetComponent<SpriteRenderer> ();
		currentMoveTransform = finish;
		speed = 2.0f;		
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = Vector3.MoveTowards(transform.position, currentMoveTransform.position, speed * Time.deltaTime);

		float distance = Vector3.Distance(transform.position, currentMoveTransform.position);

		if (Mathf.Round(distance) == 0) {
			if (currentMoveTransform == finish) {
				currentMoveTransform = start;
			}
			else {
				currentMoveTransform = finish;
			}
			sprRend.flipX = !sprRend.flipX;
		}		
	}
}
