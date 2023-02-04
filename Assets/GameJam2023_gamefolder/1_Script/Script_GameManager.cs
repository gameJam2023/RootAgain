//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;
using MoreMountains.Feedbacks;


[System.Serializable]
public class FarmLand
{
    public GameObject model = null;
    public int index;
    public int nutrientTotalCount;
    public int flaskA_num = 0;
    public int flaskB_num = 0;
    public int flaskC_num = 0;
    public int stageGrowingCount = 3;
    public int stageMatureCount = 5;
    public bool isPlanted = false;
    public bool isFresh = false;
    // public bool isGrowing = false;
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
    public bool canDrag = true;

}

public class Script_GameManager : MonoBehaviour
{
    [FoldoutGroup("ObjectList")] public FarmLandDB farmLandDB;
    [FoldoutGroup("ObjectList")] public List<FarmLand> farmlandList = new List<FarmLand>();

    [FoldoutGroup("ObjectList")] public NutrientFlaskDB nutrientFlaskDataBase;
    [FoldoutGroup("ObjectList")] public List<NutrientFlask> nutrientList = new List<NutrientFlask>();

    [FoldoutGroup("SeedClass")] public SeedDB seedDateBase;
    [FoldoutGroup("SeedClass")] public List<Seeds> seedList = new List<Seeds>();
    [FoldoutGroup("SeedClass")] public bool seedCanDrag = true;
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

    //public GameObject seed;

    private void Awake()
    {
        for (int i = 0; i < seedDateBase.seedDatasList.Count; i++)
        {
            seedList.Add(new Seeds());
            seedList[i].model = seedDateBase.seedDatasList[i].model;
            seedList[i].index = seedDateBase.seedDatasList[i].index;
        }


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
        //DetectFarmLand();
    }

    void FarmLandChecking()
    {

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
        }
        if (Input.GetMouseButtonUp(0) && selectedObject != null && isFlask) //? 放低果下
        {
            // if (selectedObject.GetComponent<NutrientFlask_Original>().nutrientFlaskData.flaskFalling == false)
            // {
            SelectObjectPos(putDownheight_Flask);//!放低果
            selectedObject = null;
            Cursor.visible = true;


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
            selectedObject = null;
            Cursor.visible = true;
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





