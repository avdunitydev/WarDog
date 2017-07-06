using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RRR : MonoBehaviour {

	Animator an;
	public Transform lefthand, righthand, gun;
	// Use this for initialization
	 WarriorMainScript wars;
	void Start () {
		an = GetComponent<Animator> ();
		wars = GetComponentInParent<WarriorMainScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (wars.Reloading == true) {
			gameObject.transform.parent = righthand;
		} else {
			gameObject.transform.parent = lefthand;
			//transform.position = new Vector3 (-0.082f, 0.076189f, -0.07118942f);
		}

		
		an.SetBool ("R", false);
		if (Input.GetKeyDown (KeyCode.K)) {
			an.SetBool ("R", true);}
	}
}
