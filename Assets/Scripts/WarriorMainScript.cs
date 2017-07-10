using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WarriorMainScript : MonoBehaviour
{

	private Animator anim;
	public float speed = 3;
	public float shiftspeed = 1;
	public Camera cam, cam1, cam2;
	private Rigidbody commando;
	Vector3 rotCam;
	bool isAlive;
	public bool isGround;
	RaycastHit hit;
	public Transform sens;
	public bool Reloading;
	public bool FPS;
	public ShootScript shoot;
	public GameObject LeftRifle, RightRifle;
	public int HP;





	void Start ()
	{
		HP = 100;
		commando = GetComponent<Rigidbody> ();
		anim = GetComponent<Animator> ();
		isAlive = true;
		Reloading = false;
		FPS = true;
		Cursor.visible = !Cursor.visible;
		Cursor.lockState = CursorLockMode.Locked;
	

	}

	void FixedUpdate ()
	{
		
		if (isAlive) {
			transform.position = anim.rootPosition;
			commando.velocity = transform.TransformDirection (Input.GetAxis ("Horizontal") * speed * shiftspeed / 2, commando.velocity.y, Input.GetAxis ("Vertical") * shiftspeed * speed);
			transform.Rotate (0, Input.GetAxis ("Mouse X") * 2f, 0, Space.World);
			rotCam = cam.transform.localEulerAngles;
			cam.transform.Rotate (-Input.GetAxis ("Mouse Y") * 2f, 0, 0, Space.Self);

			if ((cam.transform.localEulerAngles.x < 35 || cam.transform.localEulerAngles.x < 360 && cam.transform.localEulerAngles.x > 330)) {
			} else {
				cam.transform.localEulerAngles = rotCam;
			}


		}
	}

	public void StartOfReload ()
	{
		Reloading = true;
		LeftRifle.SetActive (false);
		RightRifle.SetActive (true);
	}

	public void EndOfReload ()
	{
		Reloading = false;
		LeftRifle.SetActive (true);
		RightRifle.SetActive (false);
		shoot.allAmmo = shoot.allAmmo - (shoot.maxAmmo - shoot.currentAmmo);
		shoot.currentAmmo = shoot.maxAmmo;

		

	}

	void Update ()
	{
		if (isAlive) {
			anim.SetFloat ("YSpeed", commando.velocity.y);	
			if (Physics.Raycast (sens.position, Vector3.down, out hit, 0.5f)) {
				anim.SetBool ("jump", false);
				isGround = true;
								if (speed < 3) {
									speed += 1.2f * Time.deltaTime;
								}
				if (!anim.GetBool ("ctrl") && isGround && Input.GetKeyDown (KeyCode.Space)) {
					shoot.audioSource.PlayOneShot (shoot.jump);
					commando.AddForce (0, 2000, 0);
				}
				Debug.DrawRay (sens.position, Vector3.down);

			} else 
			{
								if (speed > 1.2f) {
									speed -= 2f * Time.deltaTime;
								}

				anim.SetBool ("jump", true);
				isGround = false;


			}
			if (Input.GetKeyDown (KeyCode.T)) {
				cam1.enabled = !cam1.enabled;
				cam2.enabled = !cam2.enabled;
				if (cam1.enabled) {
					cam = cam1;
				} else {
					cam = cam2;
				}

			}

			anim.SetBool ("isReloading", false);
			anim.SetBool ("L", false);
			anim.SetBool ("R", false);

			anim.SetBool ("move", false);
			if (Input.GetAxis ("Horizontal") != 0) {
				anim.SetBool ("move", true);
				anim.SetFloat ("XSpeed", Input.GetAxis ("Horizontal"));
			}
			
			if (Input.GetAxis ("Vertical") != 0) {
				anim.SetBool ("move", true);
				anim.SetFloat ("ZSpeed", Input.GetAxis ("Vertical"));
			} 
	
			if ((Input.GetAxis ("Horizontal") != 0 || Input.GetAxis ("Vertical") != 0) && Input.GetKey (KeyCode.LeftShift) && !Input.GetKey (KeyCode.LeftControl)) {
				shiftspeed = 2;
				anim.SetBool ("Shift", true);
				anim.SetFloat ("XSpeed", Input.GetAxis ("Horizontal") * 2);
				anim.SetFloat ("ZSpeed", Input.GetAxis ("Vertical") * 2);
			} else {
				shiftspeed = 1;
				anim.SetBool ("Shift", false);
			}
			if (Input.GetKey (KeyCode.LeftControl) && !Reloading) {
				
				anim.SetBool ("ctrl", true);
				shiftspeed = 0.4f;
				if (cam1.enabled) {
					cam.transform.localPosition = new Vector3 (cam.transform.localPosition.x, 1.35f, 0.34f);
				}
			}
			else{
//			} else { if (cam1 = enabled) {
//					cam1.transform.localPosition = new Vector3 (0.08f, 1.58f, 0.2f);
//				}
				anim.SetBool ("ctrl", false);
			}
			if (Input.GetKeyDown (KeyCode.R)) {

				anim.SetBool ("isReloading", true);
				shoot.audioSource.PlayOneShot (shoot.reload);

			}
		} 
	}

	void Die ()
	{
		print("Dssds");
		anim.SetBool ("isAlive", false);
		anim.SetTrigger ("Rel");
		anim.SetInteger ("DeathNumber", Random.Range (1, 7));
		cam1.enabled = false;
		cam2.enabled = true;
		
	}

	void OnTriggerEnter (Collider coll)
	{
		if (coll.transform.name == "Weapon") {
			HP = HP - 20;
			print ("HP = " + HP);  

			if (HP <= 0 && isAlive) {
				isAlive = false;                      
				Die ();
			}
		}
	}


	
}
