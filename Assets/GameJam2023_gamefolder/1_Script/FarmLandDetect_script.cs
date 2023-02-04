using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FarmLandDetect_script : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject seedObj;
    public GameObject original_pos;
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
            if (other.gameObject.tag == "isFlask" || other.gameObject.tag == "isSeed")
            {
                this.GetComponent<MeshRenderer>().material.color = Color.green;
                print("Stay");
                //舊野要發光
            }
            if (other.gameObject.tag == "isFlask") //! 倒水
            {
                if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<Script_GameManager>().selectedObject != null)
                {

                    isFarmLandFilling = true;
                    // gameManager.GetComponent<GameManager_script>().nutrientList[index - 1].flaskFalling = true;
                    print("fallingTrue");
                }
            }
            else if (other.gameObject.tag == "isSeed") //!種子種植
            {
                if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<Script_GameManager>().selectedObject != null)
                {
                    StartCoroutine(BackToOriginalPos(other));

                    //!播animation    
                }

                //other.gameObject.SetActive(true);
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
        // if (other.gameObject.tag == "drag")
        // {
        this.GetComponent<MeshRenderer>().material.color = Color.white;
        //}
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

    IEnumerator BackToOriginalPos(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        other.gameObject.SetActive(false); //!試

        gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted = true; //種植 

        float x = original_pos.transform.position.x;
        float y = original_pos.transform.position.y;
        float z = original_pos.transform.position.z;
        seedObj.transform.position = new Vector3(x, y, z); //!番去原本個位 ????
        print(seedObj.transform.position);
        other.gameObject.SetActive(true);
        this.GetComponent<MeshRenderer>().material.color = Color.white;
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
