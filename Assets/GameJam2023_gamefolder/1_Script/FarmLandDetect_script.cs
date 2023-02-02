using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmLandDetect_script : MonoBehaviour
{
    public GameObject gameManager;
    public int index;
    public bool isFarmLandFilling = false; //!倒水果下


    // public Vector3 nutrientFlaskPosition;


    void Start()
    {

        gameManager.GetComponent<Script_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {



        print("Enter");
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isFarmLandFilling)
        {
            if (other.gameObject.tag == "drag")
            {
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                print("Stay");
                //舊野要發光
            }
            if (other.gameObject.tag == "drag")
            {
                if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<Script_GameManager>().selectedObject != null)
                {

                    isFarmLandFilling = true;
                    // gameManager.GetComponent<GameManager_script>().nutrientList[index - 1].flaskFalling = true;
                    print("fallingTrue");
                }
            }
        }
        else
        {
            //gameManager.GetComponent<GameManager_script>().farmlandList[index - 1].nutrientTotalCount++;
            //gameManager.GetComponent<GameManager_script>().flaskFillingFeedBackList[index - 1].PlayFeedbacks();
            StartCoroutine(fallingTest(other)); //? falling animation 等你加

        }


        // if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<GameManager_script>().selectedObject != null)
        // {
        //     gameManager.GetComponent<GameManager_script>().farmlandList[index - 1].nutrientTotalCount++;
        //     falling = true;
        //     print("fallingTrue");
        // }
        // if (falling)
        // {
        //     StartCoroutine(fallingAction(other, fallingHeight));
        //     falling = false;
        // }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "drag")
        {
            this.GetComponent<MeshRenderer>().material.color = Color.white;
        }
    }

    IEnumerator fallingTest(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = Color.red;
        int id = other.gameObject.GetComponent<NutrientFlask_Original>().nutrientFlaskData.flaskIndex;
        gameManager.GetComponent<Script_GameManager>().flaskOpeningList[id - 1].PlayFeedbacks();
        print("fallingAnimation");
        this.isFarmLandFilling = false;
        yield return null;
        // gameManager.GetComponent<GameManager_script>().nutrientFlaskDataBase[other.]
    }


    // IEnumerator fallingAction(Collider other, float fallingHeight)
    // {
    //     if (falling)
    //     {
    //         //倒既動作
    //         Vector3 tempPos = new Vector3(this.transform.position.x, this.transform.position.y + fallingHeight, this.transform.position.z);
    //         print("THIS:" + this.transform.position);
    //         print("TEMP:" + tempPos);
    //         other.transform.position = tempPos;
    //         yield return new WaitForSeconds(1f);
    //         print("falling");
    //     }

    // }


}
