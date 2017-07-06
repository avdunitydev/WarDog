using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{
    float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 5)
        {
            timer = 0;            
            Destroy(gameObject);
        }
    }

}
