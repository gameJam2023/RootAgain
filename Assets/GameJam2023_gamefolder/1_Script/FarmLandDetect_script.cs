using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLandDetect_script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "drag")
            print("Enter");
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "drag")
            print("Stay");
    }



}
