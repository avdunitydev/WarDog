using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{

	public float damage = 10f;
	public float range = 100f;
	public float fireRate = 21f;
	public float impactForce = 30f;
	private bool isReloading;
	WarriorMainScript mainscr;
	public int maxAmmo = 30;
	public int currentAmmo;
	public int allAmmo = 150;
	public float reloadTime = 1f;
	public Camera fpsCam, tpsCam;
	public ParticleSystem muzzleFlash;
	public GameObject impacteffect;
	private float nextTimeToFire = 0f;
	Animator animator;
	public GameObject Blood;
	public Text AmmoTextUI;
	public AudioClip reload, shot, jump;
	public AudioSource audioSource;

	void Start ()
	{

		mainscr = GetComponent <WarriorMainScript> ();
		animator = GetComponent<Animator> ();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		isReloading = mainscr.Reloading;

		if (Input.GetMouseButton (0) && Time.time >= nextTimeToFire && !isReloading) {
			currentAmmo--;
			//AmmoTextUI.text = currentAmmo.ToString();

			if (currentAmmo <= 0) {
				animator.Play ("reload_inPlace");
				audioSource.PlayOneShot (reload);
			} else {
				nextTimeToFire = Time.time + 1f / fireRate;
				Shoot ();
			}
		}
	}

	public	void Shoot ()
	{
		if (currentAmmo <= 0) {
			return;
		}
		audioSource.PlayOneShot (shot);
		muzzleFlash.Play ();
		if (fpsCam.enabled) {
			print("12122");
			RaycastHit hit;
			if (Physics.Raycast (fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
				Debug.Log (hit.transform.name);

				if (hit.transform.tag == "human") {
					GameObject inpact1 = Instantiate (Blood, hit.point, Quaternion.LookRotation (hit.normal));
					Destroy (inpact1, 2f);
					Target target = hit.transform.GetComponent<Target> ();

					if (target != null) {
						target.TakeDamage (damage);
					}
				
				} else {
					GameObject inpact2 = Instantiate (impacteffect, hit.point, Quaternion.LookRotation (hit.normal));
					Destroy (inpact2, 2f);
				}
						
			}
		} else if (!fpsCam.enabled) {
			print ("ELSE");
			RaycastHit hit;
			if (Physics.Raycast (tpsCam.transform.position, tpsCam.transform.forward, out hit, range)) {
				Debug.Log (hit.transform.name);

				if (hit.transform.tag == "human") {
					GameObject inpact1 = Instantiate (Blood, hit.point, Quaternion.LookRotation (hit.normal));
					Destroy (inpact1, 2f);
					Target target = hit.transform.GetComponent<Target> ();

					if (target != null) {
						target.TakeDamage (damage);
					}

				} else {
					GameObject inpact2 = Instantiate (impacteffect, hit.point, Quaternion.LookRotation (hit.normal));
					Destroy (inpact2, 2f);
				}

			}}
	}


}
