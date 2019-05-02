using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D
{
	public class PlantformMove : MonoBehaviour {

	    private Transform currentMoveTransform = null;

		public GameObject Other;
	    public Transform start;
	    public Transform finish;

		public float speed = 2.0f;

	    // Use this for initialization
	    void Start () {
	        currentMoveTransform = finish;
			if(Other) 
				Other.transform.SetParent(transform, true);
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
	        }
	    }

	    void OnCollisionEnter2D(Collision2D coll) {

	        // Mantain the player on the platform (update movement automatically)
	        if (coll.gameObject.tag == "Player") {
	            coll.collider.transform.SetParent(transform);	        }
	    }

	    void OnCollisionExit2D(Collision2D coll) {

	        // Mantain the player on the platform (update movement automatically)
	        if (coll.gameObject.tag == "Player") {
	            coll.collider.transform.SetParent(null);
	        }
	    }
	}
}

