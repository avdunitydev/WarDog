using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recharge : MonoBehaviour {


	public Transform lefthand, righthand, clip, pos;
	Vector3 posclip;


	void Update(){
		
	}

	void Begin ()
	{ posclip = clip.localPosition;
		clip.transform.parent = lefthand;
	}
	void End()
	{
		clip.transform.parent = righthand;
		clip.localPosition= posclip;
		clip.localEulerAngles = Vector3.zero;
	}
}
