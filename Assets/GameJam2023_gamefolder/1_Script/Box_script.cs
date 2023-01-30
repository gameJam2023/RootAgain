using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box_script : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Grab");
        }
        else if (other.tag == "Ground")
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (other.collider.tag == "Player")
    //     {
    //         print("Grab");
    //     }
    //     else if (other.collider.tag == "Ground")
    //     {
    //         rb.isKinematic = true;
    //         rb.useGravity = false;
    //     }
    // }


}
