
using UnityEngine;
using UnityEngine.AI;

public class Target : MonoBehaviour {

	public float health = 150f;
	public bool isAlive;
	Animator anim;
	public bool attackRange;
	public Transform sold;
	Vector3 pos;
	bool Agr;
	bool agr1=true;
	NavMeshAgent agent;
	public AudioClip roar, attack,death;
	AudioSource _audioSource;
	float timer = 0, timer2 = 0;
	public bool playerIsDead = false;
	public float visible = 25f;
	public float angleV = 80f;

	public Transform [] mas;
	int idPoint = 0;

	public Transform obj;

	void Start () {
		agent = GetComponent<NavMeshAgent> ();

		agent.SetDestination (mas [0].position);
		Agr = false;
		isAlive = true;
		_audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();

		obj = GetComponentInChildren<weaponscipr> ().transform;

	}


	void Update(){
		anim.SetFloat ("Move", Mathf.Abs( agent.velocity.z));
		if ((Vector3.Distance (transform.position, mas [idPoint].position) < 2) && !Agr) {
			timer2 += Time.deltaTime;
			if (timer2 > 10) {
				timer2 = 0;
			idPoint = ++idPoint % mas.Length;
				agent.SetDestination (mas [idPoint].position);
			}
		}
		float distance = Vector3.Distance (sold.position, gameObject.transform.position);
		if (playerIsDead) {
			anim.SetBool ("PK", true);
			anim.SetBool ("Agro", false);
			anim.SetBool ("Attack", false);
			anim.SetBool ("Run", false);
			anim.SetBool ("Attack", false);
		}
		if(sold.GetComponent<WarriorMainScript> ().HP <=0 )
		{
			playerIsDead = true;
		}
		if (isAlive && playerIsDead == false) {

			timer = timer + Time.deltaTime;

			if (Agr == true) {
				agent.SetDestination(sold.position);
				anim.SetBool ("Agro", true);
				Run ();
			}

			if (health < 150) {
				Agr = true;
				if (agr1) {
					agr1 = false;
					_audioSource.PlayOneShot (roar);
				}
				Run ();
			}
			if (distance < visible ) {
				Quaternion look = Quaternion.LookRotation (sold.position - transform.position);
				float angle = Quaternion.Angle (transform.rotation, look);
				if (angle < angleV) {

					RaycastHit hit; 
					Ray ray = new Ray (transform.position + Vector3.up * 1.2f, sold.position - transform.position);
					Debug.DrawRay (transform.position + Vector3.up * 1.2f, sold.position - transform.position);
					if(Physics.Raycast(ray, out hit, visible))
					{
						
						if(hit.transform == sold){
							
							Agr = true;
						}
					}
				}

			} 
			if (distance < 2f) {
				Attack ();
			}
			else if (distance > 2f) 
			{
				anim.SetBool ("Attack", false);
			}
		} 
	}

			public void TakeDamage (float amount)
	{
		if (isAlive) {
			health -= amount;
			if (health <= 0f) {
				Die ();
			}
		}
	}

			void Run(){
				agent.SetDestination(sold.position);
				anim.SetBool ("Run", true);
			}

			void Attack()
	{			Agr = true;
				anim.SetBool ("Attack", true);
				if (timer >= 2f) {
					timer = 0;
					_audioSource.PlayOneShot (attack);
				}
			}
			void Die()
			{
			_audioSource.PlayOneShot (death);
				anim.SetLayerWeight (1, 0f);
				anim.SetBool ("IsAlive", false);
				anim.SetBool ("Agro", false);
				Agr = false;
				obj.gameObject.SetActive (false);
				GetComponent<CapsuleCollider> ().isTrigger = true;
				isAlive = false;

			}	
}
