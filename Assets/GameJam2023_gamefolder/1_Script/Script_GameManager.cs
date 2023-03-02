//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;
using ForMakeGameNameSpace;
using System.Collections;

[System.Serializable]
public class FarmLand
{
    public GameObject model = null;
    public int index;
    public int nutrientTotalCount;
    public int seedTypeInLand;
    public int flaskA_num = 0;
    public int flaskB_num = 0;
    public int flaskC_num = 0;
    public int stageGrowingCount = 3;
    public int stageMatureCount = 5;
    public bool isPlanted = false;
    // public bool isFresh = false;
    //public bool isGrowing = false;
    public bool isMature = false;
    public TMP_Text textNutrientTotalCount;
    public TMP_Text textCountNum;
}

[System.Serializable]
public class NutrientFlask
{
    [PreviewField(75)] public GameObject nutrientFlask_model;
    public bool flaskFalling = false;
    public bool canDrag = true;
    public int index;
}

[System.Serializable]
public class Seeds
{
    public GameObject model = null;
    public int index;
    //public bool canDrag = true;

}

[System.Serializable]
public class FinalPlant
{
    public GameObject growingModel;
    public GameObject matureModel;
}

public class Script_GameManager : MonoBehaviour
{
    [FoldoutGroup("ObjectList")] public FarmLandDB farmLandDB;
    [FoldoutGroup("ObjectList")] public List<FarmLand> farmlandList = new List<FarmLand>();

    [FoldoutGroup("ObjectList")] public NutrientFlaskDB nutrientFlaskDataBase;
    [FoldoutGroup("ObjectList")] public List<NutrientFlask> nutrientList = new List<NutrientFlask>();
    [FoldoutGroup("ObjectList")] public List<FinalPlant> finalCrop = new List<FinalPlant>();
    [FoldoutGroup("ObjectList")] public List<GameObject> ObjectGroup = new List<GameObject>();


    //[FoldoutGroup("SeedClass")] public SeedDB seedDateBase;
    [FoldoutGroup("SeedClass")] public List<Seeds> seedList = new List<Seeds>();
    [FoldoutGroup("SeedClass")] public GameObject seedSpawnPos;
    [FoldoutGroup("SeedClass")] public GameObject seedParent;
    [FoldoutGroup("SeedClass")] public bool seedCanDrag = true;
    [FoldoutGroup("SeedClass")] public MMF_Player seedSpawningAni;
    [FoldoutGroup("SeedClass")] public MMF_Player seedSpawningMachingeAni;
    //  [FoldoutGroup("ObjectList")] public List<Seeds> seedList = new List<Seeds>();


    //public List<Vector3> nutrientPositionList = new List<Vector3>();//! flask position
    public List<MMF_Player> flaskOpeningList = new List<MMF_Player>();//!MMfeedback
   
    public List<MMF_Player> flaskFillingFeedBackList = new List<MMF_Player>();//!flaskFillingAnimation
                                                                              // public List<GameObject> objectGroupList = new List<GameObject>();
                                                                              //public LayerMask layerMask;
    public GameObject selectedObject = null;
    public GameObject detectedObject = null;

    //public FarmLandDetect_script farmLandDetect_Script;

    //? select過下既高度
    [InfoBox("因為個parent scale 乘大咗40, so child 既高度都要乘大40,child 既localPosition 會係1即係冇變, set數值說用40黎做基準")]
    [InfoBox("putDownheigh 要低過基準少少,for touch 到collider ", InfoMessageType.Warning)]
    [FoldoutGroup("SelectUnit")] public float DragHeight_Flask;
    [FoldoutGroup("SelectUnit")] public float putDownOnFarmLandHeight_Flask;
    [FoldoutGroup("SelectUnit")] public float putDownheight_Flask;
    [FoldoutGroup("SelectUnit")] public float DragHeight_Seed;
    [FoldoutGroup("SelectUnit")] public float putDownheight_Seed;
    [FoldoutGroup("SelectUnit")] public bool isFlask = false;
    [FoldoutGroup("SelectUnit")] public bool isSeed = false;

    public Transform finalGroupParent;
    public int collectionCount;
    public TMP_Text collectionCount_text;
    public Color farmLandColorStay;
    public Color farmLandColorOriginal;
    public int EndCount;
    public MMF_Player Middee;
    public MMF_Player EndFeedBack;
    public int TotalEndNum;
    public bool isEnd = false;
    public Camera Camera;
    public float viewwidth;
    public float viewHi;

   
    //public GameObject seed;

    private void Awake()
    {
        // for (int i = 0; i < seedDateBase.seedDatasList.Count; i++)
        // {
        //     seedList.Add(new Seeds());
        //     seedList[i].model = seedDateBase.seedDatasList[i].model;
        //     seedList[i].index = seedDateBase.seedDatasList[i].index;
        // }
        GenerateSeed();
        //GenerateTheFirstSeed();

        for (int i = 0; i < nutrientFlaskDataBase.NutrientFlaskDataList.Count; i++)
        {
            nutrientList.Add(new NutrientFlask());
            nutrientList[i].nutrientFlask_model = nutrientFlaskDataBase.NutrientFlaskDataList[i].flaskModel;
            nutrientList[i].flaskFalling = nutrientFlaskDataBase.NutrientFlaskDataList[i].flaskFalling;
            nutrientList[i].index = nutrientFlaskDataBase.NutrientFlaskDataList[i].flaskIndex;
            nutrientList[i].canDrag = nutrientFlaskDataBase.NutrientFlaskDataList[i].canDrag;
        }

        for (int i = 0; i < farmLandDB.farmLandDataList.Count; i++)
        {
            farmlandList.Add(new FarmLand());
            farmlandList[i].model = farmLandDB.farmLandDataList[i].model;
            farmlandList[i].index = farmLandDB.farmLandDataList[i].farmLandIndex;
            farmlandList[i].nutrientTotalCount = farmLandDB.farmLandDataList[i].nutrientTotalCount;
            farmlandList[i].stageGrowingCount = farmLandDB.farmLandDataList[i].stageGrowingCount;
            farmlandList[i].stageMatureCount = farmLandDB.farmLandDataList[i].stageMatureCount;
        }
        foreach (var feedBack in flaskOpeningList)
        {
            //feedBack.GetComponent<MMF_Player>().
            feedBack.PlayFeedbacks();
        }
    }
    void Start()
    {

        // //? flask position
        // for (int i = 0; i < nutrientPositionList.Count; i++)
        // {
        //     nutrientPositionList[i] = nutrientList[i].nutrientFlask_model.transform.position;

        // }



    }

    // Update is called once per frame
    void Update()
    {
        //RaycastMouse();
        Drag();
        if (EndCount >= TotalEndNum && !isEnd)
        {
            PlayEndAni();
            isEnd = true;
        }

        //DetectFarmLand();
    }

    public void PlayEndAni()
    {
        // Middee.PlayFeedbacks();
        Camera.orthographicSize = 150;
        StartCoroutine(ZeroPointEightsecond());
        // EndFeedBack.PlayFeedbacks();
        print("end");

        Camera.orthographicSize = 100f;
        StartCoroutine(ZeroPointEightsecond());
        Camera.orthographicSize = 50f;
        StartCoroutine(ZeroPointEightsecond());
        Camera.orthographicSize = 6000f;

    }
    public IEnumerator ZeroPointEightsecond()
    {
        yield return new WaitForSeconds(0.8f);
    }


    void DetectFarmLand()
    {

        if (Input.GetMouseButton(0) && selectedObject != null)
        {
            for (int i = 0; i < farmlandList.Count; i++)
            {
                Ray ray = new Ray(farmlandList[i].model.transform.position, Vector3.up);
                RaycastHit hit;
                Physics.Raycast(ray, out hit);
                Debug.DrawRay(farmlandList[i].model.transform.position, Vector3.up, Color.green);
                detectedObject = hit.transform.gameObject;
                //Physics.Raycast(farmlandList[i].model.transform.position, Vector3.up);
                Debug.Log("detecting");

            }
        }


    }

    void SpawnSeed()
    {
        //seedList;

    }

    //? Object Pool
    // void SpawnSeed()
    // {
    //     GameObject spawnedSeed = ObjectPool.instance.GetPooledObject();

    //     if (spawnedSeed != null)
    //     {
    //         spawnedSeed.SetActive(true);
    //     }
    // }

    // IEnumerator SeedGenerator()
    // {
    //     yield return new WaitForSeconds(1f);
    // }

    void PlantSeed(GameObject spawnedSeed)
    {
        spawnedSeed.SetActive(true);
    }
    void Drag()
    {
        if (Input.GetMouseButtonDown(0)) //? select果下
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    // if (!hit.collider.CompareTag("drag"))  //!hit.collider.CompareTag("drag") || 
                    // {
                    //     print("return");
                    //     return;
                    // }
                    if (hit.collider.CompareTag("Untagged"))
                    {
                        print("return");
                        return;
                    }
                    if (hit.collider.CompareTag("isFlask"))
                    {
                        isFlask = true;
                        print("isFlask");
                        selectedObject = hit.collider.gameObject;
                        Cursor.visible = false;
                    }
                    if (hit.collider.CompareTag("isSeed"))
                    {
                        isSeed = true;
                        print("isSeed");
                        selectedObject = hit.collider.gameObject;
                        Cursor.visible = false;
                    }
                    if (hit.collider.CompareTag("isBook"))
                    {
                        for (int i = 0; i < farmlandList.Count; i++)
                        {
                            if (farmlandList[i].isMature)
                            {
                                collectionCount++;
                            }
                        }
                        collectionCount_text.text = collectionCount.ToString();
                        ObjectGroup[0].SetActive(true);

                    }

                    // else if (hit.collider.CompareTag("isFlask"))
                    // {
                    //     isFlask = true;
                    // }
                    // else if (hit.collider.CompareTag("isSeed"))
                    // {
                    //     isSeed = true;
                    // }
                    // if (hit.collider.TryGetComponent(out Seed_script seed))
                    // {
                    //     isSeed = true;
                    //     print("isSeed");
                    // }
                    // if (hit.collider.TryGetComponent(out NutrientFlaskData nutrientFlask))
                    // {
                    //     isFlask = true;
                    //     print("isFlask");
                    // }




                }
            }
            // else
            // {
            //     SelectObjectPos(0f);
            //     selectedobject = null;
            //     Cursor.visible = true;
            // }
        }


        //!上面過慮完先落呢度
        if (selectedObject != null && isFlask)
        {
            SelectObjectPos(DragHeight_Flask);//!升起果下
        }
        else if (selectedObject != null && isSeed)
        {
            SelectObjectPos(DragHeight_Seed);
            print("dragSeed");
        }
        if (Input.GetMouseButtonUp(0) && selectedObject != null && isFlask) //? 放低果下
        {
            // if (selectedObject.GetComponent<NutrientFlask_Original>().nutrientFlaskData.flaskFalling == false)
            // {
            SelectObjectPos(putDownheight_Flask);//!放低果
            selectedObject = null;
            Cursor.visible = true;
            isFlask = false;


            //}
            #region reference
            // }
            // else if (isSeed)
            // {
            //     SelectObjectPos(putDownheight_Seed);
            //     isSeed = false;
            // }
            #endregion
        }
        else if (Input.GetMouseButtonUp(0) && selectedObject != null && isSeed)
        {
            SelectObjectPos(putDownheight_Seed);
            //selectedObject.SetActive(false);
            print("Seeddown");
            selectedObject = null;
            Cursor.visible = true;
            isSeed = false;
        }


    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
           Input.mousePosition.x,
           Input.mousePosition.y,
           Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        return hit;
    }

    private void SelectObjectPos(float yPosition)
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
        selectedObject.transform.position = new Vector3(worldPosition.x, yPosition, worldPosition.z);

    }

    // void GenerateTheFirstSeed()
    // {
    //     seedList = Shuffle.list(seedList); //Random seed
    //     seedList[0].model.SetActive(true);
    //     print("true");
    //     if (seedList[0].model.activeInHierarchy == true)
    //     {
    //         seedList[0].model.transform.position = new Vector3(188f, 40, -188);
    //         print("GenerateFirstSeed");
    //     }
    // }

    //!try this
    public void GenerateSeed()
    {
        seedParent.transform.position = new Vector3(0, 0, 0);
        //? MMF_Player 有個setting 係比Target 的child 去control animation, 咁呢每之move呢都要set番Vector 0,0,0, 先set 000再spawn seed 先做到個效果
        seedList = Shuffle.list(seedList);
        Vector3 pos = new Vector3(seedSpawnPos.transform.position.x, seedSpawnPos.transform.position.y, seedSpawnPos.transform.position.z);
        GameObject newSeed = Instantiate(seedList[0].model, pos, Quaternion.identity, seedParent.transform);

        seedSpawningAni.PlayFeedbacks();
        seedSpawningMachingeAni.PlayFeedbacks();
        // seedParent.transform.position = new Vector3(0, 0, 0);
        //Vector3 endPos = new Vector3(newSeed.transform.position.x, 10, newSeed.transform.position.z);
        //newSeed.transform.position = Vector3.Lerp(newSeed.transform.position, endPos, 1f);
    }
    IEnumerator GrowingAnimation(int id, GameObject cropPlace)
    {
        GameObject growingModel = finalCrop[id].growingModel;
        GameObject matureModel = finalCrop[id].matureModel;
        float x = cropPlace.transform.position.x;
        float y = cropPlace.transform.position.y;
        float z = cropPlace.transform.position.z;
        Vector3 pos = new Vector3(x, y, z);
        Instantiate(growingModel, pos, Quaternion.identity, finalGroupParent);
        yield return new WaitForSeconds(1f);
        growingModel.SetActive(false);
        Instantiate(matureModel, pos, Quaternion.identity, finalGroupParent);
        print("ani");

    }

    public void GrowingTest(int id, Vector3 pos)
    {
        pos = new Vector3(pos.x, pos.y, pos.z);
        Instantiate(finalCrop[id].growingModel, pos, Quaternion.identity, finalGroupParent);
        print("Test");
        StartCoroutine(Second());
        Instantiate(finalCrop[id].matureModel, pos, Quaternion.identity, finalGroupParent);

    }

    //     public void Spawn(int id)
    //     {
    //  Instantiate(finalCrop[id].growingModel,)
    //     }

    public void GrowingAni(int id, GameObject cropPlace)
    {
        StartCoroutine(GrowingAnimation(id, cropPlace));
    }

    IEnumerator Second()
    {
        yield return new WaitForSeconds(1f);
    }


}

#region ReferenceCode

// void RaycastMouse()
// {
//     if (Input.GetMouseButton(0))
//     {
//         RaycastHit hit;
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//         if (Physics.Raycast(ray, out hit, 1000f))
//         {
//             GameObject nutrientClick = hit.collider.gameObject;
//             for (int i = 0; i < nutrientList.Count; i++)
//             {
//                 if (nutrientClick == nutrientList[i].gameObject)
//                 {
//                     print(nutrientClick);
//                 }
//             }

//         }
//     }
// }

#endregion





