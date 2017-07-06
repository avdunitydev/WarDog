using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float speed = 5f;
    public float lookSpeed = 5f;
    public Camera cam;
    public LayerMask ground;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {        
        rb.velocity = transform.TransformDirection(Input.GetAxis("Horizontal") * speed, rb.velocity.y, Input.GetAxis("Vertical") * speed);

        transform.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed, 0, Space.World);
        cam.transform.Rotate(-Input.GetAxis("Mouse Y") * lookSpeed, 0, 0, Space.Self);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics.OverlapSphere(transform.position - new Vector3(0, 1.4f, 0), 0.5f, ground).Length > 0)
            {
                rb.AddForce(0, 300, 0);
            }

        }
    }

}
