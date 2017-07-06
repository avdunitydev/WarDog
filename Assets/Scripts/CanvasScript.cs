using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasScript : MonoBehaviour {

	public ShootScript shootscr; 

	Text text;
	void Start () {
		text = GetComponent<Text> ();
	}

	void Update () {
		text.text = shootscr.currentAmmo.ToString ();
	}
}
