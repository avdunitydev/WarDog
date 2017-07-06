using UnityEngine;
using UnityEngine.Networking;

public class UNOPlayerShoot1 :MonoBehaviour
{
    public float damage = 10;
    public float range = 100;
    public LayerMask mask;
    public Camera cam;
    public Rigidbody bullet;
    public Transform gun;
	//Animator anim;
	public WarriorMainScript war;

    void Start()
    {
        if (cam == null)
        {
            Debug.LogError("PlayerShoot: no camera");
            this.enabled = false;
        }
		//anim = GetComponent<Animator> ();
    }

    void Update()
	{
		
	}

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            CmdPlayerShoot(gameObject.name, 20);
            Destroy(coll.gameObject);
        }
    }

  
   public void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, mask))
        {
            if (hit.collider.tag == "Enemy")
            {
                CmdPlayerShoot(hit.collider.name, damage);
            }
        }
    }

 
    void CmdPlayerShoot(string id, float damage)
    {
        Debug.Log("in player" + id + " shoot");
        Player player = GameManager.GetPlayer(id);
        player.TakeDamage(damage);
    }

  
   public void CmdShootBulet(Vector3 pos, Vector3 direction)
    {
        RpcShootBulet(pos, direction);
    }

    
    void RpcShootBulet(Vector3 pos, Vector3 direction)
    {
        Rigidbody temp = Instantiate(bullet, pos, Quaternion.identity);
        temp.AddForce(direction);
    }
}
