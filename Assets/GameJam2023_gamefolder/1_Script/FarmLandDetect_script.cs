using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ForMakeGameNameSpace;


public class FarmLandDetect_script : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject seedObj = null;
    public GameObject original_pos;

    public int index;
    //public bool isFarmLandFilling = false; //!倒水果下


    // public Vector3 nutrientFlaskPosition;


    void Start()
    {

        gameManager.GetComponent<Script_GameManager>();

    }


    private void OnTriggerEnter(Collider other)
    {

        print("Enter");
    }
    private void OnTriggerStay(Collider other)
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


                // gameManager.GetComponent<GameManager_script>().nutrientList[index - 1].flaskFalling = true;
                if (!gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted) //! 沒seed
                {
                    print("you should plant a seed!");
                }
                else if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted) //!kb
                {

                    StartCoroutine(CheckTypeOfFlask(other));
                    StartCoroutine(CheckStage()); // ?Check 佢個stage
                    StartCoroutine(fallingTest(other)); //? falling animation 等你加
                }

                print("fallingTrue");
            }
        }
        else if (other.gameObject.tag == "isSeed") //!種子種植
        {
            if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<Script_GameManager>().selectedObject != null)
            {
                if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted)
                {
                    print("AlreadyPlant");
                    // StartCoroutine(BackToOriginalPos(other));
                    //StartCoroutine(GenerateNewSeed(other));
                }
                else if (!gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted)
                {
                    gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted = true; //種植
                    gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].seedTypeInLand = other.gameObject.GetComponent<Seed_script>().index;
                    other.gameObject.SetActive(false);
                    gameManager.GetComponent<Script_GameManager>().GenerateTest();
                    //StartCoroutine(BackToOriginalPos(other));
                    //StartCoroutine(GenerateNewSeed(other));

                }

                //StartCoroutine(RandomSeed());

                //!播animation
            }


            //other.gameObject.SetActive(true);
        }


        //gameManager.GetComponent<GameManager_script>().farmlandList[index - 1].nutrientTotalCount++;
        //gameManager.GetComponent<GameManager_script>().flaskFillingFeedBackList[index - 1].PlayFeedbacks();





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
        //this.isFarmLandFilling = false;
        yield return null;
        // gameManager.GetComponent<GameManager_script>().nutrientFlaskDataBase[other.]
    }

    IEnumerator BackToOriginalPos(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        other.gameObject.SetActive(false); //!試

        // gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].isPlanted = true; //種植
        // gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].seedTypeInLand = other.gameObject.GetComponent<Seed_script>().index;
        seedObj = other.gameObject;
        float x = original_pos.transform.position.x;
        float y = original_pos.transform.position.y;
        float z = original_pos.transform.position.z;
        seedObj.transform.position = new Vector3(x, y, z); //!番去原本個位 ????
        print("BackToOriginalPos");
        //other.gameObject.SetActive(true);
        this.GetComponent<MeshRenderer>().material.color = Color.white;
    }

    IEnumerator CheckTypeOfFlask(Collider other)
    {

        switch (other.gameObject.GetComponent<NutrientFlask_Original>().nutrientFlaskData.flaskIndex)
        {
            case 1:
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].nutrientTotalCount++;
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskA_num++;
                break;
            case 2:
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].nutrientTotalCount++;
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskB_num++;
                break;
            case 3:
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].nutrientTotalCount++;
                gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskC_num++;
                break;

        }
        print("CheckFlask");

        yield return null;
    }
    IEnumerator CheckStage()
    {

        if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].nutrientTotalCount == 5)
        {
            switch (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].seedTypeInLand) //? 種子type
            {
                case 1: //type A seed
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskA_num >= 3)
                    {
                        //Animation Seed1A
                    }
                    else if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskB_num >= 3)
                    {
                        //Animation Seed1B
                    }
                    else if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskC_num >= 3)
                    {
                        //Animation Seed1C
                    }
                    else
                    {
                        //Animation Seed1D
                    }
                    break;
                case 2: //tpye B seed
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskA_num >= 3)
                    {
                        //Animation Seed2A
                    }
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskB_num >= 3)
                    {
                        //Animation Seed2B
                    }
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskC_num >= 3)
                    {
                        //Animation Seed2C
                    }
                    else
                    {
                        //Animation Seed2D
                    }
                    break;
                case 3: //type C seed
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskA_num >= 3)
                    {
                        //Animation Seed3A
                    }
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskB_num >= 3)
                    {
                        //Animation Seed3B
                    }
                    if (gameManager.GetComponent<Script_GameManager>().farmlandList[index - 1].flaskC_num >= 3)
                    {
                        //Animation Seed3C
                    }
                    else
                    {
                        //Animation Seed3D
                    }
                    break;
            }
        }
        print("CheckStage");
        yield return null;
    }

    public IEnumerator GenerateNewSeed(Collider other)
    {
        // gameManager.GetComponent<Script_GameManager>().seedList = Shuffle.list(gameManager.GetComponent<Script_GameManager>().seedList); //Random seed
        gameManager.GetComponent<Script_GameManager>().seedList[0].model.SetActive(true);
        if (gameManager.GetComponent<Script_GameManager>().seedList[0].model.activeInHierarchy == true)
        {
            other.gameObject.transform.position = new Vector3(188f, 12, -188);
            print("GenerateNewSeed");
        }

        yield return new WaitForSeconds(0.2f);
    }

    // public IEnumerator GenerateNewSeed()
    // {
    //     gameManager.GetComponent<Script_GameManager>().seedList = Shuffle.list(gameManager.GetComponent<Script_GameManager>().seedList); //Random seed
    //     gameManager.GetComponent<Script_GameManager>().seedList[0].model.SetActive(true);
    //     print("true");
    //     if (gameManager.GetComponent<Script_GameManager>().seedList[0].model.activeInHierarchy == true)
    //     {
    //         gameManager.GetComponent<Script_GameManager>().seedList[0].model.transform.position = new Vector3(188f, 40, -188);
    //         print("GenerateNewSeed");
    //     }

    //     yield return null;
    // }


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
