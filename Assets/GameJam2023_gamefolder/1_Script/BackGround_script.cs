using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NutrientFlask;

public class BackGround_script : MonoBehaviour
{
    public GameObject gameManager;
    void Start()
    {
        gameManager.GetComponent<Script_GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "drag")
        {
            int id = other.gameObject.GetComponent<NutrientFlask_Original>().nutrientFlaskData.flaskIndex;
            gameManager.GetComponent<Script_GameManager>().flaskOpeningList[id - 1].PlayFeedbacks();
            print("BackGround_Enter");
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "drag")
        {
            print("BackGround_Stay");
        }

    }
}
