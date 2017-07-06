
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
	public AudioClip roar, attack;
	AudioSource _audioSource;
	public float timer = 0;
	public bool playerIsDead = false;
	public float visible = 25f;
	public float angleV = 80f;

	void Start () {
		Agr = false;
		isAlive = true;
		_audioSource = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		agent = GetComponent<NavMeshAgent> ();
	}


	void Update(){
		float distance = Vector3.Distance (sold.position, gameObject.transform.position);
		if (playerIsDead) {
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
					Debug.Log ("неможу сюда зайти юююююб///");
					RaycastHit hit; 
					Ray ray = new Ray (transform.position + Vector3.up * 1.2f, sold.position - transform.position);
					Debug.DrawRay (transform.position + Vector3.up * 1.2f, sold.position - transform.position);
					if(Physics.Raycast(ray, out hit, visible))
					{
						Debug.Log ("неможу сюда зайти 1111/");
						if(hit.transform == sold){
							Debug.Log ("неможу сюда зайти 33333");
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
				health -= amount;
				if (health <= 0f) {
					Die ();
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
				anim.SetLayerWeight (1, 0f);
				anim.SetBool ("IsAlive", false);
				anim.SetBool ("Agro", false);
				isAlive = false;
				Agr = false;
				GetComponent<CapsuleCollider> ().isTrigger = true;
			}	
}
