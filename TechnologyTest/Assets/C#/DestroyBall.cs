using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    private const string floor = "Floor";

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == floor)
        {
            Destroy(this.gameObject);
        }
    }
}
