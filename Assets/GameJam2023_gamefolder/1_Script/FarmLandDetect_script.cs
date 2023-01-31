using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmLandDetect_script : MonoBehaviour
{
    public GameObject gameManager;
    public int index;


    void Start()
    {
        gameManager.GetComponent<GameManager_script>();
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
        {
            print("Stay");
            //舊野要發光
        }
        if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<GameManager_script>().selectedObject != null)
        {
            gameManager.GetComponent<GameManager_script>().farmlandList[index - 1].nutrientTotalCount++;
        }

    }



}
