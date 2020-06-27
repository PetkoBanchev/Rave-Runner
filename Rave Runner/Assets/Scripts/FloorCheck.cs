using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheck : MonoBehaviour {

	// Use this for initialization

	private PlayerMovement playerScript;

	void Start() {
		playerScript = gameObject.GetComponentInParent<PlayerMovement>();
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.name == "Floor")
		{
			playerScript.isOnFloor = true;
		}
		
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.name == "Floor")
		{
			playerScript.isOnFloor = true;
		}
	}

	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.name == "Floor")
		{
			playerScript.isOnFloor = false;
		}
	}
}
