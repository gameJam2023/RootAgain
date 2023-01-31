using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Sirenix.OdinInspector;

[System.Serializable]
public class FarmLand
{
    public GameObject model = null;
    public int nutrientTotalCount;
    public int stageGrowingCount = 3;
    public int stageMatureCount = 5;
    public bool isFresh = true;
    public bool isGrowing = true;
    public bool isMature = true;
    public TMP_Text textNutrientTotalCount;
    public TMP_Text textCountNum;
}

public class GameManager_script : MonoBehaviour
{

    public List<FarmLand> farmlandList = new List<FarmLand>();
    public List<GameObject> nutrientList = new List<GameObject>();
    //public LayerMask layerMask;
    public GameObject selectedObject = null;
    public GameObject detectedObject = null;

    public float dragHeight = 0.25f;
    public GameObject seed;
    void Start()
    {
        seed.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //RaycastMouse();
        Drag();
        //DetectFarmLand();
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

    void Drag()
    {
        if (Input.GetMouseButtonDown(0)) //? select果下
        {
            if (selectedObject == null)
            {
                RaycastHit hit = CastRay();

                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("drag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;

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
        if (selectedObject != null)
        {
            SelectObjectPos(dragHeight);//!升起果下
        }
        if (Input.GetMouseButtonUp(0) && selectedObject != null) //? 放低果下
        {
            SelectObjectPos(0f);//!放但果下
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
}
