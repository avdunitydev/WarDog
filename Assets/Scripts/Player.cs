using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{
    public float maxHP = 100;
    public float hp;
	public LayerMask headoff;
	public LayerMask headon;
    public Behaviour[] compDis;
	public  Camera cam;

    void Start()
    {
        hp = maxHP;
        if (!isLocalPlayer)
        {
            for (int i = 0; i < compDis.Length; ++i)
            {
                compDis [i].enabled = false;
            }
//            gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
			compDis [0].GetComponent<Camera> ().cullingMask = headon;
			compDis [4].GetComponent<Camera> ().cullingMask = headon;

        } else
        {
			compDis [0].GetComponent<Camera> ().cullingMask = headon;
			compDis [4].GetComponent<Camera> ().cullingMask = headoff;
            cam = Camera.main;
//            cam.gameObject.SetActive(false);
        }
    }

    public override void OnStartClient()
	{
        base.OnStartClient();
        GameManager.RegisterPlayer(GetComponent<NetworkIdentity>().netId.ToString(), this);
    }

    void OnDisable()
	{ 
        //cam.gameObject.SetActive(true);
        GameManager.UnRegisterPlayer(transform.name);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        Debug.Log(transform.name + " take HP " + hp);
        if (hp <= 0)
        {
            if (isLocalPlayer)
            {
                cam.gameObject.SetActive(true);
                compDis [0].enabled = false;
            }
        }
    }


}
