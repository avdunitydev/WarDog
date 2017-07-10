using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOpenDoor : MonoBehaviour
{
	public float sppedCloseOpenDoor;
	Vector3 doorOpen = new Vector3 (0, 100, 0);
	Vector3 doorClose = new Vector3 (0, 0, 0);
	public bool isCloseDoor = false;

	// Use this for initialization
	void Start ()
	{
		//Debug.Log ("Time.time == " + Time.time);
	}

	void FixedUpdate ()
	{
	}
	// Update is called once per frame
	void Update ()
	{
		if (isCloseDoor && Input.GetKeyDown (KeyCode.E)) {
			if (transform.rotation.y == 0) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (doorOpen), Time.time * sppedCloseOpenDoor);
				//Debug.Log ("transform.rotation.y == " + transform.rotation.y);
				//isCloseDoor = false;
			} else if (transform.rotation.y >= .7f) {
				transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (doorClose), Time.time * sppedCloseOpenDoor);
				//Debug.Log ("transform.rotation.y == " + transform.rotation.y);
				//isCloseDoor = false;
			}
		}
		//Debug.Log ("Update >> Time.time == " + Time.time);
	}

	void OnTriggerEnter (Collider other)
	{
		if (Equals (other.tag, "Player")) {
			isCloseDoor = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (Equals (other.tag, "Player")) {
			isCloseDoor = false;
		}
	}

	//iTween -- move
	/*void iMove ()
	{
		iTween.MoveBy (gameObject, 
			iTween.Hash ("x", 2, "easeType", "easeInOutExpo", "loopType", "pingPong", "delay", .1));
	}

	//iTween -- rotate
	void iRotate ()
	{
		iTween.RotateBy (gameObject, 
			iTween.Hash ("x", .25, "easeType", "easeInOutBack", "loopType", "pingPong", "delay", .4));

	}*/
}
